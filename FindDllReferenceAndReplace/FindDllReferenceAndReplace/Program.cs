using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis.MSBuild;
using NLog.Fluent;
using NLog;

namespace FindReferences
{
    class Program
    {
        public static List<string> dllList= new List<string>();

        public static List<References> referenceList = new List<References>();

        public static Logger logger = new LogFactory().GetLogger("logs");
        public static Logger logger1 = new LogFactory().GetLogger("dlltoChange");
        static void Main(string[] args)
        {
            if (args[0] == null)
            {
               throw new Exception();
            }


            var dlls = FindReferencesFromFile();

            foreach (var dll in dlls)
            {
                //LoadNewProjectDetails(dll.NewPath);
                FindReferences(args[1], dll);
            }
        }

        public static List<string> GetDllToFind(string path)
        {
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                GetDllToFind(directory);
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fff = GetFileName(file);
                if (!string.IsNullOrWhiteSpace(fff))
                {
                    dllList.Add(fff);
                    //logger.Info(fff);
                }
                else
                {
                    //logger.Info(file);
                }
                
            }

            return dllList;
        }

        public static string GetFileName(string filename)
        {
            var fff = Regex.Match(filename, @"\\\w*\\\w*\\\w*[.]\S*", RegexOptions.IgnoreCase);
            return fff.Value;
        }

        public static List<References> FindReferencesFromFile()
        {
            var references = File.ReadAllText("References.txt");
            var filess = references.Split('\n');
            foreach (var file in filess)
            {
               
                var split = file.Trim(' ').Split(' ').Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToList();
                
                if (split.Count > 2 || split.Count < 2)
                {
                    logger.Error($"Invalid record {split}");
                    break;
                }
                referenceList.Add(new References(){ OldPath = split[0], NewPath = split[1]});
            }
            

            return referenceList;
        }

        public static void FindReferences(string path, References dllName)
        {
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                FindReferences(directory, dllName);
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (file.EndsWith(".csproj"))
                {
                    ParseProjectFile(file, dllName);
                }
            }
        }

        public static void ParseProjectFile(string path, References dllName)
        {
            if (Directory.Exists(path + ".TEMP"))
            {
                File.Delete(path + ".TEMP");
            }
            StreamReader reader = null;
            //StreamWriter writer = null;
            try
            {
                reader = new StreamReader(path);
                //writer = new StreamWriter(path +".TEMP");
                bool containsname = false;
                string line, prevline = null;
                MemoryStream stream = new MemoryStream();
                reader.BaseStream.CopyTo(stream);
                stream.Position = 0;
                reader.BaseStream.Position = 0;
                
                var string1 = new StreamReader(stream).ReadToEnd();
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(dllName.OldPath))
                    {
                        containsname = true;
                        //var index = string1.IndexOf(prevline);
                        string1 = GetXmlFile(string1, dllName, path);
                        string1 = string1.Replace(@"utf-16", "utf-8");

                        //string1 = ReplaceReference(string1, xmltag);
                        //var index = string1.IndexOf(line, StringComparison.Ordinal);

                        //var nextline = reader.ReadLine();
                        //if(nextline != null)
                        //{
                        //    var indexOfRef = string1.IndexOf("\n", index, StringComparison.Ordinal);
                        //    var indexofnextline = string1.IndexOf("\n", indexOfRef+1, StringComparison.Ordinal);
                        //    string1 = string1.Remove(indexOfRef, indexofnextline-indexOfRef);
                        //}
                        ////GetXmlFile(string1, line);
                        //string1 = prevline != null ? ReplaceReference(string1, prevline) : string1;
                        //string1 = ReplaceReference(string1, line);

                        logger1.Info($"{dllName}");
                        break;
                    }

                    if(!line.Contains("SpecificVersion"))
                    prevline = line;
                }

                reader?.Close();
                stream?.Close();
                if (containsname)
                {
                    var streamwriter = new StreamWriter(path);
                    var task = streamwriter.WriteAsync(string1);
                    task.Wait();
                    streamwriter.Close();
                }
                //writer.Close();
                //if (containsname)
                //{
                //    File.Delete(path);
                //    File.Copy(path + ".TEMP", path);
                //    File.Delete(path + ".TEMP");
                //}
                //else
                //{
                //    File.Delete(path + ".TEMP");
                //}

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                reader?.Close();
            }
        }

        private static string ReplaceReference(string data, string lineToReplace)
        {
            string updateData = string.Empty;
            var writer = new StringWriter(new StringBuilder(data));
            {
                writer.GetStringBuilder().Replace(lineToReplace, "");
                updateData = writer.ToString();
            }
            writer.Close();
            return updateData;
        }

        private static string GetXmlFile(string data, References dllname, string path)
        {
            
            var stream = new MemoryStream();
            var doc = new XmlDocument();
            
            doc.LoadXml(data);
            
            doc.Save(stream);
            stream.Position = 0;
            bool imported = false;
            //var nodes = doc?.DocumentElement?.SelectNodes("/Project/ItemGroup/Reference");


            foreach (XmlNode childnodes in doc.ChildNodes)
            {
                foreach (XmlNode node in childnodes.ChildNodes)
                {
                    foreach (XmlNode reference in node.ChildNodes)
                    {
                        if (reference.InnerText.Contains(dllname.OldPath))
                        {
                            reference.ParentNode?.RemoveChild(reference);
                            //reference.InnerXml = String.Empty;
                            //reference.RemoveAll();
                            break;
                        }
                        if (reference.Name == "ProjectReference" && !imported)
                        {
                            var doc1 = new XmlDocument();
                            var child = LoadNewProjectDetails(dllname.NewPath, path);
                            
                            doc1.LoadXml(GetProjectReference(child));
                            XmlNode importNode = reference.OwnerDocument?.ImportNode(doc1.ChildNodes[0], true);
                            
                            if (importNode != null)
                            {
                                reference.ParentNode?.AppendChild(importNode);
                                
                                imported = true;
                            }
                        }
                    }
                    
                } 
            }
            
            doc.InnerXml = doc.InnerXml.Replace(@" xmlns=""""", string.Empty);
            return GetProjectReference(doc, false);
            //if (nodes == null)
            //{
            //    return string.Empty;
            //}
            //foreach (XmlNode node in nodes)
            //{
            //    if (node.InnerText.Contains(oldPath))
            //    {
            //        node.RemoveAll();
            //    } 
            //}

            //Project obj = (Project)new XmlSerializer(typeof(Project), new XmlRootAttribute() { Namespace = "http://schemas.microsoft.com/developer/msbuild/2003" }).Deserialize(stream);


            //foreach (var node in obj.ItemGroup)
            //{
            //    var ss1 = node.ProjectReference.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.HintPath) && x.HintPath.Contains(oldPath));
            //    if (ss1 != null)
            //    {
            //        var xmva = GetProjectReference(ss1);
            //        doc.InnerXml = doc.InnerXml.Replace(xmva, "");
            //        xmva = GetProjectReference(doc);
            //        return xmva;
            //    }
            //}


            //return null;
        }

        private static string GetProjectReference<T>(T project, bool omitxmldeclaration= true)
        {
            StringWriter stringWriter = new StringWriter();
            
            var mm = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = omitxmldeclaration,
                Encoding = Encoding.UTF8,
                
            };
            xws.Encoding = Encoding.UTF8;
            // This is probably the default
            var xtw = XmlWriter.Create(stringWriter, xws);
            
            var nmspace = new XmlSerializerNamespaces();
            nmspace.Add("", "");
            new XmlSerializer(typeof(T), new XmlRootAttribute() { Namespace = "" }).Serialize(xtw, project, nmspace);
            xtw.Close();
            var sss = stringWriter.GetStringBuilder();

            //stringWriter.Close();
            return sss.ToString();
        }

        public static ProjectReference LoadNewProjectDetails(string path, string projectPath)
        {
            var project1 = projectPath
                .Replace(@"C:\stash\GemEnterprise", string.Empty)
                .Replace(Path.GetFileName(projectPath), string.Empty)
                .Split('\\').Count(x => !string.IsNullOrWhiteSpace(x));

            var seperator = string.Empty;
            for (int i =0; i <project1; i++)
            {
                seperator = seperator + @"..\";
            }

            using (var ms = MSBuildWorkspace.Create())
            {
                var currentProject = ms.OpenProjectAsync(path).Result;
                var prokectReference = new ProjectReference()
                {
                    Include = seperator + currentProject.FilePath.Replace(@"C:\stash\GemEnterprise", string.Empty).TrimStart('\\'),
                    Name = currentProject.Name,
                    Project = "{"+$"{currentProject.Id.Id.ToString()}"+"}"
                };
                ms.CloseSolution();
                return prokectReference;
            }
        }

        //private static void LOadWOrkspace(string path, References dllName)
        //{
        //    using (var ms = MSBuildWorkspace.Create())
        //    {
        //        var currentProject = ms.OpenProjectAsync(path).Result;
        //        var aa = currentProject.GetCompilationAsync().Result;
        //        var projectToAdd = ms.OpenProjectAsync(dllName.NewPath).Result;

        //        var currentProject1 =
        //            currentProject.Solution.AddProject(projectToAdd.Name, projectToAdd.AssemblyName, projectToAdd.Language);
        //        //var ss = currentProject1.GetCompilationAsync().Result;
        //        var tt = currentProject1.Solution.Workspace.TryApplyChanges(currentProject.Solution);
        //        ms.CloseSolution();
        //    }
        //}
    }
}

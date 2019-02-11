using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace findfoundationreference
{
    class Program
    {
        public static List<string> dllList = new List<string>();

        public static Logger logger = new LogFactory().GetLogger("logs");

        static void Main(string[] args)
        {
            if (args[0] == null)
            {
                throw new Exception("Specify file to find");
            }
            if (args[1] == null)
            {
                throw new Exception("specify repo to filter .csproj files");
            }

            var reftoFind = args[0];
            var placeToFInd = args[1];

            FindReferences(placeToFInd, reftoFind);
        }

        public static void FindReferences(string path, string dllName)
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

        public static void ParseProjectFile(string path, string dllName)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);
                string line =null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(dllName))
                    {
                        logger.Info($"{path}  {dllName}");
                        Console.WriteLine($"{path}  {dllName}");
                        break;
                    }
                }

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
    }
}

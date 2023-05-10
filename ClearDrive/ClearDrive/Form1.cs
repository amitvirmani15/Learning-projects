using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearDrive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var database = new Databaseconnection();
            var list = database.ReadTable();
            
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listView1.LabelWrap = true;
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
            listView1.View = View.List;
            listView1.Columns[0].Width = 1000;
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            foreach (var file in list)
            {
                listView1.Items.Add(new ListViewItem() { Text = file });
            }
            
        }

        private string ReadString()
        {
            string filelist = string.Empty;
            var database = new Databaseconnection();
            var list = database.ReadTable();
            foreach (var file in list)
            {
                filelist = filelist + file + "\n";
            }

            return filelist;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            var database = new Databaseconnection();
            list.Add(Path.GetTempPath());
            //connection.CreateTable();
            //database.CreateTable();
            var files = database.ReadTable();
            list.AddRange(files);

            foreach (var file1 in list)
            {
                if (!string.IsNullOrEmpty(file1))
                {
                    Delete(file1);
                }
            }

        }

        private void Delete(string path)
        {
            try
            {
                var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);

                    }
                }

                foreach (var file in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        Directory.Delete(file, true);

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);

                    }
                }
            }
            catch (Exception exception)
            {
                if (exception is AccessViolationException)
                {
                    throw;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            {
                
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var database = new Databaseconnection();
                database.SavePath(dialog.SelectedPath);
            }
        }
    }
}

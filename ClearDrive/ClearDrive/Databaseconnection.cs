using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace ClearDrive
{
    public class Databaseconnection
    {
        public void CreateTable()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=CLeardrive.db;Version = 3;New = True;");
            var command = conn.CreateCommand();
            var text = "Create table FileList(filename varchar(2000))";
            command.CommandText = text;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }

        public List<string> ReadTable()
        {
            List<string> files = new List<string>();
            SQLiteConnection conn = new SQLiteConnection("Data Source=CLeardrive.db");
            var command = conn.CreateCommand();
            var text = "Select filename from FileList";
            command.CommandText = text;
            conn.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    files.Add(reader.GetString(0));
                }
            }
            conn.Close();

            return files;
        }

        public List<string> SavePath(string path)
        {
            
            List<string> files = new List<string>();

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=CLeardrive.db"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                var transaction = conn.BeginTransaction();
                command.Transaction = transaction;

                var text = $"insert into FileList values('{path}')";
                command.CommandText = text;

                command.ExecuteNonQuery();
                transaction.Commit();
                conn.Close();
               


            }

            return files;
        }
    }

}

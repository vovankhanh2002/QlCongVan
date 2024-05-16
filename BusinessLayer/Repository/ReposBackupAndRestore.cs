using AccsessLayer;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class ReposBackupAndRestore : IBackupAndRestore
    {
        public void BackupDatabase(string connectionString, string backupFilePath)
        {
            string sqlCommand = $"RESTORE DATABASE [QLCongvan] FROM DISK = '{backupFilePath}' WITH REPLACE";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void RestoreDatabase(string connectionString, string backupFilePath)
        {
            string sqlCommand = $"RESTORE DATABASE [QLCongvan] FROM DISK = '{backupFilePath}' WITH REPLACE";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}

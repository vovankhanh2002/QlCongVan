using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.IRepository
{
    public interface IBackupAndRestore
    {
        void BackupDatabase(string connectionString, string backupFilePath);
        void RestoreDatabase(string connectionString, string backupFilePath);
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyShop.Model;
using MyShop.Services;
using System;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Windows.Storage;

namespace MyShop.Repository
{
    public abstract class RepositoryBase
    {
        private string _connectionString;
        private IConfigurationRoot _config;
        private string _dbUsername;
        private string _dbPassword;
        private string _dbName;
        private bool _firstTimeFlag;
        private string _connectDbStatus;
        public RepositoryBase()
        {
            //Connect to database and verify data
            _config = new ConfigurationBuilder().AddUserSecrets<MainWindow>().Build();
            // Ý nghĩa: tìm assembly có lớp MainWindow chứa SecretID
            _dbName = _config.GetSection("DB")["Database"];

        }

        protected void setDbAccountInfo(string username, string password, bool flag=false)
        {
            _dbUsername = username;
            _dbPassword = password;
            _firstTimeFlag = flag;
        }

        protected void changeConnectionString(string method)
        {
            if (method.Equals("Unsigned"))
            {
                _connectionString = _config.GetSection("DB")["Unsigned"];
            }
            else if (method.Equals("Signed")) 
            {
                _connectionString = _config.GetSection("DB")["Signed"];
                //get from local
                string dbusername = System.Configuration.ConfigurationManager.AppSettings["dbUsername"]!;
                string dbpasswordIn64 = System.Configuration.ConfigurationManager.AppSettings["dbPassword"];
                string entropyIn64 = System.Configuration.ConfigurationManager.AppSettings["dbEntropy"]!;


                if (dbpasswordIn64.Length != 0)
                {
                    byte[] entropyInBytes = Convert.FromBase64String(entropyIn64);
                    byte[] cypherTextInBytes = Convert.FromBase64String(dbpasswordIn64);

                    byte[] passwordInBytes = ProtectedData.Unprotect(cypherTextInBytes,
                        entropyInBytes,
                        DataProtectionScope.CurrentUser
                    );

                    string dbpassword = Encoding.UTF8.GetString(passwordInBytes);
                    if (!_firstTimeFlag)
                    {
                        setDbAccountInfo(dbusername, dbpassword);
                    }
                }

                _connectionString = _connectionString.Replace("@username", _dbUsername);
                _connectionString = _connectionString.Replace("@password", _dbPassword);
            }
            _connectionString = _connectionString.Replace("@database", _dbName);
        }
        protected SqlConnection GetConnection()
        {
            _connectDbStatus = System.Configuration.ConfigurationManager.AppSettings["dbConnectStatus"];
            changeConnectionString(_connectDbStatus);
            return new SqlConnection(_connectionString);
        }

        protected OleDbConnection GetOleSqlConnection(StorageFile file)
        {
            string olbConnectionString = _config.GetSection("DB")["OlbConnectionString"];
            olbConnectionString = olbConnectionString.Replace("@fileName", file.Name);
            return new OleDbConnection(olbConnectionString);
        }
    }
}

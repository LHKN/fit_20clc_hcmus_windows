using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyShop.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyShop.Repository
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            //Connect to database and verify data

            var extconfig = new ConfigurationBuilder().AddUserSecrets<MainWindow>().Build();
            // Ý nghĩa: tìm assembly có lớp MainWindow chứa SecretID

            string connectionString = extconfig.GetSection("DB")["ConnectionString"];
            string username = extconfig.GetSection("DB")["Username"];
            string password = extconfig.GetSection("DB")["Password"];
            string database = extconfig.GetSection("DB")["Database"];
            connectionString = connectionString.Replace("@username", username);
            connectionString = connectionString.Replace("@password", password);
            connectionString = connectionString.Replace("@database", database);

            _connectionString = connectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

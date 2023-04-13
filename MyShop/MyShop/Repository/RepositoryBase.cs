using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyShop.View;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var connectionString = extconfig.GetSection("DB")["ConnectionString"];
            var username = extconfig.GetSection("DB")["Username"];
            var password = extconfig.GetSection("DB")["Password"];
            var database = extconfig.GetSection("DB")["Database"];
            connectionString.Replace("@username", username);
            connectionString.Replace("@password", password);
            connectionString.Replace("@database", database);

            _connectionString = connectionString;

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

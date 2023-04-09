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
            string username = "sa";
            string password = "sqlpassword123";
            const string db_name = "db_book";

            //var extconfig = new ConfigurationBuilder().AddUserSecrets<LoginPage>().Build();
            //// Ý nghĩa: tìm assembly có lớp LoginPage chứa SecretID

            //var connectionString = extconfig.GetSection("DB")["ConnectionString"];
            //connectionString.Replace("@username", username);
            //connectionString.Replace("@password", password);
            //connectionString.Replace("@db_name", db_name);

            var connectionString = "Server=.\\sqlexpress;User ID=sa;Password=sqlpassword123;Database=db_book;TrustServerCertificate=True";

            _connectionString = connectionString;

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

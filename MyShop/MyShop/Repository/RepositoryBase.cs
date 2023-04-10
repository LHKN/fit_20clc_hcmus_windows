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
            //may be save information in usersecret?
            String username = "sa";
            String password = "sqlpassword123";
            String db_name = "db_book";

            //var extconfig = new ConfigurationBuilder().AddUserSecrets<LoginPage>().Build();
            // Ý nghĩa: tìm assembly có lớp LoginPage chứa SecretID

            //var connectionString = extconfig.GetSection("DB")["ConnectionString"];
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            //builder.DataSource = ".\\sqlexpress";
            //builder.UserID = "sa";
            //builder.Password= password;
            //builder.TrustServerCertificate= true;
            //builder.InitialCatalog = db_name;
            var connectionString = $"Server=.\\sqlexpress;User ID={username};Password={password};Database={db_name};TrustServerCertificate=True";
            //connectionString.Replace("@username", username);
            //connectionString.Replace("@password", password);
            //connectionString.Replace("@db_name", db_name);



            _connectionString = connectionString;

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

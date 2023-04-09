using Microsoft.Data.SqlClient;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateAccount(NetworkCredential credentical)
        {
            bool validUser;
            int role_id = 0;
            var connection = GetConnection();
            connection.Open();
            string sql = "select role_id from ACCOUNT where username = @username and password = @password";
            var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credentical.UserName;
            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credentical.Password;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                role_id = (int)reader["role_id"];
            }

            if (role_id == 1) { validUser = true; }
            else validUser = false;

            return validUser;
        }

        public void Edit(Account account)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetByUsername(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.VisualBasic.Logging;
using MyShop.Model;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyShop.Repository
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AuthenticateAccount(NetworkCredential credentical)
        {
            int role_id = 0;
            bool isValidAccount = false;
            string message = string.Empty;
            var connection = GetConnection();
            string password, passwordIn64 = string.Empty, entropyIn64 = string.Empty;
            await Task.Run(() =>
            {
                try
                {
                    connection.Open();
                }
                catch(Exception ex) { message = "Connection timed out!"; }
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select password, entropy, role_id from ACCOUNT where username = @username";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credentical.UserName;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    passwordIn64 = Convert.ToString(reader["password"]);
                    entropyIn64 = Convert.ToString(reader["entropy"]);
                    role_id = Convert.ToInt32(reader["role_id"]);
                }

                //Decrypting..
                byte[] entropyInBytes = Convert.FromBase64String(entropyIn64);
                byte[] cypherTextInBytes = Convert.FromBase64String(passwordIn64);

                byte[] passwordInBytes = ProtectedData.Unprotect(cypherTextInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser
                );

                password = Encoding.UTF8.GetString(passwordInBytes);

                if (role_id == 1 && password.Equals(credentical.Password))
                {
                    isValidAccount = true;
                }
                else isValidAccount = false;

                message = isValidAccount ? "TRUE" : "* Invalid username or password!";

                // Code saving encrypted password (function similar to register)
                //{
                //    var passwordInBytes = Encoding.UTF8.GetBytes(credentical.Password);
                //    var entropy = new byte[20];
                //    using (var rng = RandomNumberGenerator.Create())
                //    {
                //        rng.GetBytes(entropy);
                //    }

                //    var cypherText = ProtectedData.Protect(
                //        passwordInBytes,
                //        entropy,
                //        DataProtectionScope.CurrentUser
                //    );

                //    var passwordIn64 = Convert.ToBase64String(cypherText);
                //    var entropyIn64 = Convert.ToBase64String(entropy);


                //    string sql = "update ACCOUNT set password=@password, entropy=@entropy where username=@username";
                //    var command = new SqlCommand(sql, connection);
                //    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credentical.UserName;
                //    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = passwordIn64;
                //    command.Parameters.Add("@entropy", SqlDbType.NVarChar).Value = entropyIn64;
                //    MessageBox.Show(command.ExecuteNonQuery().ToString());
                //    isValidAccount = true;
                //}
                connection.Close();

            }
            
            return message;

        }

        public void Edit(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Account>> GetCustomers()
        {
            List<Account> accounts = new List<Account>();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex) { }
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select id, name, phone_number from ACCOUNT where role_id = @role_id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@role_id", SqlDbType.Int).Value = 2;

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string phoneNumber = Convert.ToString(reader["phone_number"]);

                    accounts.Add(new Account
                    {
                        Id = id,
                        Name = name,
                        PhoneNumber = phoneNumber,
                    });
                }

                connection.Close();
            }

            return accounts;
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByUsername(string username)
        {
            Account account = new Account();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex) { }
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {

                string sql = "select id, name, phone_number from ACCOUNT where role_id = @role_id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@role_id", SqlDbType.Int).Value = 2;

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string phoneNumber = Convert.ToString(reader["phone_number"]);

                    account = new Account
                    {
                        Id = id,
                        Name = name,
                        PhoneNumber = phoneNumber,
                    };
                }

                connection.Close();
            }

            return account;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}

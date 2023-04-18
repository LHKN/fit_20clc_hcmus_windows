using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    class BillRepository : RepositoryBase, IBillRepository
    {

        public async void Add(Bill bill)
        {
            bool isSuccessful = false;
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
                string sql = "insert into BILL (id,customer_id,total_price,transaction_date)" +
                    "values (@id,@customer_id,@total_price,@transaction_date)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = bill.Id;
                command.Parameters.Add("@customer_id", SqlDbType.Int).Value = bill.CustomerId;
                command.Parameters.Add("@total_price", SqlDbType.Decimal).Value = bill.TotalPrice;
                command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = bill.TransactionDate;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }

        public async void Edit(Bill bill)
        {
            bool isSuccessful = false;
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
                string sql = "update BILL set id=@id,customer_id=@customer_id,total_price=@total_price,transaction_date=@transaction_date" +
                    "where id = @id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = bill.Id;
                command.Parameters.Add("@customer_id", SqlDbType.Int).Value = bill.CustomerId;
                command.Parameters.Add("@total_price", SqlDbType.Decimal).Value = bill.TotalPrice;
                command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = bill.TransactionDate;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }

        public async void Remove(int id)
        {
            bool isSuccessful = false;
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
                string sql = "delete from BILL where id = @id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }

        public async Task<List<Bill>> GetAll(String date)
        {
            List<Bill> billList = null;
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
                string sql;
                var command = new SqlCommand();

                if (date.IsNullOrEmpty())
                {
                    sql = "select id,customer_id,total_price,transaction_date from BILL";
                    command = new SqlCommand(sql, connection);
                    
                }
                else
                {
                    sql = "select id,customer_id,total_price,transaction_date from BILL" +
                    "where transaction_date = @transaction_date";
                    command = new SqlCommand(sql, connection);
                    command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = date;
                }

                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int customerId = Convert.ToInt32(reader["customer_id"]);
                    decimal totalPrice = Convert.ToDecimal(reader["total_price"]);
                    DateTime transactionDate = Convert.ToDateTime(reader["transaction_date"]);

                    billList.Add(new Bill
                    {
                        Id = id,
                        CustomerId = customerId,
                        TotalPrice = totalPrice,
                        TransactionDate = transactionDate
                    });
                }

                connection.Close();
            }

            return billList;
        }

        public async Task<Bill> GetById(int id)
        {
            Bill newBill = null;
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
                string sql = "select id,customer_id,total_price,transaction_date from BILL" +
                    "where id = @id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int customerId = Convert.ToInt32(reader["customer_id"]);
                    decimal totalPrice = Convert.ToDecimal(reader["total_price"]);
                    DateTime transactionDate = Convert.ToDateTime(reader["transaction_date"]);

                    newBill = new Bill
                    {
                        Id = id,
                        CustomerId = customerId,
                        TotalPrice = totalPrice,
                        TransactionDate = transactionDate
                    };
                }
                connection.Close();
            }
            return newBill;
        }


        public async void AddBillDetail(int billId, BillDetail billDetail)
        {

        }
        
        public async void EditBillDetail(int billId, BillDetail billDetail)
        {

        }
        
        public async void RemoveBillDetail(int billId, int billDetailId)
        {

        }
    }
}

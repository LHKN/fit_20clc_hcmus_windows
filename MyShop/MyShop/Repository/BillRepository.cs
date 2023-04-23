using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MyShop.Repository
{
    class BillRepository : RepositoryBase, IBillRepository
    {

        public async Task Add(Bill bill)
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
                string sql = "insert into BILL (customer_id,total_price,transaction_date)" +
                    "values (@customer_id,@total_price,@transaction_date)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@customer_id", SqlDbType.Int).Value = bill.CustomerId;
                command.Parameters.Add("@total_price", SqlDbType.Int).Value = bill.TotalPrice;
                command.Parameters.Add("@transaction_date", SqlDbType.Date).Value = bill.TransactionDate;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }

        public async Task Edit(Bill bill)
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
                string sql = "update BILL set customer_id=@customer_id,total_price=@total_price,transaction_date=@transaction_date" +
                    "where id=@id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = bill.Id;
                command.Parameters.Add("@customer_id", SqlDbType.Int).Value = bill.CustomerId;
                command.Parameters.Add("@total_price", SqlDbType.Int).Value = bill.TotalPrice;
                command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = bill.TransactionDate;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }

        public async Task Remove(int id)
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
                string sql = "delete from BILL where id=@id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }

        public async Task<List<Bill>> GetAll(DateOnly? dateFrom, DateOnly? dateTo)
        {
            List<Bill> billList = new List<Bill>();
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

                if (dateFrom == null || dateTo == null)
                {
                    sql = "select id,customer_id,total_price,transaction_date from BILL";
                    command = new SqlCommand(sql, connection);
                    
                }
                else
                {
                    sql = "select id,customer_id,total_price,transaction_date from BILL " +
                    "where transaction_date between @date_from and @date_to";
                    command = new SqlCommand(sql, connection);
                    command.Parameters.Add("@date_from", SqlDbType.Date).Value = dateFrom;
                    command.Parameters.Add("@date_to", SqlDbType.Date).Value = dateTo;
                }

                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int customerId = Convert.ToInt32(reader["customer_id"]);
                    int totalPrice = Convert.ToInt32(reader["total_price"]);

                    object obj = reader["transaction_date"];
                    DateOnly transactionDate = obj == null || obj == DBNull.Value ? default(DateOnly) : DateOnly.FromDateTime(Convert.ToDateTime(obj));

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
                    "where id=@id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int customerId = Convert.ToInt32(reader["customer_id"]);
                    int totalPrice = Convert.ToInt32(reader["total_price"]);

                    object obj = reader["published_date"];
                    DateOnly transactionDate = obj == null || obj == DBNull.Value ? default(DateOnly) : DateOnly.FromDateTime(Convert.ToDateTime(obj));

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

        public async Task<List<int>> GetEmptyBillId()
        {
            List<int> ids = new List<int>();
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
                string sql = "select id from BILL " +
                    "where total_price=@price and transaction_date=@date";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@price", SqlDbType.Int).Value = 0;
                command.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.Now;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    ids.Add(id);
                }
                connection.Close();
            }
            return ids;
        }

        public async Task<List<BillDetail>> GetBillDetailById(int billId)
        {
            List<BillDetail> details = new List<BillDetail>();
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
                string sql = "select price,number,book_id from DETAILED_BILL " +
                    "where bill_id=@bill_id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@bill_id", SqlDbType.Int).Value = billId;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int bookId = Convert.ToInt32(reader["book_id"]);
                    int price = Convert.ToInt32(reader["price"]);
                    int number = Convert.ToInt32(reader["number"]);

                    details.Add(new BillDetail
                    {
                        BillId = billId,
                        BookId = bookId,
                        Price = price,
                        Number = number
                    });
                }
                connection.Close();
            }
            return details;
        }

        public async Task AddBillDetail(BillDetail billDetail)
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
                string sql = "insert into DETAILED_BILL (bill_id,book_id,price,number)" + // revise this
                    "values (@bill_id,@book_id,@price,@number)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@bill_id", SqlDbType.Int).Value = billDetail.BillId;
                command.Parameters.Add("@book_id", SqlDbType.Int).Value = billDetail.BookId;
                command.Parameters.Add("@price", SqlDbType.Int).Value = billDetail.Price;
                command.Parameters.Add("@number", SqlDbType.Int).Value = billDetail.Number;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }
        
        public async Task EditBillDetail(int billId, int bookId, BillDetail billDetail)
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
                string sql = "update DETAILED_BILL set price=@price,number=@number" +
                    "where bill_id = @bill_id and book_id = @book_id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@bill_id", SqlDbType.Int).Value = billDetail.BillId;
                command.Parameters.Add("@book_id", SqlDbType.Int).Value = billDetail.BookId;
                command.Parameters.Add("@price", SqlDbType.Int).Value = billDetail.Price;
                command.Parameters.Add("@number", SqlDbType.DateTime).Value = billDetail.Number;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }
        
        public async Task RemoveBillDetail(int billId, int bookId)
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
                string sql = "delete from BILL where bill_id=@bill_id and book_id=@book_id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@bill_id", SqlDbType.Int).Value = billId;
                command.Parameters.Add("@book_id", SqlDbType.Int).Value = bookId;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0) { isSuccessful = true; }
                else { isSuccessful = false; }

                connection.Close();
            }
        }
    }
}

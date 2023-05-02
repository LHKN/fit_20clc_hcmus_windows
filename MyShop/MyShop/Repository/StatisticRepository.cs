using Microsoft.Data.SqlClient;
using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    public class StatisticRepository: RepositoryBase, IStatisticRepository
    {
       public async Task<List<Tuple<DateTime, int>>> GetDailyStatistic(DateTime startDate, DateTime endDate)
        {
            var connection = GetConnection();
            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if(connection != null && connection.State == ConnectionState.Open)
            {
                /*                string sql = "select * from BILL as bill" +
                                    "where DATEDIFF(DAY, @startDate, bill.transaction_date) >= 0 and" +
                                    "DATEDIFF(DAY, bill.transaction_date, @endDate) >= 0";*/
                string sql = "GetDailyRevenue";

                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@start_date", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.AddWithValue("@end_date", SqlDbType.Date).Value = endDate.Date;
                try
                {
                    List<Tuple<DateTime, int>> result = new List<Tuple<DateTime, int>>();
                    var reader =  command.ExecuteReader();

                    while(reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["date"]);
                        int revenue = Convert.ToInt32(reader["revenue"]);
                        Tuple<DateTime, int> record = new Tuple<DateTime, int>(date, revenue);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tuple<int, DateTime>>> GetListOfWeeks()
        {
            var connection = GetConnection();
            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                /*                string sql = "select * from BILL as bill" +
                                    "where DATEDIFF(DAY, @startDate, bill.transaction_date) >= 0 and" +
                                    "DATEDIFF(DAY, bill.transaction_date, @endDate) >= 0";*/
                string sql = "GetListOfWeeks";

                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    List<Tuple<int, DateTime>> result = new List<Tuple<int, DateTime>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int week = Convert.ToInt32(reader["week"]);
                        DateTime start_date = Convert.ToDateTime(reader["start_date"]);
                        Tuple<int, DateTime> record = new Tuple<int, DateTime>(week, start_date);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tuple<DateTime, int>>> GetWeeklyStatistic(DateTime startDate, DateTime endDate)
        {
            var connection = GetConnection();
            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                /*                string sql = "select * from BILL as bill" +
                                    "where DATEDIFF(DAY, @startDate, bill.transaction_date) >= 0 and" +
                                    "DATEDIFF(DAY, bill.transaction_date, @endDate) >= 0";*/
                string sql = "GetWeeklyRevenue";

                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@start_date_start_week", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.AddWithValue("@start_date_end_week", SqlDbType.Date).Value = endDate.Date;
                try
                {
                    List<Tuple<DateTime, int>> result = new List<Tuple<DateTime, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["date"]);
                        int revenue = Convert.ToInt32(reader["revenue"]);
                        Tuple<DateTime, int> record = new Tuple<DateTime, int>(date, revenue);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public async Task<List<Tuple<DateTime, int>>> GetMonthlyStatistic(DateTime startDate, DateTime endDate)
        {
            var connection = GetConnection();
            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                /*                string sql = "select * from BILL as bill" +
                                    "where DATEDIFF(DAY, @startDate, bill.transaction_date) >= 0 and" +
                                    "DATEDIFF(DAY, bill.transaction_date, @endDate) >= 0";*/
                string sql = "GetMonthlyRevenue";

                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@start_date_start_month", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.AddWithValue("@start_date_end_month", SqlDbType.Date).Value = endDate.Date;
                try
                {
                    List<Tuple<DateTime, int>> result = new List<Tuple<DateTime, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["date"]);
                        int revenue = Convert.ToInt32(reader["revenue"]);
                        Tuple<DateTime, int> record = new Tuple<DateTime, int>(date, revenue);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tuple<DateTime, int>>> GetYearlyStatistic(DateTime startDate, DateTime endDate)
        {
            var connection = GetConnection();
            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                /*                string sql = "select * from BILL as bill" +
                                    "where DATEDIFF(DAY, @startDate, bill.transaction_date) >= 0 and" +
                                    "DATEDIFF(DAY, bill.transaction_date, @endDate) >= 0";*/
                string sql = "GetYearlyRevenue";

                var command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@start_date_start_year", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.AddWithValue("@start_date_end_year", SqlDbType.Date).Value = endDate.Date;
                try
                {
                    List<Tuple<DateTime, int>> result = new List<Tuple<DateTime, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["date"]);
                        int revenue = Convert.ToInt32(reader["revenue"]);
                        Tuple<DateTime, int> record = new Tuple<DateTime, int>(date, revenue);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tuple<string, int>>> GetProductStatistic(DateTime startDate, DateTime endDate)
        {
            List<Book> books = new List<Book>();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select book.title,ISNULL(SUM(dt.number), 0) number" +
                             "from book left join (select * from DETAILED_BILL " +
                                                   "join Bill ON Bill.id = Detailed_bill.bill_id " +
                                                   "where Bill.transaction_date BETWEEN @startDate AND @endDate) " +
                                                   "on Book.id = dt.book_id " +
                             "group by book.title";

                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate.Date;

                try
                {
                    List<Tuple<string, int>> result = new List<Tuple<string, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string title = Convert.ToString(reader["title"]);
                        int quantity = Convert.ToInt32(reader["number"]);
                        Tuple<string, int> record = new Tuple<string, int>(title, quantity);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetWeeklyNumberOfSoldBookStatistic(DateTime startDate, DateTime endDate)
        {
            int numberOfSoldBook = 0;
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select ISNULL(SUM(DETAILED_BILL.number),0) quantity " +
                             "from DETAILED_BILL join BILL on DETAILED_BILL.bill_id = BILL.id" +
                             " where BILL.transaction_date BETWEEN @startDate AND @endDate";

                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate.Date;


                try
                {
                    List<Tuple<string, int>> result = new List<Tuple<string, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        numberOfSoldBook = Convert.ToInt32(reader["quantity"]);
                       
                    }
                    reader.Close();
                    connection.Close();
                    return numberOfSoldBook;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> GetWeeklyNumberOfOrderStatistic(DateTime startDate, DateTime endDate)
        {
            int numberOfOrder = 0;
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select count(id) quantity " +
                             "from BILL" +
                             " where transaction_date BETWEEN @startDate AND @endDate";

                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate.Date;


                try
                {
                    List<Tuple<string, int>> result = new List<Tuple<string, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        numberOfOrder = Convert.ToInt32(reader["quantity"]);

                    }
                    reader.Close();
                    connection.Close();
                    return numberOfOrder;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<Tuple<string, int>>> GetTop5ProductStatistic(DateTime startDate, DateTime endDate)
        {
            List<Book> books = new List<Book>();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select TOP 5 book.title,ISNULL(SUM(dt.number), 0) quantity " +
                             "from book left join (select *from DETAILED_BILL " +
                                                   "join Bill ON Bill.id = Detailed_bill.bill_id " +
                                                   "where Bill.transaction_date BETWEEN @startDate AND @endDate) as dt " +
                                                   "on Book.id = dt.book_id " +
                             "group by book.title "+
                             "order by quantity desc";


                var command = new SqlCommand(sql, connection);
                command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate.Date;

                try
                {
                    List<Tuple<string, int>> result = new List<Tuple<string, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string title = Convert.ToString(reader["title"]);
                        int quantity = Convert.ToInt32(reader["quantity"]);
                        Tuple<string, int> record = new Tuple<string, int>(title, quantity);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tuple<string, int>>> GetProductQuantityStatistic()
        {
            List<Book> books = new List<Book>();
            var connection = GetConnection();

            await Task.Run(() =>
            {
                connection.Open();
            }).ConfigureAwait(false);

            if (connection != null && connection.State == ConnectionState.Open)
            {
                string sql = "select book.title, book.quantity " +
                             "from book" +
                             " order by book.quantity asc";

                var command = new SqlCommand(sql, connection);
                
                try
                {
                    List<Tuple<string, int>> result = new List<Tuple<string, int>>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string title = Convert.ToString(reader["title"]);
                        int quantity = Convert.ToInt32(reader["quantity"]);
                        Tuple<string, int> record = new Tuple<string, int>(title, quantity);
                        result.Add(record);
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}

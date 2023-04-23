using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repository
{
    public class StatisticRepository: RepositoryBase
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
    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Model
{
    internal interface IStatisticRepository
    {
        Task<List<Tuple<DateTime, int>>> GetDailyStatistic(DateTime startDate, DateTime endDate);

        Task<List<Tuple<int, DateTime>>> GetListOfWeeks();

        Task<List<Tuple<DateTime, int>>> GetWeeklyStatistic(DateTime startDate, DateTime endDate);

        Task<List<Tuple<DateTime, int>>> GetMonthlyStatistic(DateTime startDate, DateTime endDate);

        Task<List<Tuple<DateTime, int>>> GetYearlyStatistic(DateTime startDate, DateTime endDate);
      
    }
}

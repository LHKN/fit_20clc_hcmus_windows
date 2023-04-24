using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    class DailyProductViewModel : ViewModelBase
    {
       /* public ISeries[] Series { get; set; }
       = new ISeries[]*/
//{
//    //new LineSeries<double>
//    //{
//    //    Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
//    //    Fill = null
//    //}

//    new ColumnSeries<int>
//    {
//        Name = "Income",
//        Values = new[] { 4, 4, 7, 2, 8 ,50,30,20,10,4, 4, 7, 2, 8 ,50,30,20,10},

//        Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
//        Fill = new SolidColorPaint(SKColors.CornflowerBlue),

//    },
//new ColumnSeries<int>
//{
//    Values = new [] { 7, 5, 3, 2, 6 },
//    Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 8 },
//    Fill = null,
//}

//            new ColumnSeries<City>
//{
//    Name = "Population",
//    TooltipLabelFormatter = point => $"{point.Model.Name} {point.PrimaryValue:N2}M",
//    Values = new[]
//    {
//        new City { Name = "Tokyo", Population = 4, LandArea = 3 },
//        new City { Name = "New York", Population = 6, LandArea = 4 },
//        new City { Name = "Seoul", Population = 2, LandArea = 1 },
//        new City { Name = "Moscow", Population = 8, LandArea = 7 },
//        new City { Name = "Shanghai", Population = 3, LandArea = 2 },
//        new City { Name = "Guadalajara", Population = 4, LandArea = 5 }
//    }
//}

//                new ColumnSeries<City>
//    {
//        Name = "Population",
//        TooltipLabelFormatter = (point) => $"{point.Model.Name} population: {point.PrimaryValue:N2}M",
//        Values = new City[]
//{
//    new City { Name = "Tokyo", Population = 4, LandArea = 3 },
//    new City { Name = "New York", Population = 6, LandArea = 4 },
//    new City { Name = "Seoul", Population = 2, LandArea = 1 },
//    new City { Name = "Moscow", Population = 8, LandArea = 7 },
//    new City { Name = "Shanghai", Population = 3, LandArea = 2 },
//    new City { Name = "Guadalajara", Population = 4, LandArea = 5 }
//},
//        Mapping = (city, point) =>
//        {
//            point.PrimaryValue = (float)city.Population;
//            point.SecondaryValue = point.Context.Index;
//        }
//    },

// };
//public async Task<Dictionary<string, int>> GetProductStatistic(string query)
//{
//    Dictionary<string, int> productStatict = new Dictionary<string, int>();
//    List<string> result = new List<string>();
//    var connection = GetConnection();

//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        await connection.OpenAsync();
//        using (SqlCommand command = new SqlCommand($"SELECT * FROM {tableName}", connection))
//        {
//            using (SqlDataReader reader = await command.ExecuteReaderAsync())
//            {
//                while (await reader.ReadAsync())
//                {
//                    // Đọc giá trị từ cột có tên "column_name" và thêm vào danh sách kết quả
//                    string columnName = reader["column_name"].ToString();
//                    result.Add(columnName);
//                }
//            }
//        }
//    }

//    return productStatict;
//}

//LiveCharts.Configure(config =>
//    config
//    .HasMap<Dictionary<string, int>>((city, point) =>
//    {
//};


/*{
    new ColumnSeries<int>
    {
        Values = new  [] { 200, 558, 458, 249 },
    }
};

        public Axis[] XAxes { get; set; }
            = new Axis[]*/


/*
{
    new Axis
    {
        // Use the labels property to define named labels.
        Labels = new string[] { "Anne", "Johnny", "Zac", "Rosa" }
    }
};

        public Axis[] YAxes { get; set; }
            = new Axis[]

{
    new Axis
    {
        // Now the Y axis we will display labels as currency
        // LiveCharts provides some common formatters
        // in this case we are using the currency formatter.
        Labeler = Labelers.Currency 

        // you could also build your own currency formatter
        // for example:
        // Labeler = (value) => value.ToString("C")

        // But the one that LiveCharts provides creates shorter labels when
        // the amount is in millions or trillions
    }*/

    }
}

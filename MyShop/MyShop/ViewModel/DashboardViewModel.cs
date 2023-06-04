using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Model;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using MyShop.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using System.Windows.Input;
using Windows.ApplicationModel.Contacts;
using System.Linq;
using Windows.ApplicationModel.VoiceCommands;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;

namespace MyShop.ViewModel
{

    partial class DashboardViewModel : ViewModelBase,INotifyPropertyChanged
    {
        public class BookQuantity
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public string Status { get; set; }
            public string ColorStatus { get; set; }

           
            public BookQuantity(string name,int quantity,string status,string colorStatus)
            {
                Name= name;
                Quantity= quantity;
                Status= status;
                ColorStatus= colorStatus;
            }
        }

        public ObservableCollection<BookQuantity> BookQuantityList { get; private set; }
        public ObservableCollection<BookQuantity> allBookQuantity { get; private set; }
        private List<Tuple<string,int>> bookQuantityList;
        public ICommand Load_page { get; set; }
        public ICommand OnFilterChanged { get; set; }

        private IStatisticRepository _statisticRepository;
        public IEnumerable<ISeries> TopMonthlyBestSellerSeries { get; set; }

        public string MonthlyRevenue { get; set; }
        public string WeeklyRevenue { get; set; }
        public int NumberOfSoldBook { get; set; }
        public int NumberOfOrder { get; set; }

        private ObservableCollection<Tuple<string, int>> displayBookQuantityCollection;
        public ListView FilteredListView { get; set; }

        public ISeries[] TopWeeklyBestSellerSeries { get; set; }
        public string filterContent { get; set; }


        [ObservableProperty]
        private Axis[] _xAxes = { 
            new Axis { 
                        SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220)),
                        Name="Top 5 best selling books of the week",
                        NameTextSize=15},
            
        };

        [ObservableProperty]
        private Axis[] _yAxes = { new Axis { IsVisible = false } };

        public LabelVisual TopMonthlyBestSellerTitle { get; set; } =
            new LabelVisual
            {
                Text = "Top 5 best selling books of the month",
                TextSize = 15,
        
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

        public List<ISeries> TopYearlyBestSellerSeries { get; set; } 

        public Axis[] YearlyXAxes { get; set; } =
        {
            new Axis
            {
                Name = "Top 5 best selling books of the year",
                NameTextSize=15,
               Labels = null,
                TextSize=10
            }
        };

        public Axis[] YearlyYAxes { get; set; } =
        {
        new Axis
        {
            Name = "",
            NamePadding = new LiveChartsCore.Drawing.Padding(0, 15),

            LabelsPaint = new SolidColorPaint
            {
                Color = SKColors.Blue,
                FontFamily = "Times New Roman",
                SKFontStyle = new SKFontStyle(SKFontStyleWeight.ExtraBold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic)
            },
        }
    };

        [Obsolete]
        public DashboardViewModel()
        {
            _statisticRepository = new StatisticRepository();
            BookQuantityList = new ObservableCollection<BookQuantity>();
            allBookQuantity = new ObservableCollection<BookQuantity>();
            filterContent = "";
            displayBookQuantityCollection = new ObservableCollection<Tuple<string, int>>();
            OnFilterChanged = new RelayCommand<TextChangedEventArgs>(FilterChanged);
            Load_page = new RelayCommand<RoutedEventArgs>(Load_Dashboard);
        }

        private async void Load_Dashboard(RoutedEventArgs e)
        {
            //monthly revenue
            char seperator = '/';
            int day = 1;
            int month = DateTimeOffset.Now.Month;
            int year = DateTimeOffset.Now.Year;

            String year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();
            DateTime startMonthlyDate = DateTime.Parse(year_month_day);
            

            var monthlyRevenueTask = await _statisticRepository.GetMonthlyStatistic(startMonthlyDate.Date, DateTimeOffset.Now.Date);
            MonthlyRevenue = monthlyRevenueTask.Last().Item2.ToString("C", CultureInfo.GetCultureInfo("vi-VN"));

            //weekly revenue
            DateTime startWeeklyDate = DateTime.Parse(year_month_day);
            var getWeekTask = await _statisticRepository.GetListOfWeeks();
            startWeeklyDate = getWeekTask[getWeekTask.Count - 2].Item2;

            var weeklyRevenueTask = await _statisticRepository.GetWeeklyStatistic(startWeeklyDate.Date, DateTimeOffset.Now.Date);
            WeeklyRevenue = weeklyRevenueTask.Last().Item2.ToString("C", CultureInfo.GetCultureInfo("vi-VN"));

            //daily number of sold books
            NumberOfSoldBook =await _statisticRepository.GetWeeklyNumberOfSoldBookStatistic(startWeeklyDate.Date, DateTimeOffset.Now.Date);

            //daily number of orders
            NumberOfOrder = await _statisticRepository.GetWeeklyNumberOfOrderStatistic(startWeeklyDate.Date, DateTimeOffset.Now.Date);

            //top 5 best selling books of the week
            var top5WeeklyBook = await _statisticRepository.GetTop5ProductStatistic(startWeeklyDate.Date, DateTimeOffset.Now.Date);

            if (top5WeeklyBook == null)
            {
                top5WeeklyBook = new List<Tuple<string, int>>();
                top5WeeklyBook.Add(new Tuple<string, int>("Book 1", 1));
                top5WeeklyBook.Add(new Tuple<string, int>("Book 2", 1));
                top5WeeklyBook.Add(new Tuple<string, int>("Book 3", 1));
                top5WeeklyBook.Add(new Tuple<string, int>("Book 4", 1));
                top5WeeklyBook.Add(new Tuple<string, int>("Book 5", 1));
            }

            TopWeeklyBestSellerSeries = top5WeeklyBook
                .Select(x => new RowSeries<ObservableValue>
                {
                    Values = new[] { new ObservableValue(x.Item2) },
                    Name = x.Item1,
                    Stroke = null,
                    MaxBarWidth = 80,                    
                    DataLabelsSize=10,
                    DataLabelsPaint = new SolidColorPaint(new SKColor(245, 245, 245)),
                    DataLabelsPosition = DataLabelsPosition.Right,
                    DataLabelsTranslate = new LvcPoint(-1, 0),
                    DataLabelsFormatter = point => $"{point.Context.Series.Name} {point.PrimaryValue}"
                })
                .OrderByDescending(x => ((ObservableValue[])x.Values!)[0].Value)
                .ToArray();



            //top 5 best selling books of the month
            var top5MonthlyBook = await _statisticRepository.GetTop5ProductStatistic(startMonthlyDate.Date, DateTimeOffset.Now.Date);

            if (top5MonthlyBook == null)
            {
                top5MonthlyBook = new List<Tuple<string, int>>();
                top5MonthlyBook.Add(new Tuple<string, int>("None", 1));
            }

            TopMonthlyBestSellerSeries = top5MonthlyBook.AsLiveChartsPieSeries((value, series) =>
            {
                // here you can configure the series assigned to each value.
                series.Name = $"{value.Item1}";
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                series.DataLabelsSize = 10;
                //series.DataLabelsRotation = LiveCharts.TangentAngle + 90;
                series.Mapping = (value, p) => {
                    p.PrimaryValue = value.Item2;

                };
                series.LegendShapeSize = 40;
                series.DataLabelsFormatter = p => $"{value.Item1} {p.StackedValue.Share:P2}";
            });

            //top 5 best selling books of the year
            month = 1;
            year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();
            DateTime startYearlyDate = DateTime.Parse(year_month_day);
            var top5YearlyBook = await _statisticRepository.GetTop5ProductStatistic(startYearlyDate.Date, DateTimeOffset.Now.Date);

            if (top5YearlyBook == null)
            {
                top5YearlyBook = new List<Tuple<string, int>>();
                top5YearlyBook.Add(new Tuple<string, int>("Book 1", 0));
                top5YearlyBook.Add(new Tuple<string, int>("Book 2", 0));
                top5YearlyBook.Add(new Tuple<string, int>("Book 3", 0));
                top5YearlyBook.Add(new Tuple<string, int>("Book 4", 0));
                top5YearlyBook.Add(new Tuple<string, int>("Book 5", 0));
            }
            
            List<string> labels= new List<string>();

            top5YearlyBook.ForEach(book =>
            {
                labels.Add(book.Item1);
            });

            YearlyXAxes[0].Labels = labels;
            TopYearlyBestSellerSeries = new List<ISeries>();

            TopYearlyBestSellerSeries.Add(new ColumnSeries<Tuple<string, int>>
            {
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                Values = top5YearlyBook,

                Fill = new SolidColorPaint(SKColors.Blue),

                Mapping = (taskItem, point) =>
                {
                    point.PrimaryValue = (int)taskItem.Item2;
                    point.SecondaryValue = point.Context.Index;
                },
                TooltipLabelFormatter = point => $"{point.Model.Item1.ToString()}: {point.PrimaryValue.ToString()}"
            });

            //books running out of stock
            updateBookQuantityList();         
        }

     
        private void FilterChanged(TextChangedEventArgs newText)
        {
            
            BookQuantityList = new ObservableCollection<BookQuantity>( allBookQuantity);
            var filtered = allBookQuantity.Where(book => book.Name.Contains(filterContent, StringComparison.InvariantCultureIgnoreCase));
            Remove_NonMatching(filtered);
            AddBack_Contacts(filtered);

        }

        private void Remove_NonMatching(IEnumerable<BookQuantity> filteredData)
        {
            for (int i = BookQuantityList.Count - 1; i >= 0; i--)
            {
                var item = BookQuantityList[i];
                // If contact is not in the filtered argument list, remove it from the ListView's source.
                if (!filteredData.Contains(item))
                {
                    BookQuantityList.Remove(item);
                }
            }
        }

        private void AddBack_Contacts(IEnumerable<BookQuantity> filteredData)
        {
            foreach (var item in filteredData)
            {
                // If item in filtered list is not currently in ListView's source collection, add it back in
                if (!BookQuantityList.Contains(item))
                {
                    BookQuantityList.Add(item);
                }
            }
        }


        private async void updateBookQuantityList()
        {
            bookQuantityList = await _statisticRepository.GetProductQuantityStatistic();
            displayBookQuantityCollection.Clear();
         
            bookQuantityList.ForEach(book =>
            {
                string status = "";
                string colorStatus = "";

                if (book.Item2 == 0)
                {
                    status = "Out of stock";
                    colorStatus= "Red";
                }
                else if (book.Item2 <=5)
                {
                    status = "Low stock";
                    colorStatus = "Yellow";       
                }
                else
                {
                    status = "In stock";
                    colorStatus = "Green";
                }

                allBookQuantity.Add(new BookQuantity(book.Item1, book.Item2, status, colorStatus));
            });
            BookQuantityList = new ObservableCollection<BookQuantity>(allBookQuantity);
        }
        }
}

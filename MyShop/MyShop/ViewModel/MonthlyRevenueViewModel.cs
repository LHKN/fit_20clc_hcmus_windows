using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI.Xaml.Controls;
using MyShop.Repository;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    class MonthlyRevenueViewModel : ViewModelBase
    {
        private StatisticRepository _statisticRepository;

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set;}

        public ICommand StartDateChangeCommand { get; set; }
        public ICommand EndDateChangeCommand { get; set; }

        public List<ISeries> MonthlyRevenueSeries { get; set; }

        public Axis[] XAxes { get; set; } =
       {
            new Axis
            {
                Name = "Month",
                // Use the labels property for named or static labels 
               Labels = null
            }
        };

        public Axis[] YAxes { get; set; } =
        {
        new Axis
        {
            Name = "Revenue (VND)",
            NamePadding = new LiveChartsCore.Drawing.Padding(0, 15),

            LabelsPaint = new SolidColorPaint
            {
                Color = SKColors.Blue,
                FontFamily = "Times New Roman",
                SKFontStyle = new SKFontStyle(SKFontStyleWeight.ExtraBold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic)
            },

            // Use the Labeler property to give format to the axis values 
            // Now the Y axis we will display it as currency
            // LiveCharts provides some common formatters
            // in this case we are using the currency formatter.
            Labeler = (value) => value.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))

            // you could also build your own currency formatter
            // for example:
            // Labeler = (value) => value.ToString("C")

            // But the one that LiveCharts provides creates shorter labels when
            // the amount is in millions or trillions
        }
    };

        DateTime SelectedStartDate;
        DateTime SelectedEndDate;

        public MonthlyRevenueViewModel()
        {
            _statisticRepository = new StatisticRepository();
            MonthlyRevenueSeries = new List<ISeries>();

            MonthlyRevenueSeries.Add(new LineSeries<Tuple<DateTime, int>>
            {
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                Values = new List<Tuple<DateTime, int>>() { new Tuple<DateTime, int>(DateTime.Now, 1), new Tuple<DateTime, int>(DateTime.Now, 1), new Tuple<DateTime, int>(DateTime.Now, 1), new Tuple<DateTime, int>(DateTime.Now, 1), new Tuple<DateTime, int>(DateTime.Now, 1) },
                
                GeometryStroke = null,
                GeometryFill = null,
                Mapping = (taskItem, point) =>
                {
                    point.PrimaryValue = (int)taskItem.Item2;
                    point.SecondaryValue = point.Context.Index;
                },
                TooltipLabelFormatter = point => $"{point.Model.Item1.ToShortDateString()} revenue: {point.PrimaryValue.ToString("C", CultureInfo.GetCultureInfo("vi-VN"))}"
            });

            StartDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;

            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            StartDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnStartDateChange);
            EndDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnEndDateChange);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetMonthlyStatistic(SelectedStartDate.Date, SelectedEndDate.Date);

            var series = new LineSeries<Tuple<DateTime, int>>();

            series = (LineSeries<Tuple<DateTime, int>>)MonthlyRevenueSeries.ElementAt(0);
            series.Values = task;

            MonthlyRevenueSeries.Clear();
            MonthlyRevenueSeries.Add(series);

            XAxes[0].Name = $"Revenue from {SelectedStartDate.Date.ToShortDateString()} to {SelectedEndDate.Date.ToShortDateString()}";

            XAxes[0].Labels = null;
        }

        private void OnStartDateChange(DatePickerValueChangedEventArgs a)
        {
            char seperator = '/';
            int day = 1;
            int month = StartDate.Date.Month;
            int year = StartDate.Date.Year;
            string year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();
            SelectedStartDate = DateTime.Parse(year_month_day);
            if (SelectedStartDate.Date < SelectedEndDate.Date)
            {
                DisplayChart();
            }
        }

        private void OnEndDateChange(DatePickerValueChangedEventArgs a)
        {
            char seperator = '/';
            int day = 1;
            int month = EndDate.Date.Month;
            int year = EndDate.Date.Year;
            string year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();
            SelectedEndDate = DateTime.Parse(year_month_day);
            if (SelectedStartDate.Date < SelectedEndDate.Date)
            {
                DisplayChart();
            }
        }


    }
}

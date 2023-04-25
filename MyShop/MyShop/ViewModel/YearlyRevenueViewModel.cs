using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore;
using Microsoft.UI.Xaml.Controls;
using MyShop.Repository;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView;
using System.Globalization;

namespace MyShop.ViewModel
{
    class YearlyRevenueViewModel : ViewModelBase
    {
        //public DateTime

        public ICommand StartDateChangeCommand { get; private set; }
        public ICommand EndDateChangeCommand { get; private set; }

        private DateTime SelectedStartDate;

        private DateTime SelectedEndDate;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        private StatisticRepository _statisticRepository;

        public List<ISeries> YearlyRevenueSeries { get; private set; }

        public Axis[] XAxes { get; set; } =
       {
            new Axis
            {
                Name = "Date",
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

        public YearlyRevenueViewModel()
        {
            YearlyRevenueSeries = new List<ISeries>();

            YearlyRevenueSeries.Add(new LineSeries<Tuple<DateTime, int>>
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

            _statisticRepository = new StatisticRepository();

            StartDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;
            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            StartDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnStartDateChanged);
            EndDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnEndDateChanged);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetYearlyStatistic(SelectedStartDate.Date, SelectedEndDate.Date);

            var series = new LineSeries<Tuple<DateTime, int>>();

            series = (LineSeries<Tuple<DateTime, int>>)YearlyRevenueSeries.ElementAt(0);
            series.Values = task;

            YearlyRevenueSeries.Clear();
            YearlyRevenueSeries.Add(series);

            XAxes[0].Name = $"Revenue from {SelectedStartDate.Date.ToShortDateString()} to {SelectedEndDate.Date.ToShortDateString()}";

            XAxes[0].Labels = null;
        }
        
        private void OnStartDateChanged(DatePickerValueChangedEventArgs e)
        {
            char seperator = '/';
            int day = 1;
            int month = 1;
            int year = StartDate.Date.Year;

            String year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();

            SelectedStartDate = DateTime.Parse(year_month_day);

            if(SelectedStartDate < SelectedEndDate)
            {
                DisplayChart();
            }
        }

        private void OnEndDateChanged(DatePickerValueChangedEventArgs e)
        {
            char seperator = '/';
            int day = 1;
            int month = 1;
            int year = EndDate.Date.Year;

            String year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();

            SelectedEndDate = DateTime.Parse(year_month_day);

            if (SelectedStartDate < SelectedEndDate)
            {
                DisplayChart();
            }
        }

    }
}

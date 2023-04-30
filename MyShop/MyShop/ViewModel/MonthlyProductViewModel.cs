using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView;

namespace MyShop.ViewModel
{
    class MonthlyProductViewModel : ViewModelBase
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public ICommand StartDateChangeCommand { get; set; }
        public ICommand EndDateChangeCommand { get; set; }

        DateTime SelectedStartDate;
        DateTime SelectedEndDate;
        private StatisticRepository _statisticRepository;

        public List<ISeries> MonthlyProductSeries { get; set; } 

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Name = "",            
               Labels = null
            }
        };

        public Axis[] YAxes { get; set; } =
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

        public MonthlyProductViewModel()
        {
            MonthlyProductSeries = new List<ISeries>();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new Tuple<string, int>($"Book {i}", 1)); 
            }

            MonthlyProductSeries.Add(new ColumnSeries<Tuple<string, int>>
            {
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                Values = list,

                Fill = new SolidColorPaint(SKColors.Blue),

                Mapping = (taskItem, point) =>
                {
                    point.PrimaryValue = (int)taskItem.Item2;
                    point.SecondaryValue = point.Context.Index;
                },
                TooltipLabelFormatter = point => $"{point.Model.Item1.ToString()}: {point.PrimaryValue.ToString()}"
            });

            _statisticRepository = new StatisticRepository();

            StartDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;

            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            StartDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnStartDateChange);
            EndDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnEndDateChange);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetProductStatistic(SelectedStartDate.Date, SelectedEndDate.Date);
            var series = new ColumnSeries<Tuple<string, int>>();
            series = (ColumnSeries<Tuple<string, int>>)MonthlyProductSeries.ElementAt(0);
            series.Values = task;
            MonthlyProductSeries.Clear();
            MonthlyProductSeries.Add(series);

            XAxes[0].Name = $"Number of sold books from {SelectedStartDate.Date.ToShortDateString()} to {SelectedEndDate.Date.ToShortDateString()}";
        }

        private void OnStartDateChange(DatePickerValueChangedEventArgs a)
        {
            char seperator = '/';
            int day = 1;
            int month = StartDate.Date.Month;
            int year = StartDate.Date.Year;
            String year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();

            SelectedStartDate = DateTime.Parse(year_month_day);

            if (SelectedStartDate < SelectedEndDate)
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

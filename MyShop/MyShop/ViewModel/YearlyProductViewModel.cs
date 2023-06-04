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
    class YearlyProductViewModel : ViewModelBase
    {
        public ICommand StartDateChangeCommand { get; private set; }
        public ICommand EndDateChangeCommand { get; private set; }

        private DateTime SelectedStartDate;

        private DateTime SelectedEndDate;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        private StatisticRepository _statisticRepository;

        public List<ISeries> YearlyProductSeries { get; set; }

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

        public YearlyProductViewModel()
        {
            YearlyProductSeries = new List<ISeries>();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new Tuple<string, int>($"Book {i}", 1)); // Giá trị mặc định là 0
            }

            YearlyProductSeries.Add(new ColumnSeries<Tuple<string, int>>
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
            StartDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnStartDateChanged);
            EndDateChangeCommand = new RelayCommand<DatePickerValueChangedEventArgs>(OnEndDateChanged);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetProductStatistic(SelectedStartDate.Date, SelectedEndDate.Date);

            var series = new ColumnSeries<Tuple<string, int>>();

            series = (ColumnSeries<Tuple<string, int>>)YearlyProductSeries.ElementAt(0);
            series.Values = task;
            YearlyProductSeries.Clear();
            YearlyProductSeries.Add(series);

            XAxes[0].Name = $"Number of sold books from {SelectedStartDate.Date.ToShortDateString()} to {SelectedEndDate.Date.ToShortDateString()}";

          
            
        }

        private void OnStartDateChanged(DatePickerValueChangedEventArgs e)
        {
            char seperator = '/';
            int day = 1;
            int month = 1;
            int year = StartDate.Date.Year;

            String year_month_day = new StringBuilder().Append(year).Append(seperator).Append(month).Append(seperator).Append(day).ToString();

            SelectedStartDate = DateTime.Parse(year_month_day);

            if (SelectedStartDate < SelectedEndDate)
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

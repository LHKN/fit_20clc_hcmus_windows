using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Microsoft.UI.Xaml.Controls;
using MyShop.Model;
using MyShop.Repository;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;

namespace MyShop.ViewModel
{
    class DailyProductViewModel : ViewModelBase
    {
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public ICommand DateChangeCommand { get; set; }

        private IStatisticRepository _statisticRepository;

        public List<ISeries> DailyProductSeries { get; set; } 

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

        public DailyProductViewModel()
        {
            DailyProductSeries = new List<ISeries>();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new Tuple<string, int>($"Book {i}", 1));
            }

            DailyProductSeries.Add(new ColumnSeries<Tuple<string, int>>
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
            }) ;
            StartDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;
            _statisticRepository = new StatisticRepository();
            DateChangeCommand = new RelayCommand<CalendarDatePickerDateChangedEventArgs>(OnDateChange);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetProductStatistic(StartDate.Date, EndDate.Date);
            var series = new ColumnSeries<Tuple<string, int>>();
           

            series = (ColumnSeries<Tuple<string, int>>)DailyProductSeries.ElementAt(0);
            series.Values = task;
            DailyProductSeries.Clear();
            DailyProductSeries.Add(series);

            XAxes[0].Name = $"Number of sold books from {StartDate.Date.ToShortDateString()} to {EndDate.Date.ToShortDateString()}";

        }

        private void OnDateChange(CalendarDatePickerDateChangedEventArgs args)
        {
            if (StartDate.Date < EndDate.Date)
            {
                DisplayChart();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

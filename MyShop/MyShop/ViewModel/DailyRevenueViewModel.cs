using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Repository;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Windows.Foundation;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using OxyPlot.Series;
using Windows.Services.TargetedContent;
using System.Globalization;
using MyShop.Model;

namespace MyShop.ViewModel
{
    class DailyRevenueViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public DateTimeOffset StartDate { get; set; }

      
        public DateTimeOffset EndDate { get; set; }

        public ICommand DateChangeCommand { get; set; }

        private IStatisticRepository _statisticRepository;

        /* public PlotModel DailyRevenueModel= new PlotModel
         {

             Title = "Daily Revenue",
             PlotAreaBorderColor = OxyColors.Transparent,
             Axes =
             {
                 new LinearAxis { Position = AxisPosition.Bottom, Title="Date"},
                 new LinearAxis { Position = AxisPosition.Left, Title="Revenue" },
             },
             Series =
             {

                 new LineSeries
                 {
                     Title = "Revenue",
                     MarkerType = MarkerType.Circle,
                     Points =
                     {
                         new DataPoint(0, 0),
                         new DataPoint(10, 18),
                         new DataPoint(20, 12),
                         new DataPoint(30, 8),
                         new DataPoint(40, 15),
                     }
                 }
             }

         };*/
        //public PlotModel DailyRevenueModel { get; set; }

        public List<ISeries> DailyRevenueSeries { get; set; } /*=
        {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
        };*/

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Name = "",
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

        public DailyRevenueViewModel()
        {
            DailyRevenueSeries = new List<ISeries>();

            DailyRevenueSeries.Add(new LineSeries<Tuple<DateTime, int>>
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
        }) ;
            StartDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;
            _statisticRepository = new StatisticRepository();
            DateChangeCommand = new RelayCommand<CalendarDatePickerDateChangedEventArgs>(OnDateChange);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetDailyStatistic(StartDate.Date, EndDate.Date);

            var series = new LineSeries<Tuple<DateTime, int>>();
/*            List<int> revenue = new List<int>();
            task.ForEach(taskItem =>
            {
                revenue.Add(taskItem.Item2);
            });*/

            series = (LineSeries<Tuple<DateTime, int>>)DailyRevenueSeries.ElementAt(0);
            series.Values = task;
/*            series.Mapping = (taskItem, point) =>
            {
                point.PrimaryValue = (int)taskItem.Item2;
                //point.SecondaryValue = point.Context.Index;
            };*/
            /*series.TooltipLabelFormatter = point => $"{point.Model.Item1} revenue: {point.PrimaryValue:N2}M";*/

            DailyRevenueSeries.Clear();
            DailyRevenueSeries.Add(series);

            XAxes[0].Name = $"Revenue from {StartDate.Date.ToShortDateString()} to {EndDate.Date.ToShortDateString()}";

        }

        private void OnDateChange(CalendarDatePickerDateChangedEventArgs args)
        {
            if (StartDate.Date < EndDate.Date)
            {
                DisplayChart();
            }
        }

        /*private LineSeries fromTupleToSeries(List<Tuple<DateTime, int>> tuples)
        {
            if (tuples == null)
            {
                return null;
            }

            var series = new LineSeries()
            {
                MarkerType = MarkerType.Circle,

            };


            for (int i = 0; i < tuples.Count; i++)
            {
                series.Points.Add(new DataPoint(i + 1, tuples[i].Item2));
            }

            return series;
        }*/

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}


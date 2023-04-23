using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Repository;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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

namespace MyShop.ViewModel
{
    class DailyRevenueViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public DateTimeOffset StartDate { get; set; }


        public DateTimeOffset EndDate { get; set; }

        public ICommand DateChangeCommand { get; set; }

        private StatisticRepository _statisticRepository;

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
        public PlotModel DailyRevenueModel { get; set; }

        public DailyRevenueViewModel()
        {
            DailyRevenueModel = new PlotModel
            {

                Title = "Daily Revenue",
                PlotAreaBorderColor = OxyColors.Transparent,
                Axes =
                {
                    new LinearAxis { Position = AxisPosition.Bottom, Title = "Date",},
                    new LinearAxis { Position = AxisPosition.Left, Title = "Revenue" },
                },
            };
            StartDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;
            _statisticRepository = new StatisticRepository();
            DateChangeCommand = new RelayCommand<CalendarDatePickerDateChangedEventArgs>(OnDateChange);
        }

        private async void DisplayChart()
        {
            var task = await _statisticRepository.GetDailyStatistic(StartDate.Date, EndDate.Date);
            var series = new LineSeries()
            {
                MarkerType = MarkerType.Circle,

            };


            for (int i = 0; i < task.Count; i++)
            {
                series.Points.Add(new DataPoint(i + 1, task[i].Item2));
            }

            DailyRevenueModel.Series.Clear();
            DailyRevenueModel.Series.Add(series);
            /* LineSeries series = fromTupleToSeries(task);
             DailyRevenueModel.Series.Add(series);*/
            /* DailyRevenueModel.Axes.Clear();
             DailyRevenueModel.Axes.Add(new LinearAxis { Maximum = 50000, Position = AxisPosition.Bottom, Title= "Date" });
             DailyRevenueModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left, Title= "Revenue" });*/
            DailyRevenueModel.InvalidatePlot(true);

        }

        private void OnDateChange(CalendarDatePickerDateChangedEventArgs args)
        {
            if (StartDate.Date < EndDate.Date)
            {
                DisplayChart();
            }
        }

        private LineSeries fromTupleToSeries(List<Tuple<DateTime, int>> tuples)
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
        }

    }
}


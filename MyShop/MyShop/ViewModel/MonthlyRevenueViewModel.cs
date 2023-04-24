using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using MyShop.Repository;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
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

        public PlotModel MonthlyRevenueModel { get; private set; }

        DateTime SelectedStartDate;
        DateTime SelectedEndDate;

        public MonthlyRevenueViewModel()
        {
            MonthlyRevenueModel = new PlotModel()
            {
                Title = "Monthly Revenue",
                PlotAreaBorderColor = OxyColors.Transparent,
                Axes =
                {
                    new LinearAxis { Position = AxisPosition.Bottom, Title = "Month",},
                    new LinearAxis { Position = AxisPosition.Left, Title = "Revenue" },
                },
            };

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
            var series = new LineSeries()
            {
                MarkerType = MarkerType.Circle,

            };


            for (int i = 0; i < task.Count; i++)
            {
                series.Points.Add(new DataPoint(i + 1, task[i].Item2));
            }

            MonthlyRevenueModel.Series.Clear();
            MonthlyRevenueModel.Series.Add(series);
            MonthlyRevenueModel.InvalidatePlot(true);
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

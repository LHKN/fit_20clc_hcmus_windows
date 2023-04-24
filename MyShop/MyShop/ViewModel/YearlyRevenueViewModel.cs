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
using System.Windows.Input;

namespace MyShop.ViewModel
{
    class YearlyRevenueViewModel
    {
        //public DateTime
        public PlotModel YearlyRevenueModel { get; private set; }

        public ICommand StartDateChangeCommand { get; private set; }
        public ICommand EndDateChangeCommand { get; private set; }

        private DateTime SelectedStartDate;

        private DateTime SelectedEndDate;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        private StatisticRepository _statisticRepository;

        public YearlyRevenueViewModel()
        {
            YearlyRevenueModel = new PlotModel()
            {
                Title = "Monthly Revenue",
                PlotAreaBorderColor = OxyColors.Transparent,
                Axes =
                {
                    new LinearAxis { Position = AxisPosition.Bottom, Title = "Month",},
                    new LinearAxis { Position = AxisPosition.Left, Title = "Revenue" },
                },
            };

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
            var task = await _statisticRepository.GetYearlyStatistic(StartDate.Date, EndDate.Date);
            var series = new LineSeries()
            {
                MarkerType = MarkerType.Circle,

            };

            for (int i = 0; i < task.Count; i++)
            {
                series.Points.Add(new DataPoint(i + 1, task[i].Item2));
            }

            YearlyRevenueModel.Series.Clear();
            YearlyRevenueModel.Series.Add(series);
            YearlyRevenueModel.InvalidatePlot(true);
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

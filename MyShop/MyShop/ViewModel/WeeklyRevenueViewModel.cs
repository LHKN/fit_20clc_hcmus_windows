using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Model;
using MyShop.Repository;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShop.ViewModel
{


    class WeeklyRevenueViewModel : ViewModelBase
    {

        public ObservableCollection<Tuple<int, DateTime>> ListOfWeeks { get; private set; }

        private IStatisticRepository _statisticRepository;

        public PlotModel WeeklyRevenueModel { get; private set; }

        public ICommand Load_page { get; set; }

        public ICommand OnSelectionChangedOfStartDate { get; set; }
        public ICommand OnSelectionChangedOfEndDate { get; set; }

        public int SelectedIndex_StartDate { get; set; }

        public int SelectedIndex_EndDate { get; set; }

        public WeeklyRevenueViewModel()
        {
            _statisticRepository= new StatisticRepository();
            WeeklyRevenueModel = new PlotModel
            {
                Title = "Daily Revenue",
                PlotAreaBorderColor = OxyColors.Transparent,
                Axes =
                {
                    new LinearAxis { Position = AxisPosition.Bottom, Title = "Date",},
                    new LinearAxis { Position = AxisPosition.Left, Title = "Revenue" },
                },
            };
            SelectedIndex_StartDate = 0;
            SelectedIndex_EndDate = 0;
            ListOfWeeks = new ObservableCollection<Tuple<int, DateTime>>();
            Load_page = new RelayCommand<RoutedEventArgs>(Load_ListOfWeeks);
            OnSelectionChangedOfStartDate = new RelayCommand<SelectionChangedEventArgs>(SelectionChangedOfStartDate);
            OnSelectionChangedOfEndDate = new RelayCommand<SelectionChangedEventArgs>(SelectionChangedOfEndDate);
        }


        private async void DisplayChart()
        {
            DateTime startDate = ListOfWeeks[SelectedIndex_StartDate].Item2;
            DateTime endDate = ListOfWeeks[SelectedIndex_EndDate].Item2;

            var task = await _statisticRepository.GetWeeklyStatistic(startDate, endDate);
            var series = new LineSeries()
            {
                MarkerType = MarkerType.Circle,

            };

            for (int i = 0; i < task.Count; i++)
            {
                series.Points.Add(new DataPoint(i + 1, task[i].Item2));
            }

            WeeklyRevenueModel.Series.Clear();
            WeeklyRevenueModel.Series.Add(series);
            WeeklyRevenueModel.InvalidatePlot(true);
        }

        private async void Load_ListOfWeeks(RoutedEventArgs e)
        {
            var task = await _statisticRepository.GetListOfWeeks();
            ListOfWeeks.Clear();

            task.ForEach(taskItem =>
            {
                ListOfWeeks.Add(taskItem);
            });
        }

        private void SelectionChangedOfStartDate(SelectionChangedEventArgs e)
        {
/*            MessageBox.Show("Selected Item: " + ListOfWeeks[SelectedIndex_StartDate].Item2);*/
            if(SelectedIndex_StartDate < SelectedIndex_EndDate)
            {
                DisplayChart();
            }
        }

        private void SelectionChangedOfEndDate(SelectionChangedEventArgs e)
        {
/*            MessageBox.Show("Selected Item: " + ListOfWeeks[SelectedIndex_EndDate].Item2);*/
            if (SelectedIndex_StartDate < SelectedIndex_EndDate)
            {
                DisplayChart();
            }
        }
    }
}

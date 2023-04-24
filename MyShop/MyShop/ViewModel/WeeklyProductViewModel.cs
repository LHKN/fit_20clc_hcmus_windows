using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using MyShop.Model;
using MyShop.Repository;
using OxyPlot.Axes;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore;
using SkiaSharp;
using OxyPlot.Series;
using LiveChartsCore.SkiaSharpView;

namespace MyShop.ViewModel
{
    class WeeklyProductViewModel : ViewModelBase
    {
        public ObservableCollection<Tuple<int, DateTime>> ListOfWeeks { get; private set; }

        private IStatisticRepository _statisticRepository;

        public PlotModel WeeklyRevenueModel { get; private set; }

        public ICommand Load_page { get; set; }

        public ICommand OnSelectionChangedOfStartDate { get; set; }
        public ICommand OnSelectionChangedOfEndDate { get; set; }

        public int SelectedIndex_StartDate { get; set; }

        public int SelectedIndex_EndDate { get; set; }

        public IEnumerable<ISeries> TopBestSellerSeries { get; set; }

        private ObservableCollection<Tuple<string, int>> data;

        public LabelVisual TopBestSellerTitle { get; set; } =
            new LabelVisual
            {
                Text = "My chart title",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

        public WeeklyProductViewModel()
        {

            _statisticRepository = new StatisticRepository();

            /*TopBestSellerSeries = new List<ISeries> {
                new PieSeries<int> { Values = new List<int>{10}, Name = "Top books sold" },
                new PieSeries<int> { Values = new List<int>{20}, Name = "Top books sold" },
               
            };*/

            data =new ObservableCollection<Tuple<string, int>>()
            {
                new Tuple<string,int>("Book 1",10),
                new Tuple<string,int>("Book 2",20),
                new Tuple<string,int>("Book 3",20),
                new Tuple<string,int>("Book 4",20),
                new Tuple<string,int>("Book 5",20),
                new Tuple<string,int>("Book 6",20),
                new Tuple<string,int>("Book 7",20),
                new Tuple<string,int>("Book 8",20),
            };
            
            TopBestSellerSeries = data.AsLiveChartsPieSeries((value, series) =>
            {
                // here you can configure the series assigned to each value.
                series.Name = $"{value.Item1}";
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
                series.DataLabelsSize = 10;
                series.DataLabelsRotation = LiveCharts.TangentAngle + 90;
                series.Mapping = (value, p) => {
                    p.PrimaryValue = value.Item2;
                    
                };
                series.DataLabelsFormatter = p => $"{p.StackedValue.Share:P2}";
            });

            



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
            /*if (SelectedIndex_StartDate < SelectedIndex_EndDate)
            {
                DisplayChart();
            }*/
            data.RemoveAt(0);
        }

        private void SelectionChangedOfEndDate(SelectionChangedEventArgs e)
        {
            /*            MessageBox.Show("Selected Item: " + ListOfWeeks[SelectedIndex_EndDate].Item2);*/
            /*if (SelectedIndex_StartDate < SelectedIndex_EndDate)
            {
                DisplayChart();
            }*/
        }
    }
}

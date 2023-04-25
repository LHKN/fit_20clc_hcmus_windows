using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Model;
using MyShop.Repository;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        public List<ISeries> WeeklyRevenueSeries { get; private set; }

        public ICommand Load_page { get; set; }

        public ICommand OnSelectionChangedOfStartDate { get; set; }
        public ICommand OnSelectionChangedOfEndDate { get; set; }

        public int SelectedIndex_StartDate { get; set; }

        public int SelectedIndex_EndDate { get; set; }

        public Axis[] XAxes { get; set; } =
       {
            new Axis
            {
                Name = "Weeks",
                // Use the labels property for named or static labels 
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

        public WeeklyRevenueViewModel()
        {
            _statisticRepository= new StatisticRepository();
            WeeklyRevenueSeries = new List<ISeries>();

            WeeklyRevenueSeries.Add(new LineSeries<Tuple<DateTime, int>>
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

            var task = await _statisticRepository.GetWeeklyStatistic(startDate.Date, endDate.Date);

            var series = new LineSeries<Tuple<DateTime, int>>();

            series = (LineSeries<Tuple<DateTime, int>>)WeeklyRevenueSeries.ElementAt(0);
            series.Values = task;
          
            WeeklyRevenueSeries.Clear();
            WeeklyRevenueSeries.Add(series);

            XAxes[0].Name = $"Revenue from {startDate.Date.ToShortDateString()} to {endDate.Date.ToShortDateString()}";

            XAxes[0].Labels = null;
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

using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using MyShop.Model;
using MyShop.Repository;
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
using LiveChartsCore.SkiaSharpView;

namespace MyShop.ViewModel
{
    class WeeklyProductViewModel : ViewModelBase
    {
        public ObservableCollection<Tuple<int, DateTime>> ListOfWeeks { get; private set; }

        private IStatisticRepository _statisticRepository;

        public ICommand Load_page { get; set; }

        public ICommand OnSelectionChangedOfStartDate { get; set; }
        public ICommand OnSelectionChangedOfEndDate { get; set; }

        public int SelectedIndex_StartDate { get; set; }

        public int SelectedIndex_EndDate { get; set; }

        public List<ISeries> WeeklyProductSeries { get; set; }

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

        public WeeklyProductViewModel()
        {
            WeeklyProductSeries = new List<ISeries>();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new Tuple<string, int>($"Book {i}", 1)); 
            }

            WeeklyProductSeries.Add(new ColumnSeries<Tuple<string, int>>
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

            var task = await _statisticRepository.GetProductStatistic(startDate.Date, endDate.Date);
            var series = new ColumnSeries<Tuple<string, int>>();

            series = (ColumnSeries<Tuple<string, int>>)WeeklyProductSeries.ElementAt(0);
            series.Values = task;
            WeeklyProductSeries.Clear();
            WeeklyProductSeries.Add(series);

            XAxes[0].Name = $"Number of sold books from week {ListOfWeeks[SelectedIndex_StartDate].Item1.ToString()} to week {ListOfWeeks[SelectedIndex_EndDate].Item1.ToString()}";


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
            if (SelectedIndex_StartDate < SelectedIndex_EndDate)
            {
                DisplayChart();
            }
        }

        private void SelectionChangedOfEndDate(SelectionChangedEventArgs e)
        {
            if (SelectedIndex_StartDate < SelectedIndex_EndDate)
            {
                DisplayChart();
            }
        }
    }
}
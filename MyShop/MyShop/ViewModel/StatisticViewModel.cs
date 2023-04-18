using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using MyShop.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    class StatisticViewModel : ViewModelBase
    {
        public StatisticViewModel()
        {
            RevenueChildPageNavigation = new PageNavigation(new DailyRevenueViewModel());
            ProductChildPageNavigation = new PageNavigation(new DailyProductViewModel());
        }
        private ICommand _revenueItemInvokedCommand;
        private ICommand _productItemInvokedCommand;
        public ICommand RevenueItemInvokedCommand => _revenueItemInvokedCommand ?? (_revenueItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnRevenueItemInvoked));
        public ICommand ProductItemInvokedCommand => _productItemInvokedCommand ?? (_productItemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnProductItemInvoked));

        private void OnRevenueItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem.ToString().Equals("Daily"))
            {
                RevenueChildPageNavigation.ViewModel = new DailyRevenueViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Weekly"))
            {
                RevenueChildPageNavigation.ViewModel = new WeeklyRevenueViewModel();
            } 
            else if (args.InvokedItem.ToString().Equals("Monthly"))
            {
                RevenueChildPageNavigation.ViewModel = new MonthlyRevenueViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Yearly"))
            {
                RevenueChildPageNavigation.ViewModel = new YearlyProductViewModel();
            }

        }
        private void OnProductItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem.ToString().Equals("Daily"))
            {
                ProductChildPageNavigation.ViewModel = new DailyProductViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Weekly"))
            {
                ProductChildPageNavigation.ViewModel = new WeeklyProductViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Monthly"))
            {
                ProductChildPageNavigation.ViewModel = new MonthlyProductViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Yearly"))
            {
                ProductChildPageNavigation.ViewModel = new YearlyProductViewModel();
            }
        }
        public PageNavigation RevenueChildPageNavigation { get; set; }
        public PageNavigation ProductChildPageNavigation { get; set; }
    }

}

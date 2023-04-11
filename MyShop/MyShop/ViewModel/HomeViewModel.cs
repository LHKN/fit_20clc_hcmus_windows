using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel() {
            ChildPageNavigation = new PageNavigation(new DashboardViewModel());
        }

        private ICommand _itemInvokedCommand;
        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            // could also use a converter on the command parameter if you don't like
            // the idea of passing in a NavigationViewItemInvokedEventArgs
            if (args.InvokedItem.ToString().Equals("Dashboard"))
            {
                ChildPageNavigation.ViewModel = new DashboardViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Statistic")) 
            {
                ChildPageNavigation.ViewModel = new StatisticViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Book")) 
            {
                ChildPageNavigation.ViewModel = new BookViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Statistic")) 
            {
                ChildPageNavigation.ViewModel = new StatisticViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Order History")) 
            {
                ChildPageNavigation.ViewModel = new OrderHistoryViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Account")) 
            {
                ChildPageNavigation.ViewModel = new AccountViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Settings"))
            {
                ChildPageNavigation.ViewModel = new SettingViewModel();
            }

        }
        public PageNavigation ChildPageNavigation { get; set; }
    }
}

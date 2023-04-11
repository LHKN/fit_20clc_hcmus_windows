using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        public BookViewModel()
        {
            ChildPageNavigation = new PageNavigation(new BooksViewModel());
        }
        private ICommand _itemInvokedCommand;
        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        private void OnItemInvoked(NavigationViewItemInvokedEventArgs args)
        {
            // could also use a converter on the command parameter if you don't like
            // the idea of passing in a NavigationViewItemInvokedEventArgs
            if (args.InvokedItem.ToString().Equals("Books"))
            {
                ChildPageNavigation.ViewModel = new BooksViewModel();
            }
            else if (args.InvokedItem.ToString().Equals("Book Types"))
            {
                ChildPageNavigation.ViewModel = new BookTypeViewModel();
            }

        }
        public PageNavigation ChildPageNavigation { get; set; }
    }
}

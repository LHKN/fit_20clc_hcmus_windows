using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class RootPageViewModel : ObservableObject
    {
        public RootPageViewModel()
        {
            ChildPageNavigation = new PageNavigation(new LoginDatabaseViewModel());
        }
        public PageNavigation ChildPageNavigation { get; }
    }
}

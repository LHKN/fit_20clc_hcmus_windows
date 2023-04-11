    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel() {
            ChildPageNavigation = new PageNavigation(new DashboardViewModel());
        }
        public PageNavigation ChildPageNavigation { get; set; }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class ViewModelBase : ObservableObject
    {
        public PageNavigation ParentPageNavigation { get; set; }

    }
}

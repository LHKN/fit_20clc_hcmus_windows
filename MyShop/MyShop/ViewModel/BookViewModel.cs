using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        public BookViewModel()
        {
            ChildPageNavigation = new PageNavigation(new BooksViewModel());
        }
        public PageNavigation ChildPageNavigation { get; set; }
    }
}

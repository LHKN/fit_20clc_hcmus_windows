using Microsoft.UI.Xaml.Data;
using MyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.ViewModel;

namespace MyShop.Services
{
    public class BookIdToBookNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int bookId = (int)value;
            string bookName = getBookName(bookId);
            return bookName;
        }

        private string getBookName(int bookId)
        {
            BooksViewModel viewModel = new BooksViewModel();
            var task = viewModel.getBookName(bookId);
            string bookName = task.Result;
            return bookName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class BooksViewModel: ViewModelBase
    {

        public BooksViewModel()
        {
            EditBookCommand = new RelayCommand(ExecuteEditBookCommand);
            AddBookCommand = new RelayCommand(ExecuteAddBookCommand);
            DeleteBookCommand = new RelayCommand(ExecuteDeleteBookCommand);

        }
        private ICommand _editBookCommand { get; }
        private ICommand _deleteBookCommand { get; }
        private ICommand _addBookCommand { get; }

        public ICommand EditBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand AddBookCommand { get; }

        public void ExecuteEditBookCommand()
        {

        }

        public void ExecuteDeleteBookCommand()
        {

        }

        public void ExecuteAddBookCommand()
        {

        }



    }
}

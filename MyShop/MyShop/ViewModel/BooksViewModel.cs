using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class BooksViewModel: ViewModelBase
    {

        private ObservableCollection<Book> _books;
        private IBookRepository _bookRepository;
        public BooksViewModel()
        {
            _bookRepository = new BookRepository();
            ExecuteGetAllCommand();
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
        public ObservableCollection<Book> Books { get => _books; set => _books = value; }

        public void ExecuteEditBookCommand()
        {

        }

        public void ExecuteDeleteBookCommand()
        {

        }

        public void ExecuteAddBookCommand()
        {

        }
        
        public async void ExecuteGetAllCommand()
        {
            var task = await _bookRepository.GetAll();
            Books = task;
        }



    }
}

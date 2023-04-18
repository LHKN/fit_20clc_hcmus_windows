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
        private Book _selectedBook;
        public BooksViewModel()
        {
            _bookRepository = new BookRepository();
            ExecuteGetAllCommand();
            EditBookCommand = new RelayCommand(ExecuteEditBookCommand);
            AddBookCommand = new RelayCommand(ExecuteAddBookCommand);
            DeleteBookCommand = new RelayCommand(ExecuteDeleteBookCommand);

        }
        private RelayCommand _editBookCommand;
        private RelayCommand _deleteBookCommand;
        private RelayCommand _addBookCommand;

        public ObservableCollection<Book> Books { get => _books; set => _books = value; }
        public Book SelectedBook { get => _selectedBook; set => _selectedBook = value; }
        public RelayCommand EditBookCommand { get => _editBookCommand; set => _editBookCommand = value; }
        public RelayCommand DeleteBookCommand { get => _deleteBookCommand; set => _deleteBookCommand = value; }
        public RelayCommand AddBookCommand { get => _addBookCommand; set => _addBookCommand = value; }

        public void ExecuteEditBookCommand()
        {
            ParentPageNavigation.ViewModel = new EditBookViewModel(SelectedBook);
        }

        public async void ExecuteDeleteBookCommand()
        {
            //Message to user to delete or not
            {

            }

            //Delete
            {
                var task = await _bookRepository.Remove(SelectedBook.Id);
                if (task)
                {
                    //Success message
                }
                else
                {
                    //Failed message
                }
            }
            
        }

        public void ExecuteAddBookCommand()
        {
            ParentPageNavigation.ViewModel = new AddBookViewModel();
        }
        
        public async void ExecuteGetAllCommand()
        {
            var task = await _bookRepository.GetAll();
            Books = task;
        }



    }
}

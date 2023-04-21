using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using MyShop.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class BooksViewModel: ViewModelBase
    {
        private List<Book> _booksList;
        private List<Book> _displayBooksList;
        private List<Book> _resultBooksList;
        private ObservableCollection<Book> _displayBookCollection;
        private List<Genre> _genres;
        private IBookRepository _bookRepository;
        private Book _selectedBook;
        private string _paginationMessage;
        private int _currentPage;
        private int _itemsPerPage; //can be changed through setting
        private int _totalItems;
        private int _totalPages;
        private int _startPrice;
        private int _endPrice;
        private int _genreId;
        private string _currentKeyword = String.Empty;
        public BooksViewModel()
        {
            _bookRepository = new BookRepository();
            DisplayBooksList = new List<Book>();
            ResultBooksList = new List<Book>();
            DisplayBookCollection = new ObservableCollection<Book>();
            //Paging info
            {
                CurrentPage = 1;
                ItemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
                StartPrice = 100000;
                EndPrice = 500000;
                GenreId = 0;
            }
            ExecuteGetAllCommand();
            EditBookCommand = new RelayCommand(ExecuteEditBookCommand);
            AddBookCommand = new RelayCommand(ExecuteAddBookCommand);
            DeleteBookCommand = new RelayCommand(ExecuteDeleteBookCommand);
            GoToNextPageCommand = new RelayCommand(ExecuteGoToNextPageCommand);
            GoToPreviousPageCommand = new RelayCommand(ExecuteGoToPreviousPageCommand);
            SearchCommand = new RelayCommand<string>(ExecuteSearchCommand);

        }

        private RelayCommand _editBookCommand;
        private RelayCommand _deleteBookCommand;
        private RelayCommand _addBookCommand;
        private RelayCommand _goToPreviousPageCommand;
        private RelayCommand _goToNextPageCommand;
        private RelayCommand<string> _searchCommand;

        public Book SelectedBook { get => _selectedBook; set => _selectedBook = value; }
        public RelayCommand EditBookCommand { get => _editBookCommand; set => _editBookCommand = value; }
        public RelayCommand DeleteBookCommand { get => _deleteBookCommand; set => _deleteBookCommand = value; }
        public RelayCommand AddBookCommand { get => _addBookCommand; set => _addBookCommand = value; }
        public RelayCommand GoToPreviousPageCommand { get => _goToPreviousPageCommand; set => _goToPreviousPageCommand = value; }
        public RelayCommand GoToNextPageCommand { get => _goToNextPageCommand; set => _goToNextPageCommand = value; }
        public string PaginationMessage { get => _paginationMessage; set => _paginationMessage = value; }
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }
        public int ItemsPerPage { get => _itemsPerPage; set => _itemsPerPage = value; }
        public int TotalItems { get => _totalItems; set => _totalItems = value; }
        public int TotalPages { get => _totalPages; set => _totalPages = value; }
        public List<Book> DisplayBooksList { get => _displayBooksList; set => _displayBooksList = value; }
        public List<Book> BooksList { get => _booksList; set => _booksList = value; }
        public int StartPrice { get => _startPrice; set => _startPrice = value; }
        public int EndPrice { get => _endPrice; set => _endPrice = value; }
        public int GenreId { get => _genreId; set => _genreId = value; }
        public string CurrentKeyword { get => _currentKeyword; set => _currentKeyword = value; }
        public ObservableCollection<Book> DisplayBookCollection { get => _displayBookCollection; set => _displayBookCollection = value; }
        public List<Genre> Genres { get => _genres; set => _genres = value; }
        public RelayCommand<string> SearchCommand { get => _searchCommand; set => _searchCommand = value; }
        public List<Book> ResultBooksList { get => _resultBooksList; set => _resultBooksList = value; }
        public async void ExecuteEditBookCommand()
        {
            if (SelectedBook == null)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            ParentPageNavigation.ViewModel = new EditBookViewModel(SelectedBook);
        }

        public async void ExecuteDeleteBookCommand()
        {
            if (SelectedBook == null)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            var confirmed = await App.MainRoot.ShowYesCancelDialog("Delete this item?", "Delete", "Cancel");

            if (confirmed == true)
            {
                var task = await _bookRepository.Remove(SelectedBook.Id);
                if (task)
                {
                    await App.MainRoot.ShowDialog("Success", "Book is removed!");
                }
                else
                {
                    await App.MainRoot.ShowDialog("Failure", "Removal unsuccessful...");
                }
            }
            UpdateDataSource();
            UpdatePagingInfo();
            
        }

        public void ExecuteAddBookCommand()
        {
            ParentPageNavigation.ViewModel = new AddBookViewModel();
        }
        
        public async void ExecuteGetAllCommand()
        {
            BooksList = await _bookRepository.GetAll();
            BooksList.ForEach(item => ResultBooksList.Add(item));
            Genres = await _bookRepository.GetGenres();
            TotalItems = BooksList.Count;

            UpdateDataSource();
            UpdatePagingInfo();
        }

        public void ExecuteGoToNextPageCommand()
        {
            if (CanExecuteGoToNextPageCommand()) CurrentPage += 1;
            UpdateDataSource();
            UpdatePagingInfo();
        }

        public void ExecuteGoToPreviousPageCommand()
        {
            if (CanExecuteGoToPreviousCommand()) CurrentPage -= 1;
            UpdateDataSource();
            UpdatePagingInfo();
        }

        public bool CanExecuteGoToNextPageCommand() { return CurrentPage < TotalPages; }
        public bool CanExecuteGoToPreviousCommand() { return CurrentPage > 1; }

        public void UpdatePagingInfo()
        {
            TotalPages = TotalItems / ItemsPerPage +
                  (TotalItems % ItemsPerPage == 0 ? 0 : 1);
            PaginationMessage = $"{DisplayBooksList.Count}/{TotalItems} books";
        }

        public void UpdateDataSource()
        {
            DisplayBookCollection.Clear();
           
            DisplayBooksList = ResultBooksList.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
            DisplayBooksList.ForEach(x => DisplayBookCollection.Add(x));
            
        }
        private void ExecuteSearchCommand(string keyword)
        {
            CurrentPage = 1;
            ResultBooksList = _bookRepository.Filter(BooksList, StartPrice, EndPrice, CurrentKeyword, GenreId);
            UpdateDataSource();
            TotalItems = ResultBooksList.Count;
            UpdatePagingInfo();
        }
    }
}

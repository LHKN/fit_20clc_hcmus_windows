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
    public class BookTypeViewModel : ViewModelBase
    {
        private List<Genre> _genres;
        private ObservableCollection<Genre> _displayGenresCollection;
        private IBookRepository _bookRepository;
        private Genre _selectedGenre;

        public BookTypeViewModel()
        {
            _bookRepository = new BookRepository();
            DisplayGenresCollection = new ObservableCollection<Genre>();
            PageLoaded();

            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            SaveCommand = new RelayCommand(ExecuteSaveCommand);
        }

        private async void ExecuteSaveCommand()
        {
            var task = await _bookRepository.EditGenre(SelectedGenre);
            UpdateDataSource();
        }

        private async void ExecuteDeleteCommand()
        {
            var task = await _bookRepository.RemoveGenre(SelectedGenre.Id);
            UpdateDataSource();
        }

        private async void ExecuteAddCommand()
        {
            int nextId = Genres.Count + 1;
            var task = await _bookRepository.AddGenre(new Genre { Id = nextId, Name = "New Genre" }) ;
            UpdateDataSource();

        }

        public void PageLoaded()
        {
            UpdateDataSource();
        }

        public async void UpdateDataSource()
        {
            Genres = await _bookRepository.GetGenres();
            DisplayGenresCollection.Clear();
            Genres.ForEach(genre => DisplayGenresCollection.Add(genre));
        }

        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        public List<Genre> Genres { get => _genres; set => _genres = value; }
        public ObservableCollection<Genre> DisplayGenresCollection { get => _displayGenresCollection; set => _displayGenresCollection = value; }
        public IBookRepository BookRepository { get => _bookRepository; set => _bookRepository = value; }
        public Genre SelectedGenre { get => _selectedGenre; set => _selectedGenre = value; }
        public RelayCommand AddCommand { get => _addCommand; set => _addCommand = value; }
        public RelayCommand DeleteCommand { get => _deleteCommand; set => _deleteCommand = value; }
        public RelayCommand SaveCommand { get => _saveCommand; set => _saveCommand = value; }
    }
}

using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.Storage;

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
            ImportByExcelCommand = new RelayCommand(ExecuteImportByExcelCommand);
            ImportByAccessCommand = new RelayCommand(ExecuteImportByAccessCommand);
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
            var task = await _bookRepository.AddGenre(new Genre { Name = "New Genre" }) ;
            UpdateDataSource();

        }

        private async void ExecuteImportByAccessCommand()
        {
            await App.MainRoot.ShowDialog("Warning", "This action can change the database");
            var window = new Microsoft.UI.Xaml.Window();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            filePicker.FileTypeFilter.Add(".accdb");
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                //Read data from Excel file
                Genres = await new ImportDatabaseService().ReadBookGenreFromExcelFile(file);
                UpdateDataSource();
            }

        }

        private async void ExecuteImportByExcelCommand()
        {
            await App.MainRoot.ShowDialog("Warning", "This action can change the database");
            var window = new Microsoft.UI.Xaml.Window();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            filePicker.FileTypeFilter.Add(".xlsx");
            filePicker.FileTypeFilter.Add(".xls");
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                //Read data from Excel file
                Genres = await new ImportDatabaseService().ReadBookGenreFromExcelFile(file);
                UpdateDataSource();
            }

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
        private RelayCommand _importByExcelCommand;
        private RelayCommand _importByAccessCommand;
        public List<Genre> Genres { get => _genres; set => _genres = value; }
        public ObservableCollection<Genre> DisplayGenresCollection { get => _displayGenresCollection; set => _displayGenresCollection = value; }
        public IBookRepository BookRepository { get => _bookRepository; set => _bookRepository = value; }
        public Genre SelectedGenre { get => _selectedGenre; set => _selectedGenre = value; }
        public RelayCommand AddCommand { get => _addCommand; set => _addCommand = value; }
        public RelayCommand DeleteCommand { get => _deleteCommand; set => _deleteCommand = value; }
        public RelayCommand SaveCommand { get => _saveCommand; set => _saveCommand = value; }
        public RelayCommand ImportByExcelCommand { get => _importByExcelCommand; set => _importByExcelCommand = value; }
        public RelayCommand ImportByAccessCommand { get => _importByAccessCommand; set => _importByAccessCommand = value; }
    }
}

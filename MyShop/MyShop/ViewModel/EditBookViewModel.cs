using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using MyShop.Model;
using MyShop.Repository;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System;
using MyShop.Services;

namespace MyShop.ViewModel
{
    public class EditBookViewModel : ViewModelBase
    {
        private IBookRepository _bookRepository;
        private Book _currentBook;
        private List<Genre> _genres;
        private BitmapImage _bookImageBitmap;
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        private RelayCommand _browseCommand;
        private FileInfo _selectedImage;
        private string _errorMessage;

        public EditBookViewModel(Book currentBook)
        {
            _bookRepository = new BookRepository();
            //Get the book clone instance
            CurrentBook = currentBook;
            //Loaded
            PageLoaded();
            BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);
        }

        public async void PageLoaded()
        {
            Genres = await _bookRepository.GetGenres();
        }

        public async void ExecuteConfirmCommand()
        {
            var task = await _bookRepository.Edit(CurrentBook);
            if (task)
            {
                ParentPageNavigation.ViewModel = new BooksViewModel();
            }
            else
            {
                ErrorMessage = "* Task failed!";
            }

            await App.MainRoot.ShowDialog("Success", "Book is updated!");
        }
        public void ExecuteBackCommand()
        {
            ParentPageNavigation.ViewModel = new BooksViewModel();
        }

        public void ExecuteBrowseCommand()
        {
            var screen = new OpenFileDialog();
            screen.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";
            if (screen.ShowDialog() == DialogResult.OK)
            {
                _selectedImage = new FileInfo(screen.FileName);
                BookImageBitmap = new BitmapImage();
                BookImageBitmap.UriSource = new Uri(screen.FileName, UriKind.Absolute);
            }

            if (_selectedImage == null) { ErrorMessage = "* Invalid book cover image"; return; };
            Random rng = new Random();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            string newPathAbsolute = $"{folder}Assets\\{_selectedImage.Name}";
            string relativePath = $"Assets\\{_selectedImage.Name}";


            if (File.Exists(newPathAbsolute))
            {   
                newPathAbsolute = $"{folder}Assets\\{rng.Next()}{_selectedImage.Name}";
            }
            File.Copy(_selectedImage.FullName, newPathAbsolute);
            CurrentBook.Image = relativePath;
        }

        public RelayCommand BackCommand { get => _backCommand; set => _backCommand = value; }
        public RelayCommand ConfirmCommand { get => _confirmCommand; set => _confirmCommand = value; }
        public Book CurrentBook { get => _currentBook; set => _currentBook = value; }
        public RelayCommand BrowseCommand { get => _browseCommand; set => _browseCommand = value; }
        public BitmapImage BookImageBitmap { get => _bookImageBitmap; set => _bookImageBitmap = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public List<Genre> Genres { get => _genres; set => _genres = value; }
    }
}
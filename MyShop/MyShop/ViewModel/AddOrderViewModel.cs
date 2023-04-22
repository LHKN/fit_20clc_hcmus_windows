using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using MyShop.Model;
using MyShop.Repository;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyShop.ViewModel
{
    public class AddOrderViewModel : ViewModelBase
    {
        // Fields
        private ObservableCollection<BillDetail> _billDetailList; //int <<billId>> respective to the bill's list of <<billDetail>>

        private IBillRepository _billRepository;
        private IBookRepository _bookRepository;
        private IAccountRepository _accountRepository;

        private Bill _newBill;

        private ObservableCollection<Book> _books;
        private ObservableCollection<Account> _customers;

        private Account _selectedCustomer;
        private BillDetail _selectedBillDetail;

        // test
        private Book _selectedBook;

        // getter, setter
        public RelayCommand BackCommand { get => _backCommand; set => _backCommand = value; }
        public RelayCommand ConfirmCommand { get => _confirmCommand; set => _confirmCommand = value; }
        //public RelayCommand BrowseCommand { get => _browseCommand; set => _browseCommand = value; }
        public RelayCommand AddCommand { get => _addCommand; set => _addCommand = value; }
        public RelayCommand DeleteCommand { get => _deleteCommand; set => _deleteCommand = value; }

        public Bill NewBill { get => _newBill; set => _newBill = value; }
        public ObservableCollection<Book> Books { get => _books; set => _books = value; }
        public ObservableCollection<BillDetail> BillDetailList { get => _billDetailList; set => _billDetailList = value; }
        public ObservableCollection<Account> Customers { get => _customers; set => _customers = value; }
        public Account SelectedCustomer { get => _selectedCustomer; set => _selectedCustomer = value; }
        public BillDetail SelectedBillDetail { get => _selectedBillDetail; set => _selectedBillDetail = value; }
        public Book SelectedBook 
        { 
            get => _selectedBook;
            set
            {
                _selectedBook = value;

                SelectedBillDetail.BookId = SelectedBook.Id;
                SelectedBillDetail.Price = SelectedBook.Price;
                SelectedBillDetail.Number = 1;

                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        //-> Commands
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        //private RelayCommand _browseCommand;  
        
        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        //private RelayCommand _editCommand;

        public async void PageLoaded()
        {
            var task = await _bookRepository.GetAll();
            Books = new ObservableCollection<Book>();
            task.ForEach(book => Books.Add(book));

            var task2 = await _accountRepository.GetCustomers();
            Customers = new ObservableCollection<Account>();
            task2.ForEach(customer => Customers.Add(customer));
        }

        // Edit bill details
        public void ExecuteAddCommand()
        {
            // + totalprice
            BillDetail newBillDetail = new BillDetail
            {
                BillId = NewBill.Id,
                Number = 0,
                Price = 0,
            };

            BillDetailList.Add(newBillDetail);

            //await _billRepository.AddBillDetail(newBillDetail);
            UpdateDataSource();
        }
        public void ExecuteDeleteCommand()
        {
            // - totalprice

            BillDetailList.Remove(SelectedBillDetail);

            //await _billRepository.RemoveBillDetail(SelectedBillDetail.BillId, SelectedBillDetail.BookId);
            UpdateDataSource();
        }

        //public async void ExecuteEditCommand()
        //{
        //}

        public async void ExecuteConfirmCommand()
        {
            // TODO: add bill values (update total price)

            NewBill.CustomerId = SelectedCustomer.Id;
            NewBill.TotalPrice = 10;

            await _billRepository.Add(NewBill);

            // TODO: add bill details

            for (int i = 0; i<_billDetailList.Count; i++)
            {
                await _billRepository.AddBillDetail(_billDetailList[i]);
            }

            //if (task)
            //{
            //    ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
            //    await App.MainRoot.ShowDialog("Success", "New order is added!");

            //}
            //else
            //{
            //    ErrorMessage = "* Task failed!";
            //}

            await App.MainRoot.ShowDialog("Success", "New order is added!");
        }

        public void ExecuteBackCommand()
        {
            ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
        }

        public async void UpdateDataSource()
        {
            var task = await _billRepository.GetBillDetailById(NewBill.Id);
            BillDetailList.Clear();
            task.ForEach(billDetail => BillDetailList.Add(billDetail));
        }

        //public void ExecuteBrowseCommand()
        //{
        //    // TODO: browse book with images?
        //}

        // Constructor
        public AddOrderViewModel(int newId)
        {
            _billRepository = new BillRepository();
            _bookRepository = new BookRepository();
            _accountRepository = new AccountRepository();

            var task = _billRepository.GetById(newId);

            NewBill = task.Result;

            PageLoaded();

            //BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);   
            
            AddCommand = new RelayCommand(ExecuteBackCommand);
            DeleteCommand = new RelayCommand(ExecuteConfirmCommand);
        }

    }
}
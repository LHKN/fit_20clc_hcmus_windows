﻿using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using MyShop.Services;
using System.Collections.ObjectModel;
using System.Linq;

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
        private ObservableCollection<int> _selectedBookIds;

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
        //public ObservableCollection<Book> SelectedBooks { get => _selectedBooks; set => _selectedBooks = value; }
        public Account SelectedCustomer { get => _selectedCustomer; set => _selectedCustomer = value; }
        public BillDetail SelectedBillDetail { get => _selectedBillDetail; set => _selectedBillDetail = value; }
        public Book SelectedBook 
        { 
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        //-> Commands
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        //private RelayCommand _browseCommand;  
        
        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;

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
        public async void ExecuteAddCommand()
        {
            if (SelectedBook == null)
            {
                await App.MainRoot.ShowDialog("No selected book", "Please select the book you would like to add to the order!");
                return;
            }
            if (_selectedBookIds.Contains(SelectedBook.Id))
            {
                await App.MainRoot.ShowDialog("Duplicate book", "This book already exists in the order! Please edit that...");
            }
            else
            {
                _selectedBookIds.Add(SelectedBook.Id);

                // + totalprice
                BillDetail newBillDetail = new BillDetail
                {
                    BookId = SelectedBook.Id,
                    BillId = NewBill.Id,
                    Number = 1,
                    Price = SelectedBook.Price,
                };

                NewBill.TotalPrice += newBillDetail.TotalPrice();

                BillDetailList.Add(newBillDetail);
            }
        }

        public void ExecuteDeleteCommand()
        {
            // - totalprice
            NewBill.TotalPrice -= SelectedBillDetail.TotalPrice();

            BillDetailList.Remove(SelectedBillDetail);
        }

        public async void ExecuteConfirmCommand()
        {
            if (SelectedCustomer == null)
            {
                await App.MainRoot.ShowDialog("No selected customer", "Please select the owner of this order!");
                return;
            }

            // TODO: add bill values (update total price in real-time?)

            NewBill.CustomerId = SelectedCustomer.Id;
            NewBill.TotalPrice = 0;

            // TODO: add bill details

            for (int i = 0; i<_billDetailList.Count; i++)
            {
                NewBill.TotalPrice += _billDetailList[i].TotalPrice();
                await _billRepository.AddBillDetail(_billDetailList[i]);
            }

            await _billRepository.Edit(NewBill);

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
            ExecuteBackCommand();
        }

        public void ExecuteBackCommand()
        {
            ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
        }

        // Constructor
        public AddOrderViewModel(int newId)
        {
            _billRepository = new BillRepository();
            _bookRepository = new BookRepository();
            _accountRepository = new AccountRepository();

            var task = _billRepository.GetById(newId);

            NewBill = task.Result;

            BillDetailList = new ObservableCollection<BillDetail>();
            _selectedBookIds = new ObservableCollection<int>();

            PageLoaded();

            //BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);   
            
            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

    }
}
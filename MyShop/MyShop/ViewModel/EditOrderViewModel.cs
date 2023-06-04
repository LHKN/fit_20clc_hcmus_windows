using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyShop.ViewModel
{
    class EditOrderViewModel : ViewModelBase
    {
        // Fields
        private ObservableCollection<BillDetail> _billDetailList; //int <<billId>> respective to the bill's list of <<billDetail>>

        private IBillRepository _billRepository;
        private IBookRepository _bookRepository;
        private IAccountRepository _accountRepository;

        private Bill _currentBill;

        private ObservableCollection<Book> _books;
        private ObservableCollection<Account> _customers;

        private Account _selectedCustomer;
        private BillDetail _selectedBillDetail;

        private int _currentTotalPrice;
        private Book _selectedBook;
        private ObservableCollection<int> _selectedBookIds;

        // getter, setter
        public RelayCommand BackCommand { get => _backCommand; set => _backCommand = value; }
        public RelayCommand ConfirmCommand { get => _confirmCommand; set => _confirmCommand = value; }
        //public RelayCommand BrowseCommand { get => _browseCommand; set => _browseCommand = value; }
        public RelayCommand AddCommand { get => _addCommand; set => _addCommand = value; }
        public RelayCommand DeleteCommand { get => _deleteCommand; set => _deleteCommand = value; }
        public RelayCommand RefreshCommand { get => _refreshCommand; set => _refreshCommand = value; }

        public Bill CurrentBill { get => _currentBill; set => _currentBill = value; }
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
                OnPropertyChanged(nameof(SelectedBook));
            }
        }
        public int CurrentTotalPrice { get => _currentTotalPrice; set => _currentTotalPrice = value; }

        //-> Commands
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        private RelayCommand _browseCommand;

        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _refreshCommand;

        public async void PageLoaded()
        {
            var task = await _bookRepository.GetAll();
            Books = new ObservableCollection<Book>();
            task.ForEach(book => Books.Add(book));

            var task2 = await _accountRepository.GetCustomers();
            Customers = new ObservableCollection<Account>();
            task2.ForEach(customer => Customers.Add(customer));

            var task3 = await _billRepository.GetBillDetailById(CurrentBill.Id);
            BillDetailList = new ObservableCollection<BillDetail>();
            task3.ForEach(bill => {
                // combine local total quantity
                bill.BookQuantity += bill.Number;

                BillDetailList.Add(bill);
                _selectedBookIds.Add(bill.BookId);
            }
            );

            // sync the local total quantity
            for (int i = 0; i < Books.Count; i++)
            {
                for (int j = 0; j < BillDetailList.Count; j++)
                {
                    if (Books[i].Id == BillDetailList[j].BookId)
                    {
                        Books[i].Quantity = BillDetailList[j].BookQuantity;
                    }
                }
            }

            SelectedCustomer = await _accountRepository.GetById(CurrentBill.CustomerId);
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
                    BillId = CurrentBill.Id,
                    BookName = SelectedBook.Title,
                    BookQuantity = SelectedBook.Quantity,
                    Number = 1,
                    Price = SelectedBook.Price,
                };

                ExecuteRefreshCommand();
                CurrentTotalPrice += newBillDetail.TotalPrice();

                BillDetailList.Add(newBillDetail);
            }
        }
        public async void ExecuteDeleteCommand()
        {
            // - totalprice
            if (SelectedBillDetail == null)
            {
                await App.MainRoot.ShowDialog("No selected bill detail", "Please select the bill detail you would like to delete!");
                return;
            }
            _selectedBookIds.Remove(SelectedBillDetail.BookId);

            ExecuteRefreshCommand();
            CurrentTotalPrice -= SelectedBillDetail.TotalPrice();

            BillDetailList.Remove(SelectedBillDetail);
        }

        public void ExecuteRefreshCommand()
        {
            CurrentTotalPrice = 0;

            for (int i = 0; i < _billDetailList.Count; i++)
            {
                CurrentTotalPrice += _billDetailList[i].TotalPrice();
            }

        }

        public async void ExecuteConfirmCommand()
        {
            if (SelectedCustomer == null)
            {
                await App.MainRoot.ShowDialog("No selected customer", "Please select the owner of this order!");
                return;
            }

            // add bill values + update total price in real-time
            CurrentBill.CustomerId = SelectedCustomer.Id;
            ExecuteRefreshCommand();

            CurrentBill.TotalPrice = CurrentTotalPrice;


            // add bill details, resolve duplicate book insertions
            List<int> bookIds = await _billRepository.GetBookIdsById(CurrentBill.Id);
            for (int i = 0; i<bookIds.Count; i++)
            {
                await _billRepository.RemoveBillDetail(CurrentBill.Id, bookIds[i]);
            }

            for (int i = 0; i < _billDetailList.Count; i++)
            {
                await _billRepository.AddBillDetail(_billDetailList[i]);

                _billDetailList[i].BookQuantity = _billDetailList[i].BookQuantity - _billDetailList[i].Number;
            }

            // sync the local total quantity
            for (int i = 0; i < Books.Count; i++)
            {
                for (int j = 0; j < BillDetailList.Count; j++)
                {
                    if (Books[i].Id == BillDetailList[j].BookId)
                    {
                        Books[i].Quantity = BillDetailList[j].BookQuantity;
                    }
                }

                await _bookRepository.EditBookQuantity(Books[i].Id, Books[i].Quantity);
            }

            await _billRepository.Edit(CurrentBill);

            //if (task)
            //{
            //    ParentPageNavigation.ViewModel = new OrderHistoryViewModel();

            //}
            //else
            //{
            //    ErrorMessage = "* Task failed!";
            //}
            await App.MainRoot.ShowDialog("Success", "Order is updated!");
            ExecuteBackCommand();
        }

        public void ExecuteBackCommand()
        {
            ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
        }

        //public void ExecuteBrowseCommand()
        //{
        //    // browse book
        //}

        // Constructor
        public EditOrderViewModel(Bill currentBill)
        {
            _billRepository = new BillRepository();
            _bookRepository = new BookRepository();
            _accountRepository = new AccountRepository();

            CurrentBill = currentBill;
            _selectedBookIds = new ObservableCollection<int>();
            CurrentTotalPrice = CurrentBill.TotalPrice;

            PageLoaded();

            //BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);

            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            RefreshCommand = new RelayCommand(ExecuteRefreshCommand);
        }

    }
}
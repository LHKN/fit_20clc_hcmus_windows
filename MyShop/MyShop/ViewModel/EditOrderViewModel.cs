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
using static System.Reflection.Metadata.BlobBuilder;

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

        // test
        private Book _selectedBook;
        private ObservableCollection<int> _selectedBookIds;

        // getter, setter
        public RelayCommand BackCommand { get => _backCommand; set => _backCommand = value; }
        public RelayCommand ConfirmCommand { get => _confirmCommand; set => _confirmCommand = value; }
        public RelayCommand BrowseCommand { get => _browseCommand; set => _browseCommand = value; }
        public RelayCommand AddCommand { get => _addCommand; set => _addCommand = value; }
        public RelayCommand DeleteCommand { get => _deleteCommand; set => _deleteCommand = value; }

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
                if (_selectedBookIds.Contains(value.Id))
                {
                    App.MainRoot.ShowDialog("Duplicate book", "This book already exists in the order! Please edit that...");
                }
                else
                {
                    _selectedBook = value;

                    SelectedBillDetail.BookId = SelectedBook.Id;
                    SelectedBillDetail.Price = SelectedBook.Price;
                    SelectedBillDetail.Number = 1;

                    _selectedBookIds.Add(SelectedBook.Id);

                    OnPropertyChanged(nameof(SelectedBook));
                }
            }
        }

        //-> Commands
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        private RelayCommand _browseCommand;

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

            var task3 = await _billRepository.GetBillDetailById(CurrentBill.Id);
            BillDetailList = new ObservableCollection<BillDetail>();
            task3.ForEach(bill => BillDetailList.Add(bill));
        }

        // Edit bill details
        public void ExecuteAddCommand()
        {
            // + totalprice
            BillDetail newBillDetail = new BillDetail
            {
                BookId = SelectedBook.Id,
                BillId = CurrentBill.Id,
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


        public async void ExecuteConfirmCommand()
        {
            // TODO: add bill values (update total price in real-time?)

            CurrentBill.CustomerId = SelectedCustomer.Id;
            CurrentBill.TotalPrice = 0;

            // TODO: add bill details; resolve duplicate book insertions

            for (int i = 0; i < _billDetailList.Count; i++)
            {
                CurrentBill.TotalPrice += _billDetailList[i].TotalPrice();

                //book by bill id
                List<int> bookIds = await _billRepository.GetBookIdsById(CurrentBill.Id);
                if (bookIds.Contains(_billDetailList[i].BookId))
                {
                    await _billRepository.EditBillDetail(_billDetailList[i]);
                }
                else
                {
                    await _billRepository.AddBillDetail(_billDetailList[i]);
                }
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
        }

        public void ExecuteBackCommand()
        {
            ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
        }

        public void ExecuteBrowseCommand()
        {
            // browse book
        }

        public async void UpdateDataSource()
        {
            var task = await _billRepository.GetBillDetailById(CurrentBill.Id);
            BillDetailList.Clear();
            task.ForEach(billDetail => BillDetailList.Add(billDetail));
        }

        // Constructor
        public EditOrderViewModel(Bill currentBill)
        {
            _billRepository = new BillRepository();
            _bookRepository = new BookRepository();
            _accountRepository = new AccountRepository();

            CurrentBill = currentBill;
            _selectedBookIds = new ObservableCollection<int>();

            PageLoaded();

            BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);

            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

    }
}
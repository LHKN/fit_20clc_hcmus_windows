﻿using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Model;
using MyShop.Repository;
using MyShop.Services;
using OxyPlot;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Windows.Services.Store;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Windows.Forms.AxHost;

namespace MyShop.ViewModel
{
    class OrderHistoryViewModel : ViewModelBase
    {
        // Fields
        private DateOnly _dateFrom;
        private DateOnly _dateTo;
        private string _paginationMessage;
        private int _currentPage;
        private int _itemsPerPage; //can be changed through setting
        private int _totalItems;
        private int _totalPages;
        private List<Bill> _billList;
        private ObservableCollection<Bill> _displayBillList;
        private Dictionary<int, List<BillDetail>> _billDetailDict; //int <<billId>> respective to the bill's list of <<billDetail>>

        private IBillRepository _billRepository;
        private IAccountRepository _accountRepository;

        private Bill _selectedBill;
        private List<BillDetail> _selectedBillDetailList;
        private string _selectedCustomer;

        // Constructor
        public OrderHistoryViewModel() {
            _billRepository = new BillRepository();
            _accountRepository = new AccountRepository();
            _billDetailDict = new Dictionary<int, List<BillDetail>>();
            BillList = new List<Bill>();
            DisplayBillList = new ObservableCollection<Bill>();

            //Initial paging info
            {
                CurrentPage = 1;
                ItemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
                DateFrom = new DateOnly(2023, 4, 13);
                DateTo = DateOnly.FromDateTime(DateTime.Now);
            }
            ExecuteGetAllCommand();

            //
            AddCommand = new RelayCommand(ExecuteCreateOrderCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteOrderCommand);
            EditCommand = new RelayCommand(ExecuteEditOrderCommand);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            
            GetIdCommand = new RelayCommand(ExecuteGetByIdCommand);
            GoToNextPageCommand = new RelayCommand(ExecuteGoToNextPageCommand);
            GoToPreviousPageCommand = new RelayCommand(ExecuteGoToPreviousPageCommand);
        }

        public async void ExecuteCreateOrderCommand()
        {
            var task = await _billRepository.GetEmptyBillId();
            int newId;

            if (task.Count > 0)
            {
                newId = task[0];
            }
            else
            {
                string username = ConfigurationManager.AppSettings["Username"]!;
                Account customer = await _accountRepository.GetByUsername(username);

                Bill newBill = new Bill
                {
                    CustomerId = customer.Id,
                    TotalPrice = 0,
                    TransactionDate = DateOnly.FromDateTime(DateTime.Now),
                };
                await _billRepository.Add(newBill);

                task = await _billRepository.GetEmptyBillId();
                newId = task[0];
            }

            ParentPageNavigation.ViewModel = new AddOrderViewModel(newId);
        }

        public async void ExecuteDeleteOrderCommand()
        {
            if (SelectedBill == null)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            var confirmed = await App.MainRoot.ShowYesCancelDialog("Delete this item?","Delete","Cancel");

            if (confirmed == true)
            {
                int key = SelectedBill.Id;

                // remove from DETAILTED_BILL
                List<BillDetail> billDetail;
                _billDetailDict.TryGetValue(key, out billDetail);
                for (int i = 0;i < billDetail.Count; i++)
                {
                    await _billRepository.RemoveBillDetail(key, billDetail[i].BookId);
                }

                _billDetailDict.Remove(key);
                DisplayBillList.Remove(SelectedBill);

                BillList.Remove(SelectedBill);

                // remove from BILL
                await _billRepository.Remove(key);

                await App.MainRoot.ShowDialog("Success", "Order is removed!");
            }
        }

        public async void ExecuteEditOrderCommand()
        {
            if (SelectedBill == null)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }

            ParentPageNavigation.ViewModel = new EditOrderViewModel(SelectedBill);
        }

        public async void ExecuteGetAllCommand()
        {
            // get all from date to date
            var task = await _billRepository.GetAll(DateFrom, DateTo);
            BillList = task;

            for (int i = 0; i < BillList.Count; i++)
            {
                List<BillDetail> temp = await _billRepository.GetBillDetailById(BillList[i].Id);

                _billDetailDict.Add(BillList[i].Id, temp);
            }
            TotalItems = BillList.Count;
            UpdateDataSource();
            UpdatePagingInfo();
        }

        public async void ExecuteGetByIdCommand()
        {
            var task = await _billRepository.GetById(SelectedBill.Id);
            //Bill bill = task;
        }

        

        // getter, setter

        public DateOnly DateFrom
        {
            get => _dateFrom; 
            set
            {
                //SetProperty(ref _date, value);
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        public DateOnly DateTo
        {
            get => _dateTo;
            set
            {
                //SetProperty(ref _date, value);
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        public ObservableCollection<Bill> DisplayBillList
        { 
            get => _displayBillList; 
            set => _displayBillList = value;
        }

        public Bill SelectedBill
        {
            get => _selectedBill;
            set
            {
                if (_selectedBill == value)
                    return;
                else if (value == null)
                {
                    SelectedBillDetailList = null;
                    return;
                }

                _selectedBill = value;
                List<BillDetail> billDetail;
                _billDetailDict.TryGetValue(value.Id, out billDetail);
                SelectedBillDetailList = billDetail;
                SelectedCustomer = "Customer: " + getCustomerName(value.CustomerId);

                OnPropertyChanged(nameof(SelectedBill));
            }
        }

        //-> Commands
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }        
        public ICommand GetIdCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand GoToPreviousPageCommand { get; }
        public ICommand GoToNextPageCommand { get; }
        public string PaginationMessage { get => _paginationMessage; set => _paginationMessage = value; }
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }
        public int ItemsPerPage { get => _itemsPerPage; set => _itemsPerPage = value; }
        public int TotalItems { get => _totalItems; set => _totalItems = value; }
        public int TotalPages { get => _totalPages; set => _totalPages = value; }
        public List<Bill> BillList { get => _billList; set => _billList = value; }
        public List<BillDetail> SelectedBillDetailList { get => _selectedBillDetailList; set => _selectedBillDetailList = value; }
        public string SelectedCustomer { get => _selectedCustomer; set => _selectedCustomer = value; }

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
            PaginationMessage = $"{DisplayBillList.Count}/{TotalItems} orders";
        }

        public void UpdateDataSource()
        {
            DisplayBillList.Clear();
            //ResultBooksList = _bookRepository.Filter(BooksList, StartPrice, EndPrice, CurrentKeyword, GenreId);
            var result = BillList.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
            result.ForEach(x => DisplayBillList.Add(x));

        }

        private async void ExecuteSearchCommand()
        {
            if (DateFrom <= DateTo)
            {
                CurrentPage = 1;
                _billDetailDict.Clear();
                var task = await _billRepository.GetAll(DateFrom, DateTo);
                BillList = task;

                for (int i = 0; i < BillList.Count; i++)
                {
                    List<BillDetail> temp = await _billRepository.GetBillDetailById(BillList[i].Id);

                    _billDetailDict.Add(BillList[i].Id, temp);
                }

                UpdateDataSource();
                TotalItems = BillList.Count;
                UpdatePagingInfo();
            }
            else
            {
                await App.MainRoot.ShowDialog("Please select correct dates", "Start date must be earlier or equal to end date!");
            }
        }

        private string getCustomerName(int id)
        {
            var task = _accountRepository.GetById(id);

            return task.Result.Name;
        }
    }
}

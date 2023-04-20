using CommunityToolkit.Mvvm.Input;
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.AxHost;

namespace MyShop.ViewModel
{
    class OrderHistoryViewModel : ViewModelBase
    {
        // Fields
        private System.Nullable<DateTimeOffset> _dateFrom = new DateTime(2023, 4, 13);
        private System.Nullable<DateTimeOffset> _dateTo = DateTime.Now;

        private ObservableCollection<Bill> _billList;
        private Dictionary<int, List<BillDetail>> _billDetailDict; //int <<billId>> respective to the bill's list of <<billDetail>>

        private IBillRepository _billRepository;

        private int _selectedBillIndex = -1;

        // Constructor
        public OrderHistoryViewModel() {
            _billRepository = new BillRepository();
            _billDetailDict = new Dictionary<int, List<BillDetail>>();

            ExecuteGetAllCommand();

            //
            AddCommand = new RelayCommand(ExecuteCreateOrderCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteOrderCommand);
            EditCommand = new RelayCommand(ExecuteEditOrderCommand);            
            
            GetIdCommand = new RelayCommand(ExecuteGetByIdCommand);
        }

        public async void ExecuteCreateOrderCommand()//
        {
            //await App.MainRoot.ShowDialog("DEBUG", _date.ToString());

            Bill bill = new Bill
            {
                CustomerId = 10,
                TotalPrice = 100000,
                TransactionDate = DateOnly.FromDateTime(DateTime.Now),
            };
            // TODO: add bill values

            await _billRepository.Add(bill);
            await App.MainRoot.ShowDialog("Success", "New order is added!");

        }

        public async void ExecuteDeleteOrderCommand()
        {
            if (SelectedBillIndex == -1)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            var confirmed = await App.MainRoot.ShowYesCancelDialog("Delete this item?","Delete","Cancel");

            if (confirmed == true)
            {
                // remove from BILL
                int key = _billList[SelectedBillIndex].Id;
                await _billRepository.Remove(key);

                // remove from DETAILTED_BILL
                List<BillDetail> billDetail;
                _billDetailDict.TryGetValue(key, out billDetail);
                for (int i = 0;i < billDetail.Count; i++)
                {
                    await _billRepository.RemoveBillDetail(key, billDetail[i].BookId);
                }

                _billList.RemoveAt(SelectedBillIndex);
                _billDetailDict.Remove(key);

                await App.MainRoot.ShowDialog("Success", "Order is removed!");
            }
        }

        public async void ExecuteEditOrderCommand()
        {
            if (SelectedBillIndex == -1)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            Bill bill = _billList[SelectedBillIndex];

            // TODO: update bill values and bill details

            await _billRepository.Edit(bill);
            await App.MainRoot.ShowDialog("Success", "Order is updated!");
        }

        public async void ExecuteGetAllCommand()
        {
            // get all from date to date
            DateOnly dateOnlyFrom;
            DateOnly dateOnlyTo;
            var task = await _billRepository.GetAll(dateOnlyFrom, dateOnlyTo);

            Bills = task;

            for (int i = 0; i < Bills.Count; i++)
            {
                List<BillDetail> temp = new List<BillDetail>
                {
                    // TODO: consider assign list here instead of init
                    new BillDetail
                    {
                        BillId = Bills[i].Id,

                        // bill detail here
                    }
                    //...
                };

                _billDetailDict.Add(Bills[i].Id, temp);
            }
        }

        public async void ExecuteGetByIdCommand()
        {
            var task = await _billRepository.GetById(_billList[SelectedBillIndex].Id);
            //Bill bill = task;
        }

        // getter, setter

        public System.Nullable<DateTimeOffset> DateFrom
        {
            get => _dateFrom; 
            set
            {
                //SetProperty(ref _date, value);
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        public System.Nullable<DateTimeOffset> DateTo
        {
            get => _dateTo;
            set
            {
                //SetProperty(ref _date, value);
                _dateFrom = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        public ObservableCollection<Bill> Bills 
        { 
            get => _billList; 
            set => _billList = value;
        }

        //-> Commands
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }        
        public ICommand GetIdCommand { get; }
        public int SelectedBillIndex
        {
            get => _selectedBillIndex;
            set
            {
                if (_selectedBillIndex == value)
                {
                    return;
                }
                _selectedBillIndex = value;
                OnPropertyChanged(nameof(SelectedBillIndex));
            }
        }
    }
}

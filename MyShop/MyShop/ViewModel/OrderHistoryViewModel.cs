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

        //private int _selectedBillIndex = -1;        
        private Bill _selectedBill;

        // Constructor
        public OrderHistoryViewModel() {
            _billRepository = new BillRepository();
            _billDetailDict = new Dictionary<int, List<BillDetail>>();

            ExecuteGetAllCommand();

            //
            AddCommand = new AsyncRelayCommand(ExecuteCreateOrderCommand);
            DeleteCommand = new AsyncRelayCommand(ExecuteDeleteOrderCommand);
            EditCommand = new AsyncRelayCommand(ExecuteEditOrderCommand);            
            
            GetIdCommand = new AsyncRelayCommand(ExecuteGetByIdCommand);
        }

        public async Task ExecuteCreateOrderCommand()//
        {
            ParentPageNavigation.ViewModel = new AddOrderViewModel();

            //await App.MainRoot.ShowDialog("DEBUG", _date.ToString());
        }

        public async Task ExecuteDeleteOrderCommand()
        {
            if (SelectedBill == null)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            var confirmed = await App.MainRoot.ShowYesCancelDialog("Delete this item?","Delete","Cancel");

            if (confirmed == true)
            {
                // remove from BILL
                int key = SelectedBill.Id;
                await _billRepository.Remove(key);

                // remove from DETAILTED_BILL
                List<BillDetail> billDetail;
                _billDetailDict.TryGetValue(key, out billDetail);
                for (int i = 0;i < billDetail.Count; i++)
                {
                    await _billRepository.RemoveBillDetail(key, billDetail[i].BookId);
                }

                _billList.Remove(SelectedBill);
                _billDetailDict.Remove(key);

                await App.MainRoot.ShowDialog("Success", "Order is removed!");
            }
        }

        public async Task ExecuteEditOrderCommand()
        {
            if (SelectedBill == null)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }

            ParentPageNavigation.ViewModel = new EditOrderViewModel(SelectedBill);
        }

        public async Task ExecuteGetAllCommand()
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

        public async Task ExecuteGetByIdCommand()
        {
            var task = await _billRepository.GetById(SelectedBill.Id);
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
        //public int SelectedBillIndex
        //{
        //    get => _selectedBillIndex;
        //    set
        //    {
        //        if (_selectedBillIndex == value)
        //        {
        //            return;
        //        }
        //        _selectedBillIndex = value;
        //        OnPropertyChanged(nameof(SelectedBillIndex));
        //    }
        //}
        public Bill SelectedBill
        {
            get => _selectedBill;
            set
            {
                if (_selectedBill == value)
                    return;
                _selectedBill = value;
                OnPropertyChanged(nameof(SelectedBill));
            }
        }

        //-> Commands
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }        
        public ICommand GetIdCommand { get; }
    }
}

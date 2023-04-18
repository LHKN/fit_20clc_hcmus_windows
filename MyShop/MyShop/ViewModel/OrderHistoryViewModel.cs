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
            //
            AddCommand = new AsyncRelayCommand(ExecuteCreateOrderCommand);
            DeleteCommand = new AsyncRelayCommand(ExecuteDeleteOrderCommand);
            EditCommand = new AsyncRelayCommand(ExecuteEditOrderCommand);            
            
            GetAllCommand = new AsyncRelayCommand(ExecuteGetAllCommand);
            GetIdCommand = new AsyncRelayCommand(ExecuteGetByIdCommand);
        }

        public async Task ExecuteCreateOrderCommand()//
        {
            //await App.MainRoot.ShowDialog("DEBUG", _date.ToString());

            Bill bill = new Bill();
            //add bill values

            await _billRepository.Add(bill);
            await App.MainRoot.ShowDialog("Success", "New order is added!");
        }

        public async Task ExecuteDeleteOrderCommand()
        {
            if (SelectedBillIndex == -1)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            var confirmed = await App.MainRoot.ShowYesCancelDialog("Delete this item?","Delete","Cancel");

            if (confirmed == true)
            {
                await _billRepository.Remove(SelectedBillIndex);
                await App.MainRoot.ShowDialog("Success", "Order is removed!");
            }
        }

        public async Task ExecuteEditOrderCommand()
        {
            if (SelectedBillIndex == -1)
            {
                await App.MainRoot.ShowDialog("No selected item", "Please select an item first!");
                return;
            }
            Bill bill = _billList[SelectedBillIndex];

            //update bill values and bill details

            await _billRepository.Edit(bill);
            await App.MainRoot.ShowDialog("Success", "Order is updated!");
        }

        public async Task ExecuteGetAllCommand()
        {
            // get all from date to date

            //DateOnly dateOnlyFrom;
            //DateOnly dateOnlyTo;
            //var task = await _billRepository.GetAll(dateOnlyFrom, dateOnlyTo);

            //Bills = task;
        }

        public async Task ExecuteGetByIdCommand()
        {
            int id = 0;
            //get value

            await _billRepository.GetById(id);
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
        public ICommand GetAllCommand { get; }
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

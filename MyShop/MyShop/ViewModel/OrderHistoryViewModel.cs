using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    class OrderHistoryViewModel : ViewModelBase
    {
        // Fields
        private System.Nullable<DateTimeOffset> _date = DateTime.Now;
        //private System.Nullable<DateTimeOffset> _date = new DateOnly(2023, 4, 13);

        private List<Bill> billList;
        private Dictionary<int, List<BillDetail>> billDetailDict; //int billId

        private IBillRepository _billRepository;

        // Constructor
        public OrderHistoryViewModel() {
            _billRepository = new BillRepository();
            //
            AddCommand = new RelayCommand(ExecuteCreateOrderCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteOrderCommand);
            EditCommand = new RelayCommand(ExecuteEditOrderCommand);            
            
            GetAllCommand = new RelayCommand(ExecuteGetAllCommand);
            GetIdCommand = new RelayCommand(ExecuteGetByIdCommand);
        }

        public async void ExecuteCreateOrderCommand()
        {
            Bill bill = new Bill();
            //add bill values

            _billRepository.Add(bill);
        }

        public void ExecuteDeleteOrderCommand()
        {
            int id = 0;
            //get value

            _billRepository.Remove(id);
        }

        public void ExecuteEditOrderCommand()
        {
            Bill bill = new Bill();
            //add bill values

            _billRepository.Edit(bill);
        }
        
        public async void ExecuteGetAllCommand()
        {
            string tempDate = _date.ToString();
            var task = await _billRepository.GetAll(tempDate);

            //set value to listview
        }

        public void ExecuteGetByIdCommand()
        {
            int id = 0;
            //get value

            _billRepository.GetById(id);
        }

        // getter, setter

        public System.Nullable<DateTimeOffset> Date
        {
            get => _date; 
            set
            {
                //SetProperty(ref _date, value);
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        //-> Commands
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }        
        public ICommand GetAllCommand { get; }
        public ICommand GetIdCommand { get; }

    }
}

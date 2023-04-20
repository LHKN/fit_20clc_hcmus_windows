using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
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
        private List<BillDetail> _billDetailList; //int <<billId>> respective to the bill's list of <<billDetail>>

        private IBillRepository _billRepository;
        private IBookRepository _bookRepository;
        private Bill _currentBill;
        private ObservableCollection<Book> _books;

        // getter, setter
        public Bill CurrentBill { get => _currentBill; set => _currentBill = value; }
        public RelayCommand BackCommand { get => _backCommand; set => _backCommand = value; }
        public RelayCommand ConfirmCommand { get => _confirmCommand; set => _confirmCommand = value; }
        public RelayCommand BrowseCommand { get => _browseCommand; set => _browseCommand = value; }
        public ObservableCollection<Book> Books { get => _books; set => _books = value; }

        //-> Commands
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        private RelayCommand _browseCommand;

        public async void ExecuteLoadedCommand()
        {
            Books = await _bookRepository.GetAll();
        }

        public async void ExecuteConfirmCommand()
        {

            //// TODO: update bill values and bill details

            //await _billRepository.Edit(CurrentBill);


            CurrentBill.TotalPrice = 0;

            // TODO: add bill values

            await _billRepository.Add(CurrentBill);

            // TODO: add bill details

            for (int i = 0; i < _billDetailList.Count; i++)
            {
                await _billRepository.AddBillDetail(_billDetailList[i]);
            }
            //if (task)
            //{
            //    ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
            //await App.MainRoot.ShowDialog("Success", "Order is updated!");

            //}
            //else
            //{
            //    ErrorMessage = "* Task failed!";
            //}
        }
        public void ExecuteBackCommand()
        {
            ParentPageNavigation.ViewModel = new OrderHistoryViewModel();
        }

        public void ExecuteBrowseCommand()
        {
            // browse book
        }
        // Constructor
        public EditOrderViewModel(Bill currentBill)
        {
            _billRepository = new BillRepository();
            _bookRepository = new BookRepository();

            CurrentBill = currentBill;

            ExecuteLoadedCommand();
            BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);
        }

    }
}
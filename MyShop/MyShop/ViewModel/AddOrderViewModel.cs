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
    class AddOrderViewModel : ViewModelBase
    {
        // Fields
        private List<BillDetail> _billDetailList; //int <<billId>> respective to the bill's list of <<billDetail>>

        private IBillRepository _billRepository;
        private IBookRepository _bookRepository;
        private Bill _newBill;
        private ObservableCollection<Book> _books;

        // getter, setter
        public Bill NewBill { get => _newBill; set => _newBill = value; }
        public RelayCommand BackCommand { get => _backCommand; set => _backCommand = value; }
        public RelayCommand ConfirmCommand { get => _confirmCommand; set => _confirmCommand = value; }
        public RelayCommand BrowseCommand { get => _browseCommand; set => _browseCommand = value; }
        public ObservableCollection<Book> Books { get => _books; set => _books = value; }
        public List<BillDetail> BillDetailList { get => _billDetailList; set => _billDetailList = value; }

        //-> Commands
        private RelayCommand _backCommand;
        private RelayCommand _confirmCommand;
        private RelayCommand _browseCommand;        
        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _editCommand;

        public async void ExecuteLoadedCommand()
        {
            Books = await _bookRepository.GetAll();
        }

        public async void ExecuteConfirmCommand()
        {

            NewBill.TotalPrice = 0;

            // TODO: add bill values

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
        public AddOrderViewModel()
        {
            _billRepository = new BillRepository();
            _bookRepository = new BookRepository();

            NewBill = new Bill();

            ExecuteLoadedCommand();
            BrowseCommand = new RelayCommand(ExecuteBrowseCommand);
            BackCommand = new RelayCommand(ExecuteBackCommand);
            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);
        }

    }
}
using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    class AccountViewModel : ViewModelBase
    {
        private Account _account;
        private IAccountRepository _accountRepository;
        public AccountViewModel(Account account)
        {
            Account = account;
            _accountRepository = new AccountRepository();
            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
        }

        public Account Account { get => _account; set => _account = value; }
        public RelayCommand LogoutCommand { get => _logoutCommand; set => _logoutCommand = value; }

        private RelayCommand _logoutCommand;


        private void ExecuteLogoutCommand()
        {
            
        }
    }
}

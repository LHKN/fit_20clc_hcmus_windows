using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private Account _account;
        private string _retypePassword;
        private string _errorMessage;
        private IAccountRepository _accountRepository;

        public RegisterViewModel()
        {
            _accountRepository = new AccountRepository();
            Account = new Account();
            ResetCommand = new RelayCommand(ExecuteResetCommand);
        }

        private void ExecuteResetCommand()
        {
            if (!Account.Password.Equals(RetypePassword)) 
            { ErrorMessage = "The passwords you entered do not match. Please try again";
                return;
            }
            _accountRepository.Edit(Account);
            ParentPageNavigation.ViewModel = new LoginViewModel();
        }

        public RelayCommand ResetCommand { get; }
        public Account Account { get => _account; set => _account = value; }
        public string RetypePassword { get => _retypePassword; set => _retypePassword = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
    }
}

using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyShop.ViewModel
{
    public partial class LoginViewModel: ViewModelBase
    {
        //-> Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isValidData;
        private bool _isDatabaseConnected;
        private IAccountRepository _accountRepository;

        //-> Constructor
        public LoginViewModel()
        {
            _accountRepository = new AccountRepository();
            LoginCommand = new RelayCommand(ExecuteLoginCommand);

        }

        private void ExecuteLoginCommand()
        {
            bool isValidAccount = false;
            IsDatabaseConnected = true;

            var task = _accountRepository.AuthenticateAccount(new System.Net.NetworkCredential(Username, Password));
            IsDatabaseConnected = !task.Result;
            isValidAccount= task.Result;

            if (isValidAccount)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                ParentPageNavigation.ViewModel = new HomeViewModel();
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
            
        }

        //-> getter, setter
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsValidData
        {
            get => _isValidData;
            set
            {
                SetProperty(ref _isValidData, value);
                OnPropertyChanged(nameof(IsValidData));
            }
        }

        public bool IsDatabaseConnected {
            get => _isDatabaseConnected;
            set
            {
                SetProperty(ref _isDatabaseConnected, value);
                OnPropertyChanged(nameof(IsDatabaseConnected));
            }
        }

        //-> Commands
        public RelayCommand LoginCommand { get; }
        
    }
}

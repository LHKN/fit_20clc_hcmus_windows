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

namespace MyShop.ViewModel
{
    public partial class LoginViewModel: ViewModelBase
    {
        //-> Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = false;
        private bool _isValidData;
        private IAccountRepository _accountRepository;

        //-> Constructor
        public LoginViewModel()
        {
            _accountRepository = new AccountRepository();
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);

        }

        private void ExecuteLoginCommand()
        {
            var isValidAccount = _accountRepository.AuthenticateAccount(new System.Net.NetworkCredential(Username, Password));
            if (isValidAccount)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
            ParentPageNavigation.ViewModel = new HomeViewModel();
        }

        private bool CanExecuteLoginCommand() //TODO: try to figure out a way to call this method again if PropertyChanged
        {
            //if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
            //    Password == null || Password.Length < 3)
            //{
            //    IsValidData = false;
            //}
            //else IsValidData = true;

            //return IsValidData;
            return true;
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
        public bool IsViewVisible { get => _isViewVisible; set => _isViewVisible = value; }

        //-> Commands
        public RelayCommand LoginCommand { get; }
    }
}

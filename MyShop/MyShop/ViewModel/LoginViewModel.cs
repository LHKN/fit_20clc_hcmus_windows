using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
        private Account _account;
        private string _errorMessage;
        private bool _isValidData;
        private bool _isRememberAccount;
        private IAccountRepository _accountRepository;

        //-> Constructor
        public LoginViewModel()
        {
            _accountRepository = new AccountRepository();
            Account = new Account();
            LoadedCommand = new RelayCommand(PageLoaded);
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
            RememberAccountCommand = new RelayCommand<bool>(ExecuteRememberAccountCommand);

        }

        private async void ExecuteLoginCommand()
        {
            ErrorMessage = String.Empty;
            var task = await _accountRepository.AuthenticateAccount(
                new System.Net.NetworkCredential(Account.Username, Account.Password));

            string message = task;

            if (message.Equals("TRUE"))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Account.Username), null);
                ParentPageNavigation.ViewModel = new HomeViewModel();
            }
            else
            {
                ErrorMessage = message;
            }

            if (IsRememberAccount)
            {
                //save to config for local login
                var sysconfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
                sysconfig.AppSettings.Settings["Username"].Value = Account.Username;

                // Encrypt password
                var passwordInBytes = Encoding.UTF8.GetBytes(Account.Password);
                var entropy = new byte[20];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(entropy);
                }

                var cypherText = ProtectedData.Protect(
                    passwordInBytes,
                    entropy,
                    DataProtectionScope.CurrentUser
                );

                var passwordIn64 = Convert.ToBase64String(cypherText);
                var entropyIn64 = Convert.ToBase64String(entropy);

                //MessageBox.Show("passwordInBytes: " + passwordInBytes.Length + "\n"
                //                + "entropy: " + entropy.Length + "\n"
                //                + "cypherText: " + cypherText.Length + "\n"
                //                + "passwordIn64: " + passwordIn64.Length + "\n"
                //                + "entropyIn64: " + entropyIn64.Length + "\n");

                sysconfig.AppSettings.Settings["Password"].Value = passwordIn64;
                sysconfig.AppSettings.Settings["Entropy"].Value = entropyIn64;

                sysconfig.Save(ConfigurationSaveMode.Full);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }

        }

        private void PageLoaded()
        {
            //get from local
            string username = ConfigurationManager.AppSettings["Username"]!;
            string passwordIn64 = ConfigurationManager.AppSettings["Password"];
            string entropyIn64 = ConfigurationManager.AppSettings["Entropy"]!;

            if (passwordIn64.Length != 0)
            {
                byte[] entropyInBytes = Convert.FromBase64String(entropyIn64);
                byte[] cypherTextInBytes = Convert.FromBase64String(passwordIn64);

                byte[] passwordInBytes = ProtectedData.Unprotect(cypherTextInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser
                );

                string password = Encoding.UTF8.GetString(passwordInBytes);

                Account.Username = username;
                Account.Password = password;
            }
        }

        public void ExecuteRememberAccountCommand(bool isChecked)
        {
            IsRememberAccount = isChecked;
        }

        //-> getter, setter
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

        //-> Commands
        public RelayCommand LoginCommand { get; }
        public RelayCommand LoadedCommand { get; }
        public RelayCommand<bool> RememberAccountCommand { get; }
        public Account Account { get => _account; set => _account = value; }
        public bool IsRememberAccount { get => _isRememberAccount; set => _isRememberAccount = value; }
    }
}

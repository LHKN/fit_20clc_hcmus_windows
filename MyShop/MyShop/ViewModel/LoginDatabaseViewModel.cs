using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class LoginDatabaseViewModel: ViewModelBase
    {
        private string _dbUsername;
        private string _dbPassword;
        private string _errorMessage;

        private IAccountRepository _accountRepository;
        private RelayCommand _loginCommand;

        public string DbUsername { get => _dbUsername; set => _dbUsername = value; }
        public string DbPassword { get => _dbPassword; set => _dbPassword = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public RelayCommand LoginCommand { get => _loginCommand; set => _loginCommand = value; }

        public LoginDatabaseViewModel()
        {
            _accountRepository = new AccountRepository();
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
        }


        private async void ExecuteLoginCommand()
        {
            ErrorMessage = String.Empty;
            string message = await _accountRepository.AuthenticateDbAccount(
                new System.Net.NetworkCredential(DbUsername, DbPassword));

            if (message.Equals("TRUE"))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(DbUsername), null);
                ParentPageNavigation.ViewModel = new LoginViewModel();
            }
            else
            {
                ErrorMessage = message;
                return;
            }

            //save to config for local login
            var sysconfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            sysconfig.AppSettings.Settings["dbUsername"].Value = DbUsername;

            // Encrypt password
            var passwordInBytes = Encoding.UTF8.GetBytes(DbPassword);
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

            sysconfig.AppSettings.Settings["dbPassword"].Value = passwordIn64;
            sysconfig.AppSettings.Settings["dbEntropy"].Value = entropyIn64;

            sysconfig.Save(ConfigurationSaveMode.Full);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");


        }

    }
}

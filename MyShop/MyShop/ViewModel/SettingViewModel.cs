using CommunityToolkit.Mvvm.Input;
using MyShop.Model;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.ViewModel
{
    public class SettingViewModel: ViewModelBase
    {
        // Fields
        private bool _toggleSwitchIsOn;
        private int _itemsPerPage;

        // getter, setter
        public bool ToggleSwitchIsOn { get => _toggleSwitchIsOn; set => _toggleSwitchIsOn = value; }
        public int ItemsPerPage { get => _itemsPerPage; set => _itemsPerPage = value; }

        //-> Commands
        private RelayCommand saveSettingCommand;
        public ICommand SaveSettingCommand { get; }

        // Constructor
        public SettingViewModel()
        {
            SaveSettingCommand = new RelayCommand(ExecuteSaveSettingCommand);
            PageLoaded();
        }

        private void PageLoaded()
        {
            //get from local
            ItemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            //CurrentPage = ConfigurationManager.AppSettings["RememberPage"];
        }
        private async void ExecuteSaveSettingCommand()
        {
            var sysconfig = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
            if (_toggleSwitchIsOn == true)
            {
                //sysconfig.AppSettings.Settings["RememberPage"].Value = currentPage;
            }
            sysconfig.AppSettings.Settings["ItemsPerPage"].Value = ItemsPerPage.ToString();
            sysconfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

            await App.MainRoot.ShowDialog("Save", "Your changes have been successfully saved and applied.");
        }
    }
}

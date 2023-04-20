using MyShop.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class SettingViewModel: ViewModelBase
    {
        // Fields
        private bool _toggleSwitchIsOn;

        // getter, setter
        public bool ToggleSwitchIsOn { get => _toggleSwitchIsOn; set => _toggleSwitchIsOn = value; }

        //-> Commands

        // Constructor
        public SettingViewModel()
        {
            if (_toggleSwitchIsOn == true)
            {
                var sysconfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);
                //sysconfig.AppSettings.Settings["RememberPage"].Value = currentPage;
            }
        }

    }
}

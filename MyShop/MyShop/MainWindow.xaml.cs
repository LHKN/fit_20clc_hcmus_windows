// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.Foundation.Collections;
using ConfigurationManager = System.Configuration.ConfigurationManager;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyShop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        //private SqlConnection _connection = null;

        public MainWindow()
        {
            this.InitializeComponent();

            

            //Window_Loaded();
        }

        //private async void loginButton_Click(object sender, RoutedEventArgs e)
        //{

        //    //Connect to database and verify data
        //    string username = usernameTextBox.Text;
        //    string password = passwordBox.Password;
        //    const string db_name = "db_book";

        //    var extconfig = new ConfigurationBuilder().AddUserSecrets<MainWindow>().Build();
        //    // Ý nghĩa: tìm assembly có lớp MainWindow chứa SecretID

        //    string connectionString = extconfig.GetSection("DB")["ConnectionString"];
        //    connectionString.Replace("@username", username);
        //    connectionString.Replace("@password", password);
        //    connectionString.Replace("@db_name", db_name);

        //    try
        //    {
        //        _connection = await Task.Run(() =>
        //        {
        //            var connection = new SqlConnection(connectionString);
        //            connection.OpenAsync();
        //            return connection;
        //        });

        //        if (_connection != null)
        //        {
        //            MessageBox.Show("Database connected successfully!");
        //            if (keepLoginCheckBox.IsChecked == true)
        //            {
        //                // Lưu username và pass
        //                var sysconfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(
        //                    ConfigurationUserLevel.None);
        //                sysconfig.AppSettings.Settings["Username"].Value = usernameTextBox.Text;

        //                // Ma hoa mat khau
        //                var passwordInBytes = Encoding.UTF8.GetBytes(password);
        //                var entropy = new byte[20];
        //                using (var rng = RandomNumberGenerator.Create())
        //                {
        //                    rng.GetBytes(entropy);
        //                }

        //                var cypherText = ProtectedData.Protect(
        //                    passwordInBytes,
        //                    entropy,
        //                    DataProtectionScope.CurrentUser
        //                );

        //                var passwordIn64 = Convert.ToBase64String(cypherText);
        //                var entropyIn64 = Convert.ToBase64String(entropy);
        //                sysconfig.AppSettings.Settings["Password"].Value = passwordIn64;
        //                sysconfig.AppSettings.Settings["Entropy"].Value = entropyIn64;

        //                sysconfig.Save(ConfigurationSaveMode.Full);
        //                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            $"Cannot connect to database. Reason: {ex.Message}");
        //    }

        //}

        //private void Window_Loaded()
        //{
        //    string username = ConfigurationManager.AppSettings["Username"]!;
        //    string passwordIn64 = ConfigurationManager.AppSettings["Password"]!;
        //    string entropyIn64 = ConfigurationManager.AppSettings["Entropy"]!;

        //    if (passwordIn64.Length != 0)
        //    {
        //        byte[] entropyInBytes = Convert.FromBase64String(entropyIn64);
        //        byte[] cypherTextInBytes = Convert.FromBase64String(passwordIn64);

        //        byte[] passwordInBytes = ProtectedData.Unprotect(cypherTextInBytes,
        //            entropyInBytes,
        //            DataProtectionScope.CurrentUser
        //        );

        //        string password = Encoding.UTF8.GetString(passwordInBytes);

        //        usernameTextBox.Text = username;
        //        passwordBox.Password = password;
        //    }
        //}

        //private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        //{
        //    if (revealModeCheckBox.IsChecked == true)
        //    {
        //        passwordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        //    }
        //    else
        //    {
        //        passwordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        //    }
        //}
    }
}

// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyShop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        private SqlConnection _connection = null;
        public LoginWindow()
        {
            this.InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            //Connect to database and verify data
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            const string db_name = "db_book";

            string connectionString = "Server = .\\sqlexpress;" + 
                $"User ID = {username}; Password={password};" +
                $"Database = {db_name};" +
                "TrustServerCertificate=True";
            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();
                Title = "Database is ready!";

                if (keepLoginCheckBox.IsChecked == true)
                {
                    // Lưu username và pass
                    var config = ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);
                    config.AppSettings.Settings["Username"].Value = usernameTextBox.Text;

                    // Ma hoa mat khau
                    var passwordInBytes = Encoding.UTF8.GetBytes(password);
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
                    config.AppSettings.Settings["Password"].Value = passwordIn64;
                    config.AppSettings.Settings["Entropy"].Value = entropyIn64;

                    config.Save(ConfigurationSaveMode.Full);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Cannot connect to database. Reason: {ex.Message}");
            }

        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
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

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                passwordBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                passwordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }
    }
}

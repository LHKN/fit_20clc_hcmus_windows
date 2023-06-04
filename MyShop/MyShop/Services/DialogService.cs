using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MyShop.Services
{
    public static class DialogService
    {
        public static async Task<bool?> ShowYesCancelDialog(
            this FrameworkElement element,
            string title,
            string yesButtonText,
            string cancelButtonText)
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = title,
                PrimaryButtonText = yesButtonText,
                CloseButtonText = cancelButtonText,
                DefaultButton = ContentDialogButton.Close,
                Content = new ContentDialogContent(), // customize Content by changing the .xaml file or using a different .xaml file

                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                XamlRoot = element.XamlRoot,
                RequestedTheme = element.ActualTheme,
            };

            var result = await contentDialog.ShowAsync();

            if (result == ContentDialogResult.None)
            {
                return null;
            }

            return (result == ContentDialogResult.Primary);
        }

        public static async Task<bool?> ShowYesNoCancelDialog(
            this FrameworkElement element,
            string title,
            string yesButtonText,
            string noButtonText,
            string cancelButtonText)
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = title,
                PrimaryButtonText = yesButtonText,
                SecondaryButtonText = noButtonText,
                CloseButtonText = cancelButtonText,
                DefaultButton = ContentDialogButton.Close,
                Content = new ContentDialogContent(), // customize Content by changing the .xaml file or using a different .xaml file

                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                XamlRoot = element.XamlRoot,
                RequestedTheme = element.ActualTheme,
            };

            var result = await contentDialog.ShowAsync();

            if (result == ContentDialogResult.None)
            {
                return null;
            }

            return (result == ContentDialogResult.Primary);
        }

        public static async Task ShowDialog(
            this FrameworkElement element,
            string title,
            string content)
        {
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = title,
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Close,
                Content = content,

                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                XamlRoot = element.XamlRoot,
                RequestedTheme = element.ActualTheme,
            };

            await contentDialog.ShowAsync();
        }
    }
}

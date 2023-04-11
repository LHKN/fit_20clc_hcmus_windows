using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using MyShop.View;
using MyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    
    internal class ViewModelToView : IValueConverter
    {
        private static readonly Dictionary<Type, Type> pairs = new Dictionary<Type, Type>()
        {
            {typeof(LoginViewModel), typeof(LoginPage)},
            {typeof(HomeViewModel), typeof(HomePage)},
            {typeof(BookViewModel), typeof(BookPage)},
            {typeof(DashboardViewModel), typeof(DashboardPage)},
            {typeof(OrderHistoryViewModel), typeof(OrderHistoryPage)},
            {typeof(AccountViewModel), typeof(AccountPage)},
            {typeof(SettingViewModel), typeof(SettingPage)},
            {typeof(StatisticViewModel), typeof(StatisticPage)},
            
            //add more page...

        };
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            pairs.TryGetValue(value.GetType(), out var page);
            Page x = (Page)Activator.CreateInstance(page);
            x.DataContext = value;
            return x;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

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
            {typeof(BooksViewModel), typeof(BooksPage)},
            {typeof(BookTypeViewModel), typeof(BookTypePage)},
            {typeof(DailyRevenueViewModel), typeof(DailyRevenuePage) },
            {typeof(WeeklyRevenueViewModel), typeof(WeeklyRevenuePage) },
            {typeof(MonthlyRevenueViewModel), typeof(MonthlyRevenuePage) },
            {typeof(YearlyRevenueViewModel), typeof(YearlyRevenuePage) },
            {typeof(DailyProductViewModel), typeof(DailyProductPage) },
            {typeof(WeeklyProductViewModel), typeof(WeeklyProductPage) },
            {typeof(MonthlyProductViewModel), typeof(MonthlyProductPage) },
            {typeof(YearlyProductViewModel), typeof(YearlyProductPage) },
            {typeof(AddBookViewModel), typeof(AddBookPage) },
            {typeof(EditBookViewModel), typeof(EditBookPage)},
            {typeof(AddOrderViewModel), typeof(AddOrderPage) },
            {typeof(EditOrderViewModel), typeof(EditOrderPage)},
            {typeof(LoginDatabaseViewModel), typeof(LoginDatabasePage)},
            {typeof(RegisterViewModel), typeof(RegisterPage)},
            
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

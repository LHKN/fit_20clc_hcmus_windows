using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class FormatCurrencyService : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int price = (int)value;   
            string formatted = $"{price:#,###,###.###}₫";
            return formatted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string formatted = (string)value;
            int price = int.Parse(formatted.Replace(",", "").Replace(".", "").Replace("₫", ""));
            return price;
        }
    }
}

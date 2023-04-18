using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
   public class TimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return new DateTimeOffset(((DateOnly)value).ToDateTime(TimeOnly.MinValue).ToUniversalTime());

    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return DateOnly.FromDateTime(((DateTimeOffset)value).DateTime);
    }
}
}

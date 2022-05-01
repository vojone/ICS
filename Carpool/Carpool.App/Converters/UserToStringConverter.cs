using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Carpool.App.Wrapper;
using Carpool.BL.Models;


namespace Carpool.App.Converters
{
    public class UserToStringConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
            {
                return "";
            }
            UserListModel user = (UserListModel)value;
            String result = user.Name + " " + user.Surname + " (⭐ " + user.Rating + ")";
            Debug.WriteLine(result);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

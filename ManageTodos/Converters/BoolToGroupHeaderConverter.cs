using System;
using System.Globalization;
using System.Windows.Data;
using TimeManager.ManageTodos.Properties;

namespace TimeManager.ManageTodos.Converters
{
    public class BoolToGroupHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool)) { return null; }

            return (bool)value ? Resources.AlreadyDone : Resources.NotDone;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

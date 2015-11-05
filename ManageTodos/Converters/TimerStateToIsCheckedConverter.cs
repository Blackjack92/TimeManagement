using System;
using System.Globalization;
using System.Windows.Data;
using TimeManager.ManageTodos.EventArgs;

namespace TimeManager.ManageTodos.Converters
{
    public class TimerStateToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimerState)) { return false; }

            var state = (TimerState)value;

            return state == TimerState.Tracking;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

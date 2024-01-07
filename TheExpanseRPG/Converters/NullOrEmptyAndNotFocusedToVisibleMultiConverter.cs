using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheExpanseRPG.Converters
{
    class NullOrEmptyAndNotFocusedToVisibleMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string value = (string)values[0];
            bool IsFocused = (bool)values[1];
            if (string.IsNullOrEmpty(value) && !IsFocused)
            {
                return Visibility.Visible;
            }
            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

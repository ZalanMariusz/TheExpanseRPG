using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheExpanseRPG.Converters;

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        /*
         * parameter can be used to invert
         * default null param with true value is visible
         * not null param with false is visible
        */
        if ((bool)value && parameter is null)
        {
            return Visibility.Visible;
        }
        if (!(bool)value && parameter is not null)
        {
            return Visibility.Visible;
        }
        return Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

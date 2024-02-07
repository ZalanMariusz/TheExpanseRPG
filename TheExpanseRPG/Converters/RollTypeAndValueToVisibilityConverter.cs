using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters;

public class RollTypeAndValueToVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {

        if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
        {
            return Visibility.Hidden;
        }

        if (values[0] is null)
        {
            return Visibility.Hidden;
        }
        if ((AbilityRollType)values[1] != AbilityRollType.RollAndAssign)
        {
            return Visibility.Hidden;
        }
        return Visibility.Visible;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

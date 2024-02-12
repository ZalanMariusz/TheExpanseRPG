using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Converters;

public class DriveBonusToLabelContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string content = "Describe your ";
        if (value is null)
        {
            return string.Empty;
        }
        if (value is Relationship || value is Membership || value is Reputation)
        {
            content += value.GetType().Name;
        }
        return content;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

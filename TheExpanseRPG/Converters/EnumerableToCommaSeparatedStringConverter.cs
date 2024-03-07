using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TheExpanseRPG.Converters;

public class EnumerableToCommaSeparatedStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null || value is not IEnumerable)
        {
            return string.Empty;
        }
        IEnumerable<object> collection = (value as IEnumerable<object>)!;

        return string.Join(", ", collection.Select(x => x.ToString()));

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

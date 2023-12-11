using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters;

public class OriginToButtonConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return "Origin";
        }
        return $"Origin - {Enum.Parse(typeof(CharacterOrigin), value.ToString()!)}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

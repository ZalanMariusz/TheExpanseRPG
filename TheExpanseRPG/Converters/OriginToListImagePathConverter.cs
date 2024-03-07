using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters;

public class OriginToListImagePathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            CharacterOrigin.Earth => "/Images/CharacterListEarth.png",
            CharacterOrigin.Mars => "/Images/CharacterListMars.png",
            CharacterOrigin.Belt => "/Images/CharacterListBelt.png",
            _ => string.Empty,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace TheExpanseRPG.Converters;

public class ConflictItemToTooltipContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return "";
        }

        string messageBase = "Selection conflicts with the following: ";
        HashSet<string> conflicts = new();
        foreach (var conflictItem in value.ToString()!.Split(','))
        {
            if (conflictItem.Contains("Background"))
            {
                conflicts.Add("Background");
            }
            else if (conflictItem.Contains("Origin"))
            {
                conflicts.Add("Origin");
            }
            else if (conflictItem.Contains("Profession"))
            {
                conflicts.Add("Profession");
            }
        }
        return $"{messageBase}{string.Join(", ", conflicts)}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

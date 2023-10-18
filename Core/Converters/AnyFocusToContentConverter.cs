using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.MVVM.Model;

namespace TheExpanseRPG.Core.Converters
{
    public class AnyFocusToContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AbilityFocus abilityFocus = (AbilityFocus)value;
            if (string.IsNullOrEmpty(abilityFocus.FocusName))
            {
                return string.Concat(abilityFocus.AbilityName, "(choose one)");
            }
            return abilityFocus.FocusName;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

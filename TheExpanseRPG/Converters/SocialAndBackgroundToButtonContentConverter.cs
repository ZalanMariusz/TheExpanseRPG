using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters
{
    public class SocialAndBackgroundToButtonContentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string retval = string.Empty;
            if (values.Length < 2)
            {
                return string.Empty;
            }

            if (values[0] is not null && Enum.TryParse(values[0].ToString(), out CharacterSocialClass socialClass))
            {
                retval += $"{socialClass} Class";
            }

            if (values[1] is not null && values[1] != DependencyProperty.UnsetValue)
            {
                retval += $" - {values[1]}";
            }


            return retval;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

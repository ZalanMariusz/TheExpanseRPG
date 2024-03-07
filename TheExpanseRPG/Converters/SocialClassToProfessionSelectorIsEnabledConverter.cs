using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters
{
    public class SocialClassToProfessionSelectorIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            CharacterSocialClass toCheck = Enum.Parse<CharacterSocialClass>(parameter.ToString()!);
            CharacterSocialClass selectedClass = Enum.Parse<CharacterSocialClass>(value.ToString()!);
            if (selectedClass < toCheck)
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

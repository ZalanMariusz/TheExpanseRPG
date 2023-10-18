using System;
using System.Globalization;
using System.Windows.Data;

namespace TheExpanseRPG.Core.Converters
{
    public class CharacterCreationStepLockInToButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsLocked = (bool)value;
            if (!IsLocked)
            {
                return "Lock in choice and proceed 🔓";
            }
            return "Choice locked 🔐";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

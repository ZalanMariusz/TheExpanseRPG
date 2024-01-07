using System;
using System.Globalization;
using System.Windows.Data;

namespace TheExpanseRPG.Converters
{
    class AttributeRollToContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Concat(value.ToString(), " (", parameter.ToString(), ")");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

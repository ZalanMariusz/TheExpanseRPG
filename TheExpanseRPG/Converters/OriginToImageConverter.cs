using System;
using System.Globalization;
using System.Windows.Data;

namespace TheExpanseRPG.Converters
{
    public class OriginToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string uriBase = "/Images/CharacterOrigin";
            string ImageUri = string.Concat(uriBase, value, ".png");
            return ImageUri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Converters
{
    public class AttributeRollTypeToLabelContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            AttributeRollType rollType = (AttributeRollType)Enum.Parse(typeof(AttributeRollType), value.ToString());
            switch (rollType)
            {
                case AttributeRollType.AllRandom:
                    return "All Random";
                case AttributeRollType.RollAndAssign:
                    return "Roll and Assign";
                case AttributeRollType.DistributePoints:
                    return "Distribute points";
                default:
                    break;
            }
            return value + "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters
{
    public class AttributeRollTypeToLabelContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            AbilityRollType rollType = (AbilityRollType)Enum.Parse(typeof(AbilityRollType), value.ToString()!);
            switch (rollType)
            {
                case AbilityRollType.AllRandom:
                    return "All Random";
                case AbilityRollType.RollAndAssign:
                    return "Roll and Assign";
                case AbilityRollType.DistributePoints:
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

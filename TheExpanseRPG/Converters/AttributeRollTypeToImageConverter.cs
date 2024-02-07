using System;
using System.Globalization;
using System.Windows.Data;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Converters
{
    public class AttributeRollTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            AbilityRollType rollType = (AbilityRollType)Enum.Parse(typeof(AbilityRollType), value.ToString()!);
            switch (rollType)
            {
                case AbilityRollType.AllRandom:
                    return "🎲 ??? 💪";
                case AbilityRollType.RollAndAssign:
                    return "🎲 🢂 💪";
                case AbilityRollType.DistributePoints:
                    return "➕ 💪 ➖";
                default:
                    break;
            }
            return value + "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return AbilityRollType.AllRandom;
        }
    }
}

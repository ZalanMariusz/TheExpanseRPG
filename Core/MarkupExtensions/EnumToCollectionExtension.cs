using System.ComponentModel;
using System;
using System.Windows.Markup;
using System.Linq;

namespace TheExpanseRPG.Core.MarkupExtensions
{
    public class EnumToCollectionExtension : MarkupExtension
    {
        public Type EnumType { get; set; }
        public EnumToCollectionExtension()
        {
            EnumType = typeof(Enum);
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null) throw new ArgumentNullException(nameof(EnumType));

            return Enum.GetValues(EnumType).Cast<Enum>().Select(EnumToDescriptionOrString);
        }

        private string EnumToDescriptionOrString(Enum value)
        {
                return value.GetType().GetField(value.ToString())!
                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                       .Cast<DescriptionAttribute>()
                       .FirstOrDefault()?.Description ?? value.ToString();
            
        }
    }
}

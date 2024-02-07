using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace TheExpanseRPG.ValidationRules
{
    public class ProfessionFocusSelectionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BindingGroup bindingGroup = (BindingGroup)value;

            //foreach (var item in bindingGroup.Items)
            //{
            //    if (item is AbilityFocus &&)
            //    {

            //    }
            //}

            return new ValidationResult(false, "Conflicts");
        }
    }
}

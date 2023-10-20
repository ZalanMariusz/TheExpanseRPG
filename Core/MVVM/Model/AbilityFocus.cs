using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class AbilityFocus : ICharacterCreationBonus
    {
        public CharacterAbilityName AbilityName { get; }
        public string? FocusName {  get; }
        public bool IsImproved { get; private set; } = false;
        public bool IsEmptyFocus { get; private set; } = false;

        public string BenefitName { get { return $"{AbilityName} ({FocusName})"; } }

        public AbilityFocus(CharacterAbilityName abilityName, string focusName)
        {
            AbilityName = abilityName;
            FocusName = focusName;
        }
        public AbilityFocus()
        {
            IsEmptyFocus = true;
        }
        public AbilityFocus(CharacterAbilityName abilityName)
        {
            AbilityName = abilityName;
            IsEmptyFocus = true;
        }
        public void ImproveFocus()
        {
            IsImproved = true;
        }
    }
}

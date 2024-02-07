using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{

    public class CharacterBackGround
    {
        public string BackgroundName { get; }
        public string BackgroundDescription { get; }
        public CharacterSocialClass MainSocialClass { get; }
        //public CharacterAbilityName AbilityBonus { get; }
        public CharacterAbility AbilityBonus { get; }
        public List<AbilityFocus> PossibleAbilityFocuses { get; }
        public List<CharacterTalent> PossiblePlayerTalents { get; }
        public List<ICharacterCreationBonus> BackgroundBenefits { get; }
        public CharacterBackGround(
            string backgroundName,
            string backgroundDescription,
            CharacterSocialClass mainSocialClass,
            CharacterAbility abilityBonus,
            List<AbilityFocus> possibleAbilityFocuses,
            List<CharacterTalent> possiblePlayerTalents,
            List<ICharacterCreationBonus> backgroundBenefits
            )
        {
            BackgroundName = backgroundName;
            BackgroundDescription = backgroundDescription;
            MainSocialClass = mainSocialClass;
            AbilityBonus = abilityBonus;
            PossibleAbilityFocuses = possibleAbilityFocuses;
            PossiblePlayerTalents = possiblePlayerTalents;
            BackgroundBenefits = backgroundBenefits;
        }



    }
}

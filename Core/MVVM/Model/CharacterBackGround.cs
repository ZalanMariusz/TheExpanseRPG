using System.Collections.Generic;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;

namespace TheExpanseRPG.Core.MVVM.Model
{

    public class CharacterBackGround
    {
        public string BackgroundName { get; }
        public string BackgroundDescription { get; }
        public CharacterSocialClass MainSocialClass { get; }
        public CharacterAbilityName AbilityBonus { get; }
        public List<AbilityFocus> PossibleAbilityFocuses { get; }
        public List<CharacterTalent> PossiblePlayerTalents { get; }
        public List<ICharacterCreationBonus> BackgroundBenefits { get; }


        public AbilityFocus? ChosenFocus { get; }
        public CharacterTalent? ChosenTalent { get; }
        public ICharacterCreationBonus? ChosenBenefit { get; set; }

        public CharacterBackGround(
            string backgroundName,
            string backgroundDescription,
            CharacterSocialClass mainSocialClass,
            CharacterAbilityName abilityBonus,
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

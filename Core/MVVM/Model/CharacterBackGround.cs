using System;
using System.Collections.Generic;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;

namespace TheExpanseRPG.Core.MVVM.Model
{
    
    public class CharacterBackGround
    {
        public string BackgroundName { get; }
        public CharacterSocialClass MainSocialClass { get; }
        public CharacterAbilityName AttributeBonus { get; }
        public List<AbilityFocus> PossibleAbilityFocuses { get; }
        public List<CharacterTalent> PossiblePlayerTalents { get; }
        public List<IBackgroundOrProfessionBenefit> BackgroundBenefits { get; }
        public string BackgroundDescription { get; }
        
        public AbilityFocus? ChosenFocus { get; }
        public CharacterTalent? ChosenTalent { get; }
        public IBackgroundOrProfessionBenefit? ChosenBenefit { get; set; }

        public CharacterBackGround(
            string backgroundName,
            string backgroundDescription,
            CharacterSocialClass mainSocialClass,
            CharacterAbilityName attributeBonus,
            List<AbilityFocus> possibleAbilityFocuses,
            List<CharacterTalent> possiblePlayerTalents,
            List<IBackgroundOrProfessionBenefit> backgroundBenefits
            )
        {
            BackgroundName = backgroundName;
            BackgroundDescription = backgroundDescription;
            MainSocialClass = mainSocialClass;
            AttributeBonus = attributeBonus;
            PossibleAbilityFocuses = possibleAbilityFocuses;
            PossiblePlayerTalents = possiblePlayerTalents;
            BackgroundBenefits = backgroundBenefits;
        }



    }
}

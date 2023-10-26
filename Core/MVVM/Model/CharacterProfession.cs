using System.Collections.Generic;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class CharacterProfession
    {
        public string ProfessionName { get; }
        public string ProfessionDescription { get; }
        public CharacterSocialClass ProfessionSocialClass { get; }
        public List<AbilityFocus> FocusChoices { get; }
        public AbilityFocus? ChosenFocus { get; set; }
        public List<CharacterTalent> TalentChoices { get; }
        public CharacterTalent? ChosenTalent { get; set; }

        public CharacterProfession(
            string professionName,
            string professionDescription,
            CharacterSocialClass professionSocialClass,
            List<AbilityFocus> focusChoices,
            List<CharacterTalent> talentChoices
            )
        {
            ProfessionName = professionName;
            ProfessionDescription = professionDescription;
            ProfessionSocialClass = professionSocialClass;
            FocusChoices = focusChoices;
            TalentChoices = talentChoices;
            
        }
    }
}

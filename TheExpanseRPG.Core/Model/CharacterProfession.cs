using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Model
{
    public class CharacterProfession
    {
        public string ProfessionName { get; }
        public string ProfessionDescription { get; }
        public CharacterSocialClass ProfessionSocialClass { get; }
        public List<AbilityFocus> FocusChoices { get; }
        public List<CharacterTalent> TalentChoices { get; }
        public int IncomeBase { get => (int)ProfessionSocialClass * 2; }
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

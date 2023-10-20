using System.Collections.Generic;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterProfessionListService
    {
        public List<CharacterProfession> ProfessionList { get; private set; }
        private AbilityFocusListService FocusListService { get; }
        private TalentListService TalentListService { get; }
        public CharacterProfessionListService(AbilityFocusListService focusListService, TalentListService talentListService)
        {
            ProfessionList = new List<CharacterProfession>();
            FocusListService = focusListService;
            TalentListService = talentListService;
            PopulateProfessionList();
        }

        private void PopulateProfessionList()
        {
            ProfessionList.Add(new CharacterProfession(
                "Artist",
                "You are about expression, whether it is sharing your sense of what is beautiful or expressing your pain or outrage in a form others can understand. You may or may not be making a living at it, but you need to express yourself regardless.",
                CharacterSocialClass.Outsider,
                new List<AbilityFocus>()
                {
                    FocusListService.GetFocusByName(CharacterAbilityName.Communication,"Expression"),
                    FocusListService.GetFocusByName(CharacterAbilityName.Intelligence,"Art"),
                },
                new List<CharacterTalent>()
                {
                    TalentListService.GetTalent("Artistry"),
                    TalentListService.GetTalent("Performance")
                }
                ));

            ProfessionList.Add(new CharacterProfession(
                "Athlete",
                "You pit yourself against physical challenges, from other teams and rival athletes to your own limits and personal best. Your work involves a lot of training and practice to stay at the top of your game.",
                CharacterSocialClass.Lower,
                new List<AbilityFocus>()
                {
                    FocusListService.GetFocusByName(CharacterAbilityName.Constitution,"Running"),
                    FocusListService.GetFocusByName(CharacterAbilityName.Constitution,"Swimming"),
                    FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Acrobatics"),
                    FocusListService.GetFocusByName(CharacterAbilityName.Dexterity,"Free-fall"),
                    FocusListService.GetFocusByName(CharacterAbilityName.Strength,"Climbing"),
                    FocusListService.GetFocusByName(CharacterAbilityName.Strength,"Jumping")
                },
                new List<CharacterTalent>()
                {
                    TalentListService.GetTalent("Agility"),
                    TalentListService.GetTalent("Quick Reflexes")
                }
                ));
        }
    }
}

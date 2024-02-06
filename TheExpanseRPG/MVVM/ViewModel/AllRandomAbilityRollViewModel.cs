using System;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class AllRandomAbilityRollViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand RollAllRandomAbilityCommand { get; set; }
        private readonly PopupService _popupService;
        public int? Accuracy { get { return GetCharacterAbilityValue(); } }
        public int? Constitution { get { return GetCharacterAbilityValue(); } }
        public int? Fighting { get { return GetCharacterAbilityValue(); } }
        public int? Communication { get { return GetCharacterAbilityValue(); } }
        public int? Dexterity { get { return GetCharacterAbilityValue(); } }
        public int? Intelligence { get { return GetCharacterAbilityValue(); } }
        public int? Perception { get { return GetCharacterAbilityValue(); } }
        public int? Strength { get { return GetCharacterAbilityValue(); } }
        public int? Willpower { get { return GetCharacterAbilityValue(); } }

        public AllRandomAbilityRollViewModel(ScopedServiceFactory scopedServiceFactory,PopupService popupService)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
            _popupService = popupService;
            RollAllRandomAbilityCommand = new RelayCommand(o => true, o => RollAllRandom());
        }
        public void RollAllRandom()
        {
            if ((CharacterCreationService.RollsShouldBeReset(AbilityRollType.AllRandom) &&
                _popupService.ShowPopup(WPFStringResources.PopupRollTypeChangeResetsCurrentConfirm) == MessageBoxResult.OK) ||
                !CharacterCreationService.RollsShouldBeReset(AbilityRollType.AllRandom))
            {
                if (CharacterCreationService.RollsShouldBeReset(AbilityRollType.AllRandom))
                {
                    CharacterCreationService.ResetAbilities();
                }
                CharacterCreationService.RollAllRandom();
                foreach (CharacterAbilityName abilityName in Enum.GetValues<CharacterAbilityName>())
                {
                    OnPropertyChanged(abilityName.ToString());
                }
            }

        }
    }
}

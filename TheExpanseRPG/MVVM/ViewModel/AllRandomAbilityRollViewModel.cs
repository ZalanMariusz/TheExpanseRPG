using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class AllRandomAbilityRollViewModel : CharacterAbilityRollTypeViewModel
    {
        public RelayCommand RollAllRandomAbilityCommand { get; set; }

        public AllRandomAbilityRollViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService) : base(popupService)
        {
            CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
            RollAllRandomAbilityCommand = new RelayCommand(o => true, o => RollAllRandom());
        }
        public void RollAllRandom()
        {
            if (!RollsShouldBeReset(AbilityRollType.AllRandom) ||
                ShowRollResetPopup() == MessageBoxResult.OK)
            {
                CharacterCreationService.AbilityBlockBuilder.RollAllRandom();
                NotifyAbilityPropertiesChanged();
            }
        }


    }
}

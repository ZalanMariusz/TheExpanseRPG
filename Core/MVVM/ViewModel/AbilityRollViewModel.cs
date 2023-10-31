using System;
using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AbilityRollViewModel : CharacterCreationViewModelBase
    {
        private AbilityRollType _abilityRollType;
        public AbilityRollType SelectedAbilityRollType
        {
            get { return _abilityRollType; }
            set { _abilityRollType = value; ChangeRollType(); }
        }

        private void ChangeRollType()
        {
            OnPropertyChanged("SelectedAttributeRollType");
            switch (SelectedAbilityRollType)
            {
                case AbilityRollType.AllRandom:
                    NavigateToRollType<AllRandomAbilityRollViewModel>();
                    break;
                case AbilityRollType.RollAndAssign:
                    NavigateToRollType<AssignAbilityRollViewModel>();
                    break;
                case AbilityRollType.DistributePoints:
                    NavigateToRollType<DistributeAbilitiesViewModel>();
                    break;
                default:
                    break;
            }
            CharacterCreationService?.ClearAbilities(SelectedAbilityRollType);
        }

        private void NavigateToRollType<TViewModelBase>() where TViewModelBase : ICharacterCreationViewModel 
        {
            MapServiceFromViewModel();
            NavigationService.NavigateToInnerView<TViewModelBase>(this);
            MapViewModelToService();
        }

        private void MapServiceFromViewModel()
        {
            if (CurrentInnerViewModel != null)
            {
                CharacterCreationService = ((ICharacterCreationViewModel)CurrentInnerViewModel).CharacterCreationService;
            }
        }
        private void MapViewModelToService()
        {
            if (CurrentInnerViewModel != null)
            {
                ((ICharacterCreationViewModel)CurrentInnerViewModel).CharacterCreationService = CharacterCreationService;
            }
        }

        public AbilityRollViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SelectedAbilityRollType = AbilityRollType.AllRandom;
            //NavigateToRollType<AllRandomAbilityRollViewModel>();
        }
    }
}

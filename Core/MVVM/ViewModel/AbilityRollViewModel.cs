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
                    base.NavigateToInnerView<AllRandomAbilityRollViewModel>();
                    break;
                case AbilityRollType.RollAndAssign:
                    base.NavigateToInnerView<AssignAbilityRollViewModel>();
                    break;
                case AbilityRollType.DistributePoints:
                    base.NavigateToInnerView<DistributeAbilitiesViewModel>();
                    break;
                default:
                    break;
            }
            CharacterCreationService?.ClearAbilities(SelectedAbilityRollType);
        }

        public AbilityRollViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SelectedAbilityRollType = AbilityRollType.AllRandom;
        }
    }
}

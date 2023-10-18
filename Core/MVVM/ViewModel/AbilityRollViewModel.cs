using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AbilityRollViewModel : CharacterCreationViewModelBase
    {
        private readonly ScopedServiceFactory _scopeFactory;

        public AbilityRollType SelectedAbilityRollType
        {
            get
            {
                return CharacterCreationService.AbilityRollType;
            }
            set
            {
                CharacterCreationService.AbilityRollType = value;
                ChangeRollType();
            }
        }

        private void ChangeRollType()
        {
            OnPropertyChanged("SelectedAttributeRollType");

            switch (SelectedAbilityRollType)
            {
                case AbilityRollType.AllRandom:
                    NavigationService.NavigateToInnerView<AllRandomAbilityRollViewModel>(this);
                    break;
                case AbilityRollType.RollAndAssign:
                    NavigationService.NavigateToInnerView<AssignAbilityRollViewModel>(this);
                    break;
                case AbilityRollType.DistributePoints:
                    NavigationService.NavigateToInnerView<DistributeAbilitiesViewModel>(this);
                    break;
                default:
                    break;
            }
            CharacterCreationService.ClearAbilities();
        }

        public AbilityRollViewModel(INavigationService navigationService, ScopedServiceFactory serviceScopeFactory)
        {
            NavigationService = navigationService;
            _scopeFactory = serviceScopeFactory;
            CharacterCreationService = (CharacterCreationService)_scopeFactory.GetScopedService<CharacterCreationService>();
            NavigationService.NavigateToInnerView<AllRandomAbilityRollViewModel>(this);
            //SelectedAttributeRollType = AttributeRollType.AllRandom;
        }
    }
}

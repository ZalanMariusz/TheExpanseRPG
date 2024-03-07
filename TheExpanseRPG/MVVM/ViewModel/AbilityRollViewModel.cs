using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;
public class AbilityRollViewModel : CharacterCreationViewModelBase
{
    public AbilityRollType SelectedAbilityRollType
    {
        get { return CharacterCreationService.AbilityBlockBuilder.SelectedAbilityRollType; }
        set
        {
            CharacterCreationService.AbilityBlockBuilder.SelectedAbilityRollType = value;
            NavigateToRollTypeView();
            OnPropertyChanged();
        }
    }
    private void NavigateToRollTypeView()
    {

        switch (SelectedAbilityRollType)
        {
            case AbilityRollType.AllRandom:
                NavigateToInnerView<AllRandomAbilityRollViewModel>();
                break;
            case AbilityRollType.RollAndAssign:
                NavigateToInnerView<AssignAbilityRollViewModel>();
                break;
            case AbilityRollType.DistributePoints:
                NavigateToInnerView<DistributeAbilitiesViewModel>();
                break;
        }
    }

    public AbilityRollViewModel(INavigationService navigationService, ScopedServiceFactory scopedServiceFactory)
    {
        CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
        NavigationService = navigationService;
        CharacterCreationService.AbilityBlockBuilder.AbilityRollTypeChanged += (sender, args) => NavigateToRollTypeView();
        NavigateToRollTypeView();
    }

    private bool _isSelectionLocked;
    public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }
}

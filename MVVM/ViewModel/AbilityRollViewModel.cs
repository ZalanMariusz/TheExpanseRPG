using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;
public class AbilityRollViewModel : CharacterCreationViewModelBase
{
    public AbilityRollType SelectedAbilityRollType
    {
        get { return CharacterCreationService.SelectedAbilityRollType; }
        set
        {
            CharacterCreationService.SelectedAbilityRollType = value;
            CharacterCreationService.ResetAbilities();
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
                ClearAssigneableList();
                break;
            case AbilityRollType.DistributePoints:
                NavigateToInnerView<DistributeAbilitiesViewModel>();
                break;
        }
    }
    private void ClearAssigneableList()
    {
        AssignAbilityRollViewModel? assignableValuesVm = (AssignAbilityRollViewModel?)CurrentInnerViewModel;
        assignableValuesVm?.AssignableAbilityValues.Clear();
    }
    public AbilityRollViewModel(INavigationService navigationService, ScopedServiceFactory scopedServiceFactory)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        NavigationService = navigationService;
        NavigateToRollTypeView();
    }
}

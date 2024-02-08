using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;
public class AbilityRollViewModel : CharacterCreationViewModelBase
{
    public AbilityRollType SelectedAbilityRollType
    {
        get { return CharacterCreationService.CharacterAbilityBlockBuilder.SelectedAbilityRollType; }
        set
        {
            CharacterCreationService.CharacterAbilityBlockBuilder.SelectedAbilityRollType = value;
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
                //ClearAssigneableList();
                break;
            case AbilityRollType.DistributePoints:
                NavigateToInnerView<DistributeAbilitiesViewModel>();
                break;
        }
    }

    public AbilityRollViewModel(INavigationService navigationService, ScopedServiceFactory scopedServiceFactory)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        NavigationService = navigationService;
        CharacterCreationService.CharacterAbilityBlockBuilder.AbilityRollTypeChanged += (sender, args) => NavigateToRollTypeView();
        NavigateToRollTypeView();
    }

    private bool _isSelectionLocked;
    public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }
}

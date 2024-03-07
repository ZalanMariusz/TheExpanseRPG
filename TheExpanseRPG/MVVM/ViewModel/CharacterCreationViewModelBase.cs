using TheExpanseRPG.Core.Services;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;

public class CharacterCreationViewModelBase : ViewModelBase, ICharacterCreationViewModel
{
    private ICharacterCreationService? _characterCreationService;
    public ICharacterCreationService CharacterCreationService
    {
        get { return _characterCreationService!; }
        set { _characterCreationService = value; }
    }
    protected void NavigateToInnerView<TViewModel>() where TViewModel : ICharacterCreationViewModel
    {
        //TViewModel? viewModelToNavigateTo = (TViewModel?)InnerViewModels.FirstOrDefault(x => x.GetType() == typeof(TViewModel));
        NavigationService.NavigateToInnerView<TViewModel>(this);
    }
    protected CharacterCreationFocusConflictChecker ConflictChecker { get; set; } = CharacterCreationFocusConflictChecker.Instance;
}

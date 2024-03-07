using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel;
public class SocialAndBackgroundViewModel : CharacterCreationViewModelBase
{
    public CharacterSocialClass? SelectedCharacterSocialClass
    {
        get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterSocialClass; }
        set
        {
            if ((value < CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession?.ProfessionSocialClass &&
                _popupService.ShowPopup(WPFStringResources.PopupProfessionResetConfirm) == MessageBoxResult.OK) ||
                value >= CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession?.ProfessionSocialClass ||
                CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession is null)
            {
                CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterSocialClass = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCharacterSocialClassDescription));
            }

        }
    }
    public CharacterBackGround? SelectedBackground
    {
        get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterBackground; }
        set
        {
            CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterBackground = value; ClearSelectedBonuses(); OnPropertyChanged();
        }
    }
    public ICharacterCreationBonus? SelectedBenefit
    {
        get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedBackgroundBenefit; }
        set
        {
            CharacterCreationService.SocialAndBackgroundBuilder.SelectedBackgroundBenefit = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasBackgroundFocusConflict));
            OnPropertyChanged(nameof(HasBackgroundBenefitConflict));
        }
    }
    private ObservableCollection<CharacterBackGround>? _availableBackgrounds;
    public ObservableCollection<CharacterBackGround>? AvailableBackgrounds
    {
        get { return _availableBackgrounds; }
        set { _availableBackgrounds = value; }
    }
    public AbilityFocus? SelectedBackgroundFocus
    {
        get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedBackgroundFocus; }
        set
        {
            CharacterCreationService.SocialAndBackgroundBuilder.SelectedBackgroundFocus = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasBackgroundFocusConflict));
            OnPropertyChanged(nameof(HasBackgroundBenefitConflict));
        }
    }
    public bool HasBackgroundFocusConflict
    {
        get { return CharacterCreationFocusConflictChecker.Instance.BackgroundFocusConflicts().Count > 0; }
    }
    public bool HasBackgroundBenefitConflict
    {
        get { return CharacterCreationFocusConflictChecker.Instance.BackgroundBenefitConflicts().Count > 0; }
    }

    public CharacterTalent? SelectedBackgroundTalent
    {
        get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedBackgroundTalent; }
        set { CharacterCreationService.SocialAndBackgroundBuilder.SelectedBackgroundTalent = value; OnPropertyChanged(); }
    }
    public string? SelectedCharacterSocialClassDescription
    {
        get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterSocialClassDescription; }
    }
    public ICharacterBackgroundListService BackgroundListService { get; set; }

    private readonly PopupService _popupService;
    public ObservableCollection<SocialClassWrapper> SocialClassesWithDescription { get; }

    public SocialAndBackgroundViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService, ICharacterBackgroundListService characterBackgroundListService)
    {
        CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
        BackgroundListService = characterBackgroundListService;
        _popupService = popupService;
        SocialClassesWithDescription = new(CharacterCreationService.SocialAndBackgroundBuilder.SocialClassesWithDescription);
        RefreshAvailableBackgrounds();
        CharacterCreationService.SocialAndBackgroundBuilder.SocialClassChanged += (sender, args) => { RefreshAvailableBackgrounds(); };

    }

    private bool _isSelectionLocked;
    public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }



    private void ClearSelectedBonuses()
    {
        SelectedBackgroundFocus = null;
        SelectedBackgroundTalent = null;
        SelectedBenefit = null;
        OnPropertyChanged(nameof(SelectedBackgroundFocus));
        OnPropertyChanged(nameof(SelectedBackgroundTalent));
        OnPropertyChanged(nameof(SelectedBenefit));
    }
    private void RefreshAvailableBackgrounds()
    {
        AvailableBackgrounds = new ObservableCollection<CharacterBackGround>(
            BackgroundListService.CharacterBackgroundList.Where(x => x.MainSocialClass == SelectedCharacterSocialClass));
        OnPropertyChanged(nameof(AvailableBackgrounds));
    }



}

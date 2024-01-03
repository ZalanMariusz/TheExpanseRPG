using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Commands;
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
        get { return CharacterCreationService.SelectedCharacterSocialClass; }
        set
        {
            if ((value < CharacterCreationService.SelectedCharacterProfession?.ProfessionSocialClass &&
                _popupService.ShowPopup("Your selected social class is lower than the social class of your selected profession. This will clear your profession and all selected bonuses for it, even if it is locked in.\n\nAre you sure?") == MessageBoxResult.OK) ||
                value >= CharacterCreationService.SelectedCharacterProfession?.ProfessionSocialClass ||
                CharacterCreationService.SelectedCharacterProfession is null)
            {
                CharacterCreationService.SelectedCharacterSocialClass = value;
                OnPropertyChanged();
                RefreshAvailableBackgrounds();
                OnPropertyChanged(nameof(SelectedCharacterSocialClassDescription));
                EventAggregator.PublishLinkedPropertyChanged("SelectedSocialClass");
                EventAggregator.PublishLinkedPropertyChanged("SelectedProfession");
            }

        }
    }
    public CharacterBackGround? SelectedBackground
    {
        get { return CharacterCreationService.SelectedCharacterBackground; }
        set { CharacterCreationService.SelectedCharacterBackground = value; ClearSelectedBonuses(); OnPropertyChanged(); EventAggregator.PublishLinkedPropertyChanged(); }
    }
    public ICharacterCreationBonus? SelectedBenefit
    {
        get { return CharacterCreationService.SelectedBackgroundBenefit; }
        set
        {
            CharacterCreationService.SelectedBackgroundBenefit = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasBackgroundFocusConflict));
            OnPropertyChanged(nameof(HasBackgroundBenefitConflict));
            EventAggregator.PublishLinkedPropertyChanged("HasOriginSelectionConflict");
            EventAggregator.PublishLinkedPropertyChanged("HasSocialOrBackgroundSelectionConflict");
            EventAggregator.PublishLinkedPropertyChanged("HasProfessionSelectionConflict");
            EventAggregator.PublishLinkedPropertyChanged("OriginConflicts");
            EventAggregator.PublishLinkedPropertyChanged("SocialOrBackgroundConflicts");
            EventAggregator.PublishLinkedPropertyChanged("ProfessionConflicts");
            EventAggregator.PublishLinkedPropertyChanged("HasProfessionFocusConflict");
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
        get { return CharacterCreationService.SelectedBackgroundFocus; }
        set
        {
            CharacterCreationService.SelectedBackgroundFocus = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HasBackgroundFocusConflict));
            OnPropertyChanged(nameof(HasBackgroundBenefitConflict));
            EventAggregator.PublishLinkedPropertyChanged("HasOriginSelectionConflict");
            EventAggregator.PublishLinkedPropertyChanged("HasSocialOrBackgroundSelectionConflict");
            EventAggregator.PublishLinkedPropertyChanged("HasProfessionSelectionConflict");
            EventAggregator.PublishLinkedPropertyChanged("OriginConflicts");
            EventAggregator.PublishLinkedPropertyChanged("SocialOrBackgroundConflicts");
            EventAggregator.PublishLinkedPropertyChanged("ProfessionConflicts");
            EventAggregator.PublishLinkedPropertyChanged("HasProfessionFocusConflict");
        }
    }
    public bool HasBackgroundFocusConflict
    {
        get { return CharacterCreationService.GetBackgroundFocusConflicts().Count > 0; }
    }
    public bool HasBackgroundBenefitConflict
    {
        get { return CharacterCreationService.GetBackgroundBenefitConflicts().Count > 0; }
    }

    public CharacterTalent? SelectedBackgroundTalent
    {
        get { return CharacterCreationService.SelectedBackgroundTalent; }
        set { CharacterCreationService.SelectedBackgroundTalent = value; OnPropertyChanged(); }
    }
    public string? SelectedCharacterSocialClassDescription
    {
        get { return CharacterCreationService.SelectedCharacterSocialClassDescription; }
    }
    public ICharacterBackgroundListService BackgroundListService { get; set; }

    private readonly PopupService _popupService;

    public SocialAndBackgroundViewModel(ScopedServiceFactory scopedServiceFactory, /*INavigationService navigationService*/ PopupService popupService)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        BackgroundListService = CharacterCreationService.BackgroundListService;
        _popupService = popupService;
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

using System.Collections.ObjectModel;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel;
public class SocialAndBackgroundViewModel : CharacterCreationViewModelBase
{
    public CharacterSocialClass? SelectedCharacterSocialClass
    {
        get { return CharacterCreationService.SelectedCharacterSocialClass; }
        set
        {
            CharacterCreationService.SelectedCharacterSocialClass = value;
            OnPropertyChanged();
            RefreshAvailableBackgrounds();
            OnPropertyChanged(nameof(SelectedCharacterSocialClassDescription));
        }
    }
    public CharacterBackGround? SelectedBackground
    {
        get { return CharacterCreationService.SelectedCharacterBackground; }
        set { CharacterCreationService.SelectedCharacterBackground = value; ClearSelectedBonuses(); OnPropertyChanged(); }
    }
    public ICharacterCreationBonus? SelectedBenefit
    {
        get { return CharacterCreationService.SelectedBackgroundBenefit; }
        set { CharacterCreationService.SelectedBackgroundBenefit = value; OnPropertyChanged(); }
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
        set { CharacterCreationService.SelectedBackgroundFocus = value; OnPropertyChanged(); }
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

    public SocialAndBackgroundViewModel(ScopedServiceFactory scopedServiceFactory)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        BackgroundListService = CharacterCreationService.BackgroundListService;
    }
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

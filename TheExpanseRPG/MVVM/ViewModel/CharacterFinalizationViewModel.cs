using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;
public class CharacterFinalizationViewModel : CharacterCreationViewModelBase
{
    public string CharacterName
    {
        get { return CharacterCreationService.CharacterName; }
        set { CharacterCreationService.CharacterName = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanCreateCharacter)); }
    }
    public string CharacterDescription
    {
        get { return CharacterCreationService.CharacterDescription; }
        set { CharacterCreationService.CharacterDescription = value; OnPropertyChanged(); }
    }
    public event EventHandler? CharacterCreated;
    public CharacterOrigin? SelectedOrigin { get { return CharacterCreationService.OriginBuilder.SelectedCharacterOrigin; } }
    public CharacterSocialClass? SelectedSocialClass { get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterSocialClass; } }
    public CharacterBackGround? SelectedBackground { get { return CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterBackground; } }
    public CharacterProfession? SelectedProfession { get { return CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession; } }
    public CharacterDrive? SelectedDrive { get { return CharacterCreationService.DriveBuilder.SelectedCharacterDrive; } }
    public ObservableCollection<CharacterTalent> SelectedTalents { get => GetTalentBonuses(); }

    public int? TotalAccuracyScore { get => CharacterCreationService.AbilityBlockBuilder.GetAccuracyTotal(); }
    public int? TotalCommunicationScore { get => CharacterCreationService.AbilityBlockBuilder.GetCommunicationTotal(); }
    public int? TotalConstitutionScore { get => CharacterCreationService.AbilityBlockBuilder.GetConstitutionTotal(); }
    public int? TotalDexterityScore { get => CharacterCreationService.AbilityBlockBuilder.GetDexterityTotal(); }
    public int? TotalFightingScore { get => CharacterCreationService.AbilityBlockBuilder.GetFightingTotal(); }
    public int? TotalIntelligenceScore { get => CharacterCreationService.AbilityBlockBuilder.GetIntelligenceTotal(); }
    public int? TotalPerceptionScore { get => CharacterCreationService.AbilityBlockBuilder.GetPerceptionTotal(); }
    public int? TotalStrengthScore { get => CharacterCreationService.AbilityBlockBuilder.GetStrengthTotal(); }
    public int? TotalWillpowerScore { get => CharacterCreationService.AbilityBlockBuilder.GetWillpowerTotal(); }

    public string AccuracyFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Accuracy); }
    public string CommunicationFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Communication); }
    public string ConstitutionFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Constitution); }
    public string DexterityFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Dexterity); }
    public string FightingFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Fighting); }
    public string IntelligenceFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Intelligence); }
    public string PerceptionFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Perception); }
    public string StrengthFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Strength); }
    public string WillpowerFocuses { get => GetAbilityFocusNames(CharacterAbilityName.Willpower); }


    public int? Speed { get => CharacterCreationService.Speed; }
    public int? Defense { get => CharacterCreationService.Defense; }
    public int? Toughness { get => CharacterCreationService.Toughness; }
    public int? Fortune { get => CharacterCreationService.Fortune; }
    public int? TotalIncome { get => CharacterCreationService.GetTotalIncome(); }

    public static bool HasOriginConflict { get => CharacterCreationFocusConflictChecker.HasOriginConflict(); }
    public bool IsOriginNotSelected { get => SelectedOrigin is null; }
    public bool IsBackgroundNotSelected { get => SelectedBackground is null; }
    public bool HasBackgroundConflict { get => CharacterCreationFocusConflictChecker.HasBackgroundConflict() && !IsMissingBackgroundBonus; }
    public bool IsSocialClassNotSelected { get => SelectedSocialClass is null; }
    public bool HasProfessionConflict { get => CharacterCreationFocusConflictChecker.HasProfessionConflict() && !IsMissingProfessionBonus; }
    public bool IsProfessionNotSelected { get => SelectedProfession is null; }
    public bool IsDriveNotSelected { get => SelectedDrive is null; }
    public bool IsMissingBackgroundBonus { get => CharacterCreationService.SocialAndBackgroundBuilder.IsMissingBackgroundBonus(); }
    public bool IsMissingProfessionBonus { get => CharacterCreationService.ProfessionBuilder.IsMissingProfessionBonus(); }
    public bool IsMissingDriveBonus { get => CharacterCreationService.DriveBuilder.IsMissingDriveBonus(); }
    public bool IsMissingAbilityRoll { get => CharacterCreationService.AbilityBlockBuilder.IsMissingAbilityRoll(); }
    public bool CanCreateCharacter { get => CharacterCreationService.CanCreateCharacter(); }
    public string Avatar
    {
        get { return CharacterCreationService.CharacterAvatar; }
        set { CharacterCreationService.CharacterAvatar = value; OnPropertyChanged(); }
    }
    public RelayCommand SelectAvatarCommand { get; set; }
    public RelayCommand CreateCharacterCommand { get; set; }
    public RelayCommand RandomizeCharacterCommand { get; set; }
    private PopupService PopupService { get; set; }
    public CharacterFinalizationViewModel(ScopedServiceFactory scopedService, PopupService popupService, INavigationService navigationService)
    {
        PopupService = popupService;
        NavigationService = navigationService;
        CharacterCreationService = scopedService.GetScopedService<ICharacterCreationService>();
        SelectAvatarCommand = new RelayCommand(o => true, o => ShowAvatarSelectionAndAssign());
        CreateCharacterCommand = new(o => CanCreateCharacter, CreateCharacter);
        RandomizeCharacterCommand = new(o => true, o => RandomizeCharacter());
        Avatar = $"{WPFStringResources.AvatarFolderPath}000_blank.png";
    }

    private void CreateCharacter(object? sender)
    {
        if (PopupService.ShowPopup(WPFStringResources.PopupCreateCharacterConfirm) == MessageBoxResult.OK)
        {
            if ((CharacterCreationService.CharacterExists() && PopupService.ShowPopup(WPFStringResources.PopupCharacterNameConflictConfirm) == MessageBoxResult.OK)
                || !CharacterCreationService.CharacterExists())
            {
                CharacterCreationService.CreateCharacter();
                CharacterCreated?.Invoke(sender, new EventArgs());
            }
        }
    }

    private void RandomizeCharacter()
    {
        CharacterCreationService.RandomizeCharacter();
        OnPropertyChanged(null);
    }

    private string GetAbilityFocusNames(CharacterAbilityName abilityName)
    {
        return string.Join(',', CharacterCreationService.FocusBonuses.Where(x => x.AbilityName == abilityName).Select(x => x.FocusName));
    }
    private ObservableCollection<CharacterTalent> GetTalentBonuses()
    {
        return new ObservableCollection<CharacterTalent>(CharacterCreationService.TalentBonuses);
    }
    private void ShowAvatarSelectionAndAssign()
    {
        Avatar = PopupService.ShowAvatarSelectionPopup(Avatar);
    }
}

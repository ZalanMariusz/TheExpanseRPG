using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Talents;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;
public class CharacterFinalizationViewModel : CharacterCreationViewModelBase
{
    private const int FORTUNEBASE = 15;
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
    public CharacterOrigin? SelectedOrigin => CharacterCreationService.OriginBuilder.SelectedCharacterOrigin;
    public CharacterSocialClass? SelectedSocialClass => CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterSocialClass;
    public CharacterBackGround? SelectedBackground => CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterBackground;
    public CharacterProfession? SelectedProfession => CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession;
    public CharacterDrive? SelectedDrive => CharacterCreationService.DriveBuilder.SelectedCharacterDrive;
    public ObservableCollection<CharacterTalent> SelectedTalents => GetTalentBonuses();

    public int? TotalAccuracyScore => CharacterCreationService.AbilityBlockBuilder.GetAccuracyTotal();
    public int? TotalCommunicationScore => CharacterCreationService.AbilityBlockBuilder.GetCommunicationTotal();
    public int? TotalConstitutionScore => CharacterCreationService.AbilityBlockBuilder.GetConstitutionTotal();
    public int? TotalDexterityScore => CharacterCreationService.AbilityBlockBuilder.GetDexterityTotal();
    public int? TotalFightingScore => CharacterCreationService.AbilityBlockBuilder.GetFightingTotal();
    public int? TotalIntelligenceScore => CharacterCreationService.AbilityBlockBuilder.GetIntelligenceTotal();
    public int? TotalPerceptionScore => CharacterCreationService.AbilityBlockBuilder.GetPerceptionTotal();
    public int? TotalStrengthScore => CharacterCreationService.AbilityBlockBuilder.GetStrengthTotal();
    public int? TotalWillpowerScore => CharacterCreationService.AbilityBlockBuilder.GetWillpowerTotal();

    public string AccuracyFocuses => GetAbilityFocusNames(CharacterAbilityName.Accuracy);
    public string CommunicationFocuses => GetAbilityFocusNames(CharacterAbilityName.Communication);
    public string ConstitutionFocuses => GetAbilityFocusNames(CharacterAbilityName.Constitution);
    public string DexterityFocuses => GetAbilityFocusNames(CharacterAbilityName.Dexterity);
    public string FightingFocuses => GetAbilityFocusNames(CharacterAbilityName.Fighting);
    public string IntelligenceFocuses => GetAbilityFocusNames(CharacterAbilityName.Intelligence);
    public string PerceptionFocuses => GetAbilityFocusNames(CharacterAbilityName.Perception);
    public string StrengthFocuses => GetAbilityFocusNames(CharacterAbilityName.Strength);
    public string WillpowerFocuses => GetAbilityFocusNames(CharacterAbilityName.Willpower);


    public int? Speed => CharacterCreationService.AbilityBlockBuilder.Speed;
    public int? Defense => CharacterCreationService.AbilityBlockBuilder.Defense;
    public int? Toughness => CharacterCreationService.AbilityBlockBuilder.Toughness;
    public int? Fortune => FORTUNEBASE + (CharacterCreationService.DriveBuilder.FortuneBonus);
    public int? TotalIncome => CharacterCreationService.GetTotalIncome();

    public bool HasOriginConflict => ConflictChecker.HasOriginConflict();
    public bool IsOriginNotSelected => SelectedOrigin is null;
    public bool IsBackgroundNotSelected => SelectedBackground is null;
    public bool HasBackgroundConflict => ConflictChecker.HasBackgroundConflict() && !IsMissingBackgroundBonus;
    public bool IsSocialClassNotSelected => SelectedSocialClass is null;
    public bool HasProfessionConflict => ConflictChecker.HasProfessionConflict() && !IsMissingProfessionBonus;
    public bool IsProfessionNotSelected => SelectedProfession is null;
    public bool IsDriveNotSelected => SelectedDrive is null;
    public bool IsMissingBackgroundBonus => CharacterCreationService.SocialAndBackgroundBuilder.IsMissingBackgroundBonus();
    public bool IsMissingProfessionBonus => CharacterCreationService.ProfessionBuilder.IsMissingProfessionBonus();
    public bool IsMissingDriveBonus => CharacterCreationService.DriveBuilder.IsMissingDriveBonus();
    public bool IsMissingAbilityRoll => CharacterCreationService.AbilityBlockBuilder.IsMissingAbilityRoll();
    public bool CanCreateCharacter => CharacterCreationService.CanCreateCharacter();
    public string Avatar
    {
        get { return CharacterCreationService.CharacterAvatar; }
        set { CharacterCreationService.CharacterAvatar = value; OnPropertyChanged(); }
    }
    public string IncomeSources => AssembleIncomeSources();

    private string AssembleIncomeSources()
    {
        string retval = string.Empty;

        int? incomeBase = SelectedProfession?.IncomeBase;
        int? socialClassDiff = SelectedSocialClass - SelectedProfession?.ProfessionSocialClass;
        int? bonuses = CharacterCreationService.IncomeBonuses.Sum(x => x.Value);
        int? affluentBonus = (int?)SelectedTalents.FirstOrDefault(x => x is Affluent)?.Degree + 2;

        List<string> sources = new();
        if (incomeBase is not null) sources.Add($"Base from profession: {incomeBase}");
        if (socialClassDiff is not null) sources.Add($"Social class vs Profession bonus: {socialClassDiff}");
        if (bonuses != 0) sources.Add($"Bonuses selected: {bonuses}");
        if (affluentBonus is not null) sources.Add($"From Affluent talent: {affluentBonus}");

        retval = string.Join("\n", sources);

        return retval;
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel;
public class CharacterFinalizationViewModel : CharacterCreationViewModelBase
{
    public string CharacterName
    {
        get { return CharacterCreationService.CharacterName; }
        set { CharacterCreationService.CharacterName = value; OnPropertyChanged(); }
    }
    public string CharacterDescription
    {
        get { return CharacterCreationService.CharacterDescription; }
        set { CharacterCreationService.CharacterDescription = value; OnPropertyChanged(); }
    }
    public CharacterOrigin? SelectedOrigin { get { return CharacterCreationService.SelectedCharacterOrigin; } }
    public CharacterSocialClass? SelectedSocialClass { get { return CharacterCreationService.SelectedCharacterSocialClass; } }
    public CharacterBackGround? SelectedBackground { get { return CharacterCreationService.SelectedCharacterBackground; } }
    public CharacterProfession? SelectedProfession { get { return CharacterCreationService.SelectedCharacterProfession; } }
    public CharacterDrive? SelectedDrive { get { return CharacterCreationService.SelectedCharacterDrive; } }
    public ObservableCollection<CharacterTalent> SelectedTalents { get => GetTalentBonuses(); }
    
    public int? TotalAccuracyScore { get => CharacterCreationService.GetAccuracyTotal(); }
    public int? TotalCommunicationScore { get => CharacterCreationService.GetCommunicationTotal(); }
    public int? TotalConstitutionScore { get => CharacterCreationService.GetConstitutionTotal(); }
    public int? TotalDexterityScore { get => CharacterCreationService.GetDexterityTotal(); }
    public int? TotalFightingScore { get => CharacterCreationService.GetFightingTotal(); }
    public int? TotalIntelligenceScore { get => CharacterCreationService.GetIntelligenceTotal(); }
    public int? TotalPerceptionScore { get => CharacterCreationService.GetPerceptionTotal(); }
    public int? TotalStrengthScore { get => CharacterCreationService.GetStrengthTotal(); }
    public int? TotalWillpowerScore { get => CharacterCreationService.GetWillpowerTotal(); }

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
    public int? Defense { get=> CharacterCreationService.Defense; }
    public int? Toughness { get => CharacterCreationService.Toughness; }
    public int? Fortune { get => CharacterCreationService.Fortune; }
    public int? TotalIncome { get => CharacterCreationService.GetTotalIncome(); }
    public CharacterFinalizationViewModel(ScopedServiceFactory _scopedService)
    {
        CharacterCreationService = (CharacterCreationService)_scopedService.GetScopedService<CharacterCreationService>();
    }
   
    private string GetAbilityFocusNames(CharacterAbilityName abilityName)
    {
        return string.Join(',', CharacterCreationService.GetAbilityFocuses(abilityName).Select(x => x.FocusName));
    }
    private ObservableCollection<CharacterTalent> GetTalentBonuses()
    {
        return new ObservableCollection<CharacterTalent>(CharacterCreationService.TalentBonuses);
    }

}

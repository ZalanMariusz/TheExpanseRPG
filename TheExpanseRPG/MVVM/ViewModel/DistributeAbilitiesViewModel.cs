using System.Runtime.CompilerServices;
using System;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel;

public class DistributeAbilitiesViewModel : CharacterCreationViewModelBase
{
    public RelayCommand IncreaseAbilityValue { get; set; }
    public RelayCommand DecreaseAbilityValue { get; set; }
    public RelayCommand ResetAbilitiesCommand { get; set; }
    public int? Accuracy => GetCharacterAbilityValue();
    public int? Constitution => GetCharacterAbilityValue();
    public int? Fighting => GetCharacterAbilityValue();
    public int? Communication => GetCharacterAbilityValue();
    public int? Dexterity => GetCharacterAbilityValue();
    public int? Intelligence => GetCharacterAbilityValue();
    public int? Perception => GetCharacterAbilityValue();
    public int? Strength => GetCharacterAbilityValue();
    public int? Willpower => GetCharacterAbilityValue();

    public int? AccuracyBonuses => GetAbilityBonuses();
    public int? ConstitutionBonuses => GetAbilityBonuses();
    public int? CommunicationBonuses => GetAbilityBonuses();
    public int? FightingBonuses => GetAbilityBonuses();
    
    public int? DexterityBonuses => GetAbilityBonuses();
    public int? IntelligenceBonuses => GetAbilityBonuses();
    public int? PerceptionBonuses => GetAbilityBonuses();
    public int? StrengthBonuses => GetAbilityBonuses();
    public int? WillpowerBonuses => GetAbilityBonuses();
    public int AbilityPool => CharacterCreationService!.PointsToDistribute;
    public bool NeedsReset => CharacterCreationService.RollsShouldBeReset(AbilityRollType.DistributePoints);

    private PopupService _popupService;
    public DistributeAbilitiesViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        IncreaseAbilityValue = new RelayCommand(CanIncrease, Increase);
        DecreaseAbilityValue = new RelayCommand(CanDecrease, Decrease);
        ResetAbilitiesCommand = new(o => true, o => ResetAbilities());
        _popupService = popupService;
    }



    private void ResetAbilities()
    {
        if ((NeedsReset && _popupService.ShowPopup(WPFStringResources.PopupRollTypeChangeResetsCurrentConfirm) == MessageBoxResult.OK)
            || !NeedsReset)
        {
            CharacterCreationService.SetRollTypeToDistribute();
            OnPropertyChanged(nameof(Accuracy));
            OnPropertyChanged(nameof(Constitution));
            OnPropertyChanged(nameof(Fighting));
            OnPropertyChanged(nameof(Communication));
            OnPropertyChanged(nameof(Dexterity));
            OnPropertyChanged(nameof(Intelligence));
            OnPropertyChanged(nameof(Perception));
            OnPropertyChanged(nameof(Strength));
            OnPropertyChanged(nameof(Willpower));
            OnPropertyChanged(nameof(NeedsReset));
        }
    }
    private void Increase(object abilityName)
    {
        CharacterCreationService!.IncreaseAttributeFromPool(abilityName.ToString()!);
        OnPropertyChanged(nameof(AbilityPool));
        OnPropertyChanged(abilityName.ToString()!);
    }
    private void Decrease(object abilityName)
    {
        CharacterCreationService!.DecreaseAttributeFromPool(abilityName.ToString()!);
        OnPropertyChanged(nameof(AbilityPool));
        OnPropertyChanged(abilityName.ToString()!);
    }

    private bool CanIncrease(object abilityName)
    {
        return CharacterCreationService!.CanIncrease(abilityName.ToString()!)
            && CharacterCreationService.LastUsedRollType == AbilityRollType.DistributePoints;
    }

    private bool CanDecrease(object abilityName)
    {
        return CharacterCreationService!.CanDecrease(abilityName.ToString()!)
            & CharacterCreationService.LastUsedRollType == AbilityRollType.DistributePoints;
    }

    private int? GetAbilityBonuses([CallerMemberName] string abilityName = "")
    {
        abilityName = abilityName.Replace("Bonuses", "");
        return CharacterCreationService.GetAbilityBonuses((CharacterAbilityName)Enum.Parse(typeof(CharacterAbilityName), abilityName));
    }
}

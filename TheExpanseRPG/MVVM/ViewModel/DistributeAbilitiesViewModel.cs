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
    public int? Accuracy { get { return GetCharacterAbilityValue(); } }
    public int? Constitution { get { return GetCharacterAbilityValue(); } }
    public int? Fighting { get { return GetCharacterAbilityValue(); } }
    public int? Communication { get { return GetCharacterAbilityValue(); } }
    public int? Dexterity { get { return GetCharacterAbilityValue(); } }
    public int? Intelligence { get { return GetCharacterAbilityValue(); } }
    public int? Perception { get { return GetCharacterAbilityValue(); } }
    public int? Strength { get { return GetCharacterAbilityValue(); } }
    public int? Willpower { get { return GetCharacterAbilityValue(); } }
    public int AbilityPool { get { return CharacterCreationService!.PointsToDistribute; } }
    public bool NeedsReset { get => CharacterCreationService.RollsShouldBeReset(AbilityRollType.DistributePoints); }

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
}

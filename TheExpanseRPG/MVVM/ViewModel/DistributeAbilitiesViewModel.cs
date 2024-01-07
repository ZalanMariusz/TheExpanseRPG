using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel;

public class DistributeAbilitiesViewModel : CharacterCreationViewModelBase
{
    public RelayCommand IncreaseAbilityValue { get; set; }
    public RelayCommand DecreaseAbilityValue { get; set; }
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
    public DistributeAbilitiesViewModel(ScopedServiceFactory scopedServiceFactory)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        IncreaseAbilityValue = new RelayCommand(CanIncrease, Increase);
        DecreaseAbilityValue = new RelayCommand(CanDecrease, Decrease);
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
        return CharacterCreationService!.CanIncrease(abilityName.ToString()!);
    }

    private bool CanDecrease(object abilityName)
    {
        return CharacterCreationService!.CanDecrease(abilityName.ToString()!);
    }
}

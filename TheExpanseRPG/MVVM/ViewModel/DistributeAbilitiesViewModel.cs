using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel;

public class DistributeAbilitiesViewModel : CharacterAbilityRollTypeViewModel
{
    public RelayCommand IncreaseAbilityValue { get; }
    public RelayCommand DecreaseAbilityValue { get; }
    public RelayCommand ResetAbilitiesCommand { get; }

    public int AbilityPool => CharacterCreationService!.CharacterAbilityBlockBuilder.PointsToDistribute;
    public bool NeedsReset => RollsShouldBeReset(AbilityRollType.DistributePoints);

    public DistributeAbilitiesViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService) : base(popupService)
    {
        CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        IncreaseAbilityValue = new RelayCommand(CanIncrease, Increase);
        DecreaseAbilityValue = new RelayCommand(CanDecrease, Decrease);
        ResetAbilitiesCommand = new(o => true, o => ResetAbilities());
    }

    private void ResetAbilities()
    {
        if (!NeedsReset || ShowRollResetPopup() == MessageBoxResult.OK)
        {
            CharacterCreationService.CharacterAbilityBlockBuilder.SetRollTypeToDistribute();
            NotifyAbilityPropertiesChanged();
            OnPropertyChanged(nameof(NeedsReset));
        }
    }
    private void Increase(object abilityName)
    {
        CharacterCreationService!.CharacterAbilityBlockBuilder.IncreaseAbilityFromPool(abilityName.ToString()!);
        OnPropertyChanged(nameof(AbilityPool));
        OnPropertyChanged(abilityName.ToString()!);
    }
    private void Decrease(object abilityName)
    {
        CharacterCreationService!.CharacterAbilityBlockBuilder.DecreaseAbilityFromPool(abilityName.ToString()!);
        OnPropertyChanged(nameof(AbilityPool));
        OnPropertyChanged(abilityName.ToString()!);
    }

    private bool CanIncrease(object abilityName)
    {
        return CharacterCreationService!.CharacterAbilityBlockBuilder.CanIncrease(abilityName.ToString()!)
            && CharacterCreationService.CharacterAbilityBlockBuilder.LastUsedRollType == AbilityRollType.DistributePoints;
    }

    private bool CanDecrease(object abilityName)
    {
        return CharacterCreationService!.CharacterAbilityBlockBuilder.CanDecrease(abilityName.ToString()!)
            & CharacterCreationService.CharacterAbilityBlockBuilder.LastUsedRollType == AbilityRollType.DistributePoints;
    }
}

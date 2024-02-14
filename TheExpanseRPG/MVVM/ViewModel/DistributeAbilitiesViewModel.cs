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

    public int AbilityPool => CharacterCreationService!.AbilityBlockBuilder.PointsToDistribute;
    public bool NeedsReset => RollsShouldBeReset(AbilityRollType.DistributePoints);

    public DistributeAbilitiesViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService) : base(popupService)
    {
        CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
        IncreaseAbilityValue = new RelayCommand(CanIncrease, Increase);
        DecreaseAbilityValue = new RelayCommand(CanDecrease, Decrease);
        ResetAbilitiesCommand = new(o => true, o => ResetAbilities());
    }

    private void ResetAbilities()
    {
        if (!NeedsReset || ShowRollResetPopup() == MessageBoxResult.OK)
        {
            CharacterCreationService.AbilityBlockBuilder.SetRollTypeToDistribute();
            NotifyAbilityPropertiesChanged();
            OnPropertyChanged(nameof(NeedsReset));
        }
    }
    private void Increase(object abilityName)
    {
        CharacterCreationService!.AbilityBlockBuilder.IncreaseAbilityFromPool(abilityName.ToString()!);
        OnPropertyChanged(nameof(AbilityPool));
        OnPropertyChanged(abilityName.ToString()!);
    }
    private void Decrease(object abilityName)
    {
        CharacterCreationService!.AbilityBlockBuilder.DecreaseAbilityFromPool(abilityName.ToString()!);
        OnPropertyChanged(nameof(AbilityPool));
        OnPropertyChanged(abilityName.ToString()!);
    }

    private bool CanIncrease(object abilityName)
    {
        return CharacterCreationService!.AbilityBlockBuilder.CanIncrease(abilityName.ToString()!)
            && CharacterCreationService.AbilityBlockBuilder.LastUsedRollType == AbilityRollType.DistributePoints;
    }

    private bool CanDecrease(object abilityName)
    {
        return CharacterCreationService!.AbilityBlockBuilder.CanDecrease(abilityName.ToString()!)
            & CharacterCreationService.AbilityBlockBuilder.LastUsedRollType == AbilityRollType.DistributePoints;
    }
}

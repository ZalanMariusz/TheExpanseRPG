using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Builders.Interfaces
{
    public interface ICharacterAbilityBlockBuilder
    {
        event EventHandler? AbilityRollTypeChanged;
        event EventHandler? LastUsedRollTypeChanged;
        List<ICharacterCreationBonus> AbilityBonuses { get; set; }
        List<int?> AbilityValuesToAssign { get; set; }
        CharacterAbilityBlock CharacterAbilityBlock { get; set; }
        AbilityRollType LastUsedRollType { get; }
        int PointsToDistribute { get; }
        AbilityRollType SelectedAbilityRollType { get; set; }


        int Speed { get; }
        int Defense { get; }
        int Toughness { get; }

        bool IsMissingAbilityRoll();
        void AssignAbilityScore(string abilityName, int? newScore);
        bool CanDecrease(string abilityName);
        bool CanIncrease(string abilityName);
        void DecreaseAbilityFromPool(string abilityName);
        int? GetAbilityBonuses(CharacterAbilityName abilityName);
        int? GetAbilityTotal(CharacterAbilityName abilityName);
        int? GetAccuracyTotal();
        int? GetCommunicationTotal();
        int? GetConstitutionTotal();
        int? GetDexterityTotal();
        int? GetFightingTotal();
        int? GetIntelligenceTotal();
        int? GetPerceptionTotal();
        int? GetStrengthTotal();
        int? GetWillpowerTotal();
        void IncreaseAbilityFromPool(string abilityName);
        void ResetAbilities();
        void RollAllRandom();
        void RollAssignableAbilityList();
        bool RollsShouldBeReset(AbilityRollType chosenRollType);
        void SetRollTypeToDistribute();
    }
}
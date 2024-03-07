using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders
{
    public class CharacterAbilityBlockBuilder : ICharacterAbilityBlockBuilder
    {
        private const int SPEEDBASE = 10;
        private const int DEFENSEBASE = 10;
        private const int MAXABILITYVALUE = 3;
        private const int MINABILITYVALUE = 0;
        private const int ABILITYPOOL = 12;
        public int Speed => SPEEDBASE + GetDexterityTotal() ?? 0;
        public int Defense => DEFENSEBASE + GetDexterityTotal() ?? 0;
        public int Toughness => GetConstitutionTotal() ?? 0;

        public event EventHandler? AbilityRollTypeChanged;
        public event EventHandler? LastUsedRollTypeChanged;

        public CharacterAbilityBlock CharacterAbilityBlock { get; set; }
        public List<ICharacterCreationBonus> AbilityBonuses { get; set; }
        private AbilityRollType _selectedAbilityRollType;
        public AbilityRollType SelectedAbilityRollType
        {
            get => _selectedAbilityRollType;
            set
            {
                _selectedAbilityRollType = value;
                AbilityRollTypeChanged?.Invoke(this, new EventArgs());
            }
        }

        private AbilityRollType _lastUsedRollType;
        public AbilityRollType LastUsedRollType
        {
            get => _lastUsedRollType;
            private set
            {
                _lastUsedRollType = value;
                LastUsedRollTypeChanged?.Invoke(this, new EventArgs());
            }
        }
        public int PointsToDistribute { get; private set; } = ABILITYPOOL;
        public List<int?> AbilityValuesToAssign { get; set; } = new();
        private IDiceRollService DiceRollService { get; set; }
        public CharacterAbilityBlockBuilder(IDiceRollService diceRollService)
        {
            DiceRollService = diceRollService;
            CharacterAbilityBlock = new();
            AbilityBonuses = new();
        }

        public void ResetAbilities()
        {
            int? valueToResetTo = SelectedAbilityRollType == AbilityRollType.DistributePoints ? 0 : null;
            CharacterAbilityBlock.AbilityList.ForEach(x => x.BaseValue = valueToResetTo);
            PointsToDistribute = ABILITYPOOL;
        }
        public void RollAllRandom()
        {
            if (SelectedAbilityRollType == AbilityRollType.AllRandom)
            {
                ResetAbilities();
                AbilityValuesToAssign.Clear();
                foreach (CharacterAbility ability in CharacterAbilityBlock.AbilityList)
                {
                    int rollResult = DiceRollService.Roll3D6().GetRollResultSumValue();
                    ability.BaseValue = GetAbilityValueFromRoll(rollResult);
                }
                LastUsedRollType = SelectedAbilityRollType;
            }
        }

        public void RollAssignableAbilityList()
        {
            if (SelectedAbilityRollType == AbilityRollType.RollAndAssign)
            {
                ResetAbilities();
                AbilityValuesToAssign.Clear();
                for (int i = 0; i < CharacterAbilityBlock.AbilityList.Count; i++)
                {
                    AbilityValuesToAssign.Add(GetAbilityValueFromRoll(DiceRollService.Roll3D6().GetRollResultSumValue()));
                }
                LastUsedRollType = SelectedAbilityRollType;
            }
        }
        public void AssignAbilityScore(string abilityName, int? newScore)
        {
            int? abilityValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
            if (abilityValue.HasValue)
            {
                AbilityValuesToAssign.Add(abilityValue);
            }
            AbilityValuesToAssign.Remove(newScore);
            CharacterAbilityBlock.GetAbility(abilityName).BaseValue = newScore;
        }
        public void SetRollTypeToDistribute()
        {
            LastUsedRollType = SelectedAbilityRollType;
            ResetAbilities();
        }
        public void DecreaseAbilityFromPool(string abilityName)
        {
            if (CanDecrease(abilityName))
            {
                CharacterAbilityBlock.GetAbility(abilityName).BaseValue--;
                PointsToDistribute++;
            }
        }
        public void IncreaseAbilityFromPool(string abilityName)
        {
            if (CanIncrease(abilityName))
            {
                CharacterAbilityBlock.GetAbility(abilityName).BaseValue++;
                PointsToDistribute--;
            }
        }
        public bool CanIncrease(string abilityName)
        {
            int? propertyValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
            return PointsToDistribute > 0 && propertyValue < MAXABILITYVALUE && propertyValue != null;
        }
        public bool CanDecrease(string abilityName)
        {
            int? propertyValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
            return PointsToDistribute < ABILITYPOOL && propertyValue > MINABILITYVALUE && propertyValue != null;
        }
        /*checks if abilities need to be reset when trying to "chose" roll type*/
        public bool RollsShouldBeReset(AbilityRollType chosenRollType)
        {
            if (chosenRollType == LastUsedRollType)
            {
                return false;
            }
            if (chosenRollType == AbilityRollType.RollAndAssign)
            {
                return (LastUsedRollType != AbilityRollType.DistributePoints && CharacterAbilityBlock.AbilityList.Any(x => x.BaseValue is not null))
                    || PointsToDistribute != ABILITYPOOL;
            }

            if (chosenRollType == AbilityRollType.AllRandom)
            {
                return
                    LastUsedRollType == AbilityRollType.RollAndAssign || PointsToDistribute != ABILITYPOOL;
            }
            return LastUsedRollType != AbilityRollType.DistributePoints;
        }

        private static int GetAbilityValueFromRoll(int rollResult)
        {
            return rollResult switch
            {
                3 => -2,
                4 or 5 => -1,
                6 or 7 or 8 => 0,
                9 or 10 or 11 => 1,
                12 or 13 or 14 => 2,
                15 or 16 or 17 => 3,
                _ => 4,
            };
        }

        public bool IsMissingAbilityRoll()
        {
            return LastUsedRollType switch
            {
                AbilityRollType.AllRandom or AbilityRollType.RollAndAssign => CharacterAbilityBlock.AbilityList.Any(x => x.AbilityValue is null),
                AbilityRollType.DistributePoints => PointsToDistribute != 0,
                _ => false,
            };
        }

        public int? GetAbilityTotal(CharacterAbilityName abilityName)
        {
            List<int?> abilityValues = new()
            {
                CharacterAbilityBlock.GetAbility(abilityName).BaseValue,
                GetAbilityBonuses(abilityName)
            };
            return abilityValues.Any(x => x is not null) ? abilityValues.Sum() : null;
        }
        public int? GetAbilityBonuses(CharacterAbilityName abilityName)
        {
            var abilityBonuses = AbilityBonuses.Where(x => (x is CharacterAbility ability) && ability.AbilityName == abilityName);
            return abilityBonuses.Any() ? abilityBonuses.Count() : null;
        }
        public int? GetAccuracyTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Accuracy);
        }
        public int? GetCommunicationTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Communication);
        }
        public int? GetConstitutionTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Constitution);
        }
        public int? GetDexterityTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Dexterity);
        }
        public int? GetFightingTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Fighting);
        }
        public int? GetIntelligenceTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Intelligence);
        }
        public int? GetPerceptionTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Perception);
        }
        public int? GetStrengthTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Strength);
        }
        public int? GetWillpowerTotal()
        {
            return GetAbilityTotal(CharacterAbilityName.Willpower);
        }
    }
}

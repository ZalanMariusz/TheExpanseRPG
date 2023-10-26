using System;
using System.Collections.Generic;
using System.Windows.Navigation;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterCreationService : IExpanseService
    {
        public const int MAXABILITYVALUE = 3;
        public const int MINABILITYVALUE = 0;
        public const int ABILITYPOOL = 12;
        public CharacterSocialClass[] PossibleCharacterSocialClass { get; set; } = new CharacterSocialClass[12];
        public CharacterBackGround? ChosenCharacterBackground { get; set; }
        public ICharacterCreationBonus? ChosenCharacterBackgroundBenefit { get; set; }
        public CharacterProfession? ChosenCharacterProfession { get; set; }
        public CharacterAbilityBlock CharacterAbilityBlock { get; set; } = new();
        public TalentListService TalentListService { get; set; }
        public AbilityFocusListService FocusListService { get; set; }
        public DiceRollService DiceRollService { get; set; }
        public CharacterProfessionListService ProfessionListService { get; set; }
        public CharacterBackgroundListService BackgroundListService { get; set; }
        private CharacterOrigin? _characterOrigin;
        public CharacterOrigin? CharacterOrigin { get => _characterOrigin; set { _characterOrigin = value; PopulatePossibleSocialClass(); } }
        public AbilityRollType AbilityRollType { get; set; }
        public CharacterSocialClass? CharacterSocialClass { get; set; }
        public List<int?> AttributeValuesToAssign { get; set; } = new();
        public int PointsToDistribute { get; private set; } = ABILITYPOOL;
        public CharacterCreationService(
            TalentListService talentListService,
            AbilityFocusListService focusListService,
            CharacterBackgroundListService backgroundListService,
            DiceRollService diceRollService,
            CharacterProfessionListService professionListService
            )
        {
            TalentListService = talentListService;
            FocusListService = focusListService;
            DiceRollService = diceRollService;
            ProfessionListService = professionListService;
            BackgroundListService = backgroundListService;

            AbilityRollType = AbilityRollType.AllRandom;
        }
        public bool HasFocusConflict(AbilityFocus toAdd)
        {
            return
                HasChosenBackgroundFocusConflict(toAdd) 
                || HasChosenBackgroundBenefitConflict(toAdd) 
                || HasChosenProfessionFocusConflict(toAdd);
        }

        private bool HasChosenProfessionFocusConflict(AbilityFocus toAdd)
        {
            return ChosenCharacterProfession?.ChosenFocus?.AbilityName == toAdd.AbilityName
                && ChosenCharacterProfession?.ChosenFocus?.FocusName == toAdd.FocusName;
        }

        private bool HasChosenBackgroundBenefitConflict(AbilityFocus toAdd)
        {
            AbilityFocus? benefitFocus = (ChosenCharacterBackground?.ChosenBenefit as AbilityFocus);
            if (benefitFocus == null)
            {
                return false;
            }
            return benefitFocus.AbilityName == toAdd.AbilityName
                && benefitFocus.FocusName == toAdd.FocusName;
        }

        private bool HasChosenBackgroundFocusConflict(AbilityFocus toAdd)
        {
            return ChosenCharacterBackground?.ChosenFocus?.AbilityName == toAdd.AbilityName 
                && ChosenCharacterBackground?.ChosenFocus?.FocusName == toAdd.FocusName;
        }

        public void PopulatePossibleSocialClass()
        {
            if (CharacterOrigin != null)
            {
                int outsiderUpperThreshold = 0;
                int lowerClassUpperThreshold = 0;
                int MiddleClassUpperThreshold = 0;
                switch (CharacterOrigin)
                {
                    case Enums.CharacterOrigin.Belt:
                        outsiderUpperThreshold = 5;
                        lowerClassUpperThreshold = 8;
                        MiddleClassUpperThreshold = 11;
                        break;
                    case Enums.CharacterOrigin.Earth:
                        outsiderUpperThreshold = 3;
                        lowerClassUpperThreshold = 6;
                        MiddleClassUpperThreshold = 10;
                        break;
                    case Enums.CharacterOrigin.Mars:
                        outsiderUpperThreshold = 2;
                        lowerClassUpperThreshold = 6;
                        MiddleClassUpperThreshold = 11;
                        break;
                }
                for (int i = 0; i < PossibleCharacterSocialClass.Length; i++)
                {
                    if (i <= outsiderUpperThreshold - 1)
                    {
                        PossibleCharacterSocialClass[i] = Enums.CharacterSocialClass.Outsider;
                    }
                    else if (i <= lowerClassUpperThreshold - 1)
                    {
                        PossibleCharacterSocialClass[i] = Enums.CharacterSocialClass.Lower;
                    }
                    else if (i <= MiddleClassUpperThreshold - 1)
                    {
                        PossibleCharacterSocialClass[i] = Enums.CharacterSocialClass.Middle;
                    }
                    else
                    {
                        PossibleCharacterSocialClass[i] = Enums.CharacterSocialClass.Upper;
                    }
                }
            }
        }

        public void SetRandomSocialClass()
        {
            if (CharacterOrigin != null)
            {
                int rollValue = DiceRollService.RollND6(2).GetRollResultSumValue();
                CharacterSocialClass = PossibleCharacterSocialClass[rollValue - 1];
            }
        }
        internal void ClearAbilities()
        {
            int? valueToResetTo = AbilityRollType == AbilityRollType.DistributePoints ? 0 : null;
            CharacterAbilityBlock.AbilityList.ForEach(x => x.BaseValue = valueToResetTo);
            PointsToDistribute = ABILITYPOOL;
        }

        public void RollAllRandom()
        {
            foreach (CharacterAbility ability in CharacterAbilityBlock.AbilityList)
            {
                int rollResult = DiceRollService.Roll3D6().GetRollResultSumValue();
                ability.BaseValue = GetAttributeValueFromRoll(rollResult);
            }
        }

        private static int GetAttributeValueFromRoll(int rollResult)
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

        public void RollAssignableAbilityList()
        {
            ClearAbilities();
            AttributeValuesToAssign.Clear();
            PopulateScoreList();
        }
        private void PopulateScoreList()
        {
            RollResult rollResult;
            foreach (CharacterAbility ability in CharacterAbilityBlock.AbilityList)
            {
                rollResult = DiceRollService.Roll3D6();
                int rolledAttributeValue = GetAttributeValueFromRoll(rollResult.GetRollResultSumValue());
                AttributeValuesToAssign.Add(rolledAttributeValue);
            }
        }

        public void AssignAbilityScore(string abilityName, int? newScore)
        {
            int? abilityValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
            if (abilityValue.HasValue)
            {
                AttributeValuesToAssign.Add(abilityValue);
            }
            AttributeValuesToAssign.Remove(newScore);
            CharacterAbilityBlock.GetAbility(abilityName).BaseValue = newScore;
        }

        public void DecreaseAttributeFromPool(string abilityName)
        {
            if (CanDecrease(abilityName))
            {
                CharacterAbilityBlock.GetAbility(abilityName).BaseValue--;
                PointsToDistribute++;
            }
        }

        public void IncreaseAttributeFromPool(string abilityName)
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
            return PointsToDistribute > 0 && (propertyValue < MAXABILITYVALUE || propertyValue == null);
        }
        public bool CanDecrease(string abilityName)
        {
            int? propertyValue = CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
            return PointsToDistribute < ABILITYPOOL && (propertyValue > MINABILITYVALUE || propertyValue == null);
        }


    }
}

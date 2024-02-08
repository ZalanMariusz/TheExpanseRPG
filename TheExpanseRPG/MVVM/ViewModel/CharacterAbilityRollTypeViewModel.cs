using System;
using System.Runtime.CompilerServices;
using System.Windows;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class CharacterAbilityRollTypeViewModel : CharacterCreationViewModelBase
    {
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

        protected PopupService PopupSerivce { get; }

        public CharacterAbilityRollTypeViewModel(PopupService popupService)
        {
            PopupSerivce = popupService;
        }

        protected MessageBoxResult ShowRollResetPopup()
        {
            return PopupSerivce.ShowPopup(WPFStringResources.PopupRollTypeChangeResetsCurrentConfirm);
        }


        protected int? GetCharacterAbilityValue([CallerMemberName] string abilityName = "")
        {
            return CharacterCreationService.CharacterAbilityBlockBuilder.CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
        }
        private int? GetAbilityBonuses([CallerMemberName] string abilityName = "")
        {
            abilityName = abilityName.Replace("Bonuses", "");
            return CharacterCreationService.CharacterAbilityBlockBuilder.GetAbilityBonuses((CharacterAbilityName)Enum.Parse(typeof(CharacterAbilityName), abilityName));
        }

        protected bool RollsShouldBeReset(AbilityRollType rollType)
        {
            return CharacterCreationService.CharacterAbilityBlockBuilder.RollsShouldBeReset(rollType);
        }

        protected void NotifyAbilityPropertiesChanged()
        {
            foreach (var item in Enum.GetValues<CharacterAbilityName>())
            {
                OnPropertyChanged(item.ToString());
            }
        }
    }
}

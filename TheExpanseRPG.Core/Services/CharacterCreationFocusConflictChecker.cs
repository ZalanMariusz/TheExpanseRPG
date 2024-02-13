using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Builders;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterCreationFocusConflictChecker
    {
        public static Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; } = new();
        private static List<string> GetConflictsWith(string bonusKey)
        {
            if (AllBonuses.TryGetValue(bonusKey, out ICharacterCreationBonus? bonus))
            {
                return AllBonuses.Where(x =>
                    x.Value?.CreationBonusName == bonus?.CreationBonusName
                        && x.Key != bonusKey
                        && x.Value is AbilityFocus).Select(x => x.Key).ToList();
            }
            return new();
        }
        public static List<string> GetBackgroundFocusConflicts()
        {
            return GetConflictsWith(nameof(CharacterSocialAndBackgroundBuilder.SelectedBackgroundFocus));
        }
        public static List<string> GetBackgroundBenefitConflicts()
        {
            return GetConflictsWith(nameof(CharacterSocialAndBackgroundBuilder.SelectedBackgroundBenefit));
        }
        public static List<string> GetOriginFocusConflicts()
        {
            return GetConflictsWith(nameof(CharacterOriginBuilder.SelectedCharacterOrigin));
        }
        public static List<string> GetProfessionFocusConflicts()
        {
            return GetConflictsWith(nameof(CharacterProfessionBuilder.SelectedProfessionFocus));
        }
        public static bool HasBackgroundConflict()
        {
            return GetBackgroundBenefitConflicts().Any() || GetBackgroundFocusConflicts().Any();
        }
        public static bool HasOriginConflict()
        {
            return GetOriginFocusConflicts().Any();
        }
        public static bool HasProfessionConflict()
        {
            return GetProfessionFocusConflicts().Any();
        }
        public static bool HasConfclits()
        {
            return HasBackgroundConflict() || HasOriginConflict() || HasProfessionConflict();
        }
    }
}

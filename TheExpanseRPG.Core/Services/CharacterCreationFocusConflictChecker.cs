using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterCreationFocusConflictChecker : ICharacterCreationFocusConflictChecker
    {
        private CharacterCreationFocusConflictChecker() { }
        private static CharacterCreationFocusConflictChecker? _instance;
        public static CharacterCreationFocusConflictChecker Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }
        public Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; } = new();
        private List<string> ConflictsWith(string bonusKey)
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
        public List<string> BackgroundFocusConflicts()
        {
            return ConflictsWith(nameof(CharacterSocialAndBackgroundBuilder.SelectedBackgroundFocus));
        }
        public List<string> BackgroundBenefitConflicts()
        {
            return ConflictsWith(nameof(CharacterSocialAndBackgroundBuilder.SelectedBackgroundBenefit));
        }
        public List<string> OriginFocusConflicts()
        {
            return ConflictsWith(nameof(CharacterOriginBuilder.SelectedCharacterOrigin));
        }
        public List<string> ProfessionFocusConflicts()
        {
            return ConflictsWith(nameof(CharacterProfessionBuilder.SelectedProfessionFocus));
        }
        public bool HasBackgroundConflict()
        {
            return BackgroundBenefitConflicts().Any() || BackgroundFocusConflicts().Any();
        }
        public bool HasOriginConflict()
        {
            return OriginFocusConflicts().Any();
        }
        public bool HasProfessionConflict()
        {
            return ProfessionFocusConflicts().Any();
        }
        public bool HasConfclits()
        {
            return HasBackgroundConflict() || HasOriginConflict() || HasProfessionConflict();
        }
    }
}

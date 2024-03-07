using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface ICharacterCreationFocusConflictChecker
    {
        Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; }

        List<string> BackgroundBenefitConflicts();
        List<string> BackgroundFocusConflicts();
        bool HasBackgroundConflict();
        bool HasConfclits();
        bool HasOriginConflict();
        bool HasProfessionConflict();
        List<string> OriginFocusConflicts();
        List<string> ProfessionFocusConflicts();
    }
}
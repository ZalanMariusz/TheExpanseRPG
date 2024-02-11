using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Builders.Interfaces;

public interface ICharacterDriveBuilder
{
    CharacterDrive? SelectedCharacterDrive { get; set; }
    ICharacterCreationBonus? SelectedDriveBonus { get; set; }
    CharacterTalent? SelectedDriveTalent { get; set; }

    event EventHandler? DriveSelectionChanged;
    event EventHandler<string>? SelectedDriveTalentChanged;
    event EventHandler<string>? SelectedDriveBonusChanged;

    void GenerateRandom();
    bool IsMissingDriveBonus();
}
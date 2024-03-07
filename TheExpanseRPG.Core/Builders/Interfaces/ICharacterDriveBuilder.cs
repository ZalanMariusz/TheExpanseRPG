using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Builders.Interfaces;

public interface ICharacterDriveBuilder
{
    CharacterDrive? SelectedCharacterDrive { get; set; }
    ICharacterCreationBonus? SelectedDriveBonus { get; set; }
    CharacterTalent? SelectedDriveTalent { get; set; }
    string? DriveBonusDescription { get; set; }
    int FortuneBonus { get; }

    event EventHandler? DriveSelectionChanged;
    event EventHandler<string>? BonusSelectionChanged;

    void GenerateRandom();
    bool IsMissingDriveBonus();
}
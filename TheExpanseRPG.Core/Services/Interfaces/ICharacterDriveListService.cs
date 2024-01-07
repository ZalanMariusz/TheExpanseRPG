using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface ICharacterDriveListService
    {
        List<ICharacterCreationBonus> DriveBonuses { get; }
        List<CharacterDrive> DriveList { get; }
        Dictionary<string, List<CharacterTalent>> DriveTalentList { get; }

        List<CharacterTalent> GetDriveTalentOptions(string? driveName);
    }
}
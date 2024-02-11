using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders
{
    public class CharacterDriveBuilder : ICharacterDriveBuilder
    {
        public event EventHandler? DriveSelectionChanged;
        public event EventHandler<string>? SelectedDriveTalentChanged;
        public event EventHandler<string>? SelectedDriveBonusChanged;

        private CharacterTalent? _selectedDriveTalent;
        private CharacterDrive? _selectedCharacterDrive;
        private ICharacterCreationBonus? _selectedDriveBonus;

        private ICharacterDriveListService DriveListService { get; set; }
        public CharacterDrive? SelectedCharacterDrive
        {
            get { return _selectedCharacterDrive; }
            set
            {
                _selectedCharacterDrive = value;
                DriveSelectionChanged?.Invoke(this, new EventArgs());
            }
        }

        public CharacterTalent? SelectedDriveTalent
        {
            get => _selectedDriveTalent;
            set
            {
                _selectedDriveTalent = value;
                SelectedDriveTalentChanged?.Invoke(this, nameof(SelectedDriveTalent));
            }
        }
        public ICharacterCreationBonus? SelectedDriveBonus
        {
            get => _selectedDriveBonus;
            set
            {
                _selectedDriveBonus = value;
                SelectedDriveBonusChanged?.Invoke(this, nameof(SelectedDriveBonus));
            }
        }
        public CharacterDriveBuilder(ICharacterDriveListService characterDriveListService)
        {
            DriveListService = characterDriveListService;
        }
        public bool IsMissingDriveBonus()
        {
            return SelectedDriveBonus is null || SelectedDriveTalent is null;
        }

        public void GenerateRandom()
        {
            SelectedCharacterDrive = DriveListService.DriveList[Random.Shared.Next(0, DriveListService.DriveList.Count)];
            SelectedDriveBonus = DriveListService.DriveBonuses[Random.Shared.Next(0, DriveListService.DriveBonuses.Count)];
            SelectedDriveTalent = DriveListService.DriveTalentList[SelectedCharacterDrive.DriveName][Random.Shared.Next(0, DriveListService.DriveTalentList[SelectedCharacterDrive.DriveName].Count)];
        }
    }
}

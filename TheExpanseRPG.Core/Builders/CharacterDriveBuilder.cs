using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders
{
    public class CharacterDriveBuilder : ICharacterDriveBuilder
    {
        private const int FORTUNEBONUS = 5;
        public event EventHandler? DriveSelectionChanged;
        public event EventHandler<string>? BonusSelectionChanged;

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
                BonusSelectionChanged?.Invoke(this, nameof(SelectedDriveTalent));
            }
        }
        public ICharacterCreationBonus? SelectedDriveBonus
        {
            get => _selectedDriveBonus;
            set
            {
                _selectedDriveBonus = value;
                BonusSelectionChanged?.Invoke(this, nameof(SelectedDriveBonus));
            }
        }
        public string? DriveBonusDescription
        {
            get => GetDriveBonusDescription();
            set => SetDriveBonusDescription(value);
        }

        public int FortuneBonus => (SelectedDriveBonus is Fortune) ? FORTUNEBONUS : 0;
        private IRandomGenerator RandomGenerator { get; set; }
        public CharacterDriveBuilder(ICharacterDriveListService characterDriveListService, IRandomGenerator randomGenerator)
        {
            DriveListService = characterDriveListService;
            RandomGenerator = randomGenerator;
        }
        public bool IsMissingDriveBonus()
        {
            return SelectedDriveBonus is null
                || SelectedDriveTalent is null
                || (SelectedDriveBonus is CharacterTie characterTie && string.IsNullOrEmpty(characterTie.Description))
                || SelectedCharacterDrive is null;
        }

        public void GenerateRandom()
        {
            SelectedCharacterDrive = DriveListService.DriveList[RandomGenerator.GetRandomInteger(0, DriveListService.DriveList.Count)];
            SelectedDriveBonus = DriveListService.DriveBonuses[RandomGenerator.GetRandomInteger(0, DriveListService.DriveBonuses.Count)];
            var actualDriveTalentList = DriveListService.DriveTalentList[SelectedCharacterDrive.DriveName];
            SelectedDriveTalent = actualDriveTalentList[RandomGenerator.GetRandomInteger(0, actualDriveTalentList.Count)];
        }
        private string? GetDriveBonusDescription()
        {
            if (SelectedDriveBonus is CharacterTie characterTie)
            {
                return characterTie.Description;
            }
            return string.Empty;
        }


        private void SetDriveBonusDescription(string? value)
        {
            if (SelectedDriveBonus is CharacterTie characterTie)
            {
                characterTie.Description = value ?? string.Empty;
            }
        }
    }
}

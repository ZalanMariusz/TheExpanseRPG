using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders
{
    public class CharacterDriveBuilder : ICharacterDriveBuilder
    {
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
                SetDriveBonusDescription(null);
                BonusSelectionChanged?.Invoke(this, nameof(SelectedDriveBonus));
            }
        }
        public string? DriveBonusDescription
        {
            get => GetDriveBonusDescription();
            set => SetDriveBonusDescription(value);
        }

        

        public CharacterDriveBuilder(ICharacterDriveListService characterDriveListService)
        {
            DriveListService = characterDriveListService;
        }
        public bool IsMissingDriveBonus()
        {
            return SelectedDriveBonus is null || SelectedDriveTalent is null || (
                   (SelectedDriveBonus is Relationship && string.IsNullOrEmpty((SelectedDriveBonus as Relationship)!.Description))
                || (SelectedDriveBonus is Membership && string.IsNullOrEmpty((SelectedDriveBonus as Membership)!.Description))
                || (SelectedDriveBonus is Reputation && string.IsNullOrEmpty((SelectedDriveBonus as Reputation)!.Description))
            );
        }

        public void GenerateRandom()
        {
            SelectedCharacterDrive = DriveListService.DriveList[Random.Shared.Next(0, DriveListService.DriveList.Count)];
            SelectedDriveBonus = DriveListService.DriveBonuses[Random.Shared.Next(0, DriveListService.DriveBonuses.Count)];
            SelectedDriveTalent = DriveListService.DriveTalentList[SelectedCharacterDrive.DriveName][Random.Shared.Next(0, DriveListService.DriveTalentList[SelectedCharacterDrive.DriveName].Count)];
        }
        private string? GetDriveBonusDescription()
        {
            if (SelectedDriveBonus is Membership)
            {
                return (SelectedDriveBonus as Membership)?.Description;
            }
            if (SelectedDriveBonus is Relationship)
            {
                return (SelectedDriveBonus as Relationship)?.Description;
            }
            if (SelectedDriveBonus is Reputation)
            {
                return (SelectedDriveBonus as Reputation)?.Description;
            }
            return string.Empty;
        }


        private void SetDriveBonusDescription(string? value)
        {
            if (SelectedDriveBonus is Membership membership)
            {
                membership.Description = value;
            }
            if (SelectedDriveBonus is Relationship relationship)
            {
                relationship.Description = value;
            }
            if (SelectedDriveBonus is Reputation reputation)
            {
                reputation.Description = value;
            }
        }


    }
}

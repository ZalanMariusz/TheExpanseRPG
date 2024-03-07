using System.Collections.Generic;
using System.Linq;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class DrivesViewModel : CharacterCreationViewModelBase
    {
        public ICharacterDriveListService DriveListService { get; }
        public List<CharacterDrive> DriveListPartOne { get; }
        public List<CharacterDrive> DriveListPartTwo { get; }
        public List<ICharacterCreationBonus> DriveBonuses => DriveListService.DriveBonuses;
        public List<CharacterTalent>? DriveTalents => DriveListService.GetDriveTalentOptions(SelectedCharacterDrive?.DriveName);
        public bool IsDescriptionRequired => IsDescriptionRequiredBasedOnBonus();
        public CharacterDrive? SelectedCharacterDrive
        {
            get => CharacterCreationService!.DriveBuilder.SelectedCharacterDrive;
            set
            {
                CharacterCreationService.DriveBuilder.SelectedCharacterDrive = value;
                OnPropertyChanged();
            }
        }

        public ICharacterCreationBonus? SelectedDriveBonus
        {
            get => CharacterCreationService!.DriveBuilder.SelectedDriveBonus;
            set
            {
                CharacterCreationService!.DriveBuilder.SelectedDriveBonus = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDescriptionRequired));
                OnPropertyChanged(nameof(DriveBonusDescription));
            }
        }

        public CharacterTalent? SelectedTalent
        {
            get => CharacterCreationService!.DriveBuilder.SelectedDriveTalent;
            set
            {
                CharacterCreationService!.DriveBuilder.SelectedDriveTalent = value;
                OnPropertyChanged();
            }
        }
        public string? DriveBonusDescription
        {
            get => CharacterCreationService.DriveBuilder.DriveBonusDescription;
            set => CharacterCreationService.DriveBuilder.DriveBonusDescription = value;
        }
        private bool _isSelectionLocked;
        public bool IsSelectionLocked
        {
            get => _isSelectionLocked;
            set
            {
                _isSelectionLocked = value;
                OnPropertyChanged();
            }
        }
        public DrivesViewModel(ScopedServiceFactory scopedServiceFactory, ICharacterDriveListService characterDriveListService)
        {
            CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
            DriveListService = characterDriveListService;

            int listPartitionAtIndex = DriveListService.DriveList.Count / 2;

            DriveListPartOne = new(DriveListService.DriveList.Take(listPartitionAtIndex));
            DriveListPartTwo = new(DriveListService.DriveList.TakeLast(DriveListService.DriveList.Count - listPartitionAtIndex));

            CharacterCreationService.DriveBuilder.DriveSelectionChanged += (sender, EventArgs) => OnPropertyChanged(nameof(DriveTalents));
        }
        private bool IsDescriptionRequiredBasedOnBonus()
        {
            return SelectedDriveBonus is Relationship || SelectedDriveBonus is Membership || SelectedDriveBonus is Reputation;
        }
    }
}

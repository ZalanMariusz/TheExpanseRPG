using System;
using System.Collections.ObjectModel;
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
        //public ObservableCollection<CharacterDrive> DriveList { get; }
        public ObservableCollection<CharacterDrive> DriveListPartOne { get; }
        public ObservableCollection<CharacterDrive> DriveListPartTwo { get; }
        public ObservableCollection<ICharacterCreationBonus> DriveBonuses { get; }
        private ObservableCollection<CharacterTalent>? _driveTalents;
        public ObservableCollection<CharacterTalent>? DriveTalents { get { return _driveTalents; } set { _driveTalents = value; OnPropertyChanged(); } }

        public CharacterDrive? SelectedCharacterDrive
        {
            get { return CharacterCreationService!.SelectedCharacterDrive; }
            set
            {
                CharacterCreationService.SelectedCharacterDrive = value;
                OnPropertyChanged();
                RefreshDriveTalents();
                EventAggregator.PublishLinkedPropertyChanged("SelectedDrive");
            }
        }

        public ICharacterCreationBonus? SelectedDriveBonus
        {
            get { return CharacterCreationService!.SelectedDriveBonus; }
            set
            {
                CharacterCreationService!.SelectedDriveBonus = value;
                OnPropertyChanged();
            }
        }

        public CharacterTalent? SelectedTalent
        {
            get { return CharacterCreationService!.SelectedDriveTalent; }
            set
            {
                CharacterCreationService!.SelectedDriveTalent = value;
                OnPropertyChanged();
            }
        }

        public DrivesViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
            DriveListService = CharacterCreationService.CharacterDriveListService;
            
            int listPartitionAtIndex = DriveListService.DriveList.Count / 2;
            
            //DriveList = new(DriveListService.DriveList);
            DriveListPartOne = new(DriveListService.DriveList.Take(listPartitionAtIndex));
            DriveListPartTwo = new(DriveListService.DriveList.TakeLast(DriveListService.DriveList.Count-listPartitionAtIndex));

            DriveBonuses = new(DriveListService.DriveBonuses);
        }

        private void RefreshDriveTalents()
        {
            DriveTalents = new ObservableCollection<CharacterTalent>(DriveListService.GetDriveTalentOptions(SelectedCharacterDrive?.DriveName));
        }
        private bool _isSelectionLocked;
        public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }
    }
}

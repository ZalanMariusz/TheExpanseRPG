using System;
using System.Collections.Generic;
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
            get { return CharacterCreationService!.DriveBuilder.SelectedCharacterDrive; }
            set
            {
                CharacterCreationService.DriveBuilder.SelectedCharacterDrive = value;
                OnPropertyChanged();
                //RefreshDriveTalents();
            }
        }

        public ICharacterCreationBonus? SelectedDriveBonus
        {
            get { return CharacterCreationService!.DriveBuilder.SelectedDriveBonus; }
            set
            {
                CharacterCreationService!.DriveBuilder.SelectedDriveBonus = value;
                OnPropertyChanged();
            }
        }

        public CharacterTalent? SelectedTalent
        {
            get { return CharacterCreationService!.DriveBuilder.SelectedDriveTalent; }
            set
            {
                CharacterCreationService!.DriveBuilder.SelectedDriveTalent = value;
                OnPropertyChanged();
            }
        }

        public DrivesViewModel(ScopedServiceFactory scopedServiceFactory, ICharacterDriveListService characterDriveListService)
        {
            
            CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
            DriveListService = characterDriveListService;

            int listPartitionAtIndex = DriveListService.DriveList.Count / 2;
            
            //DriveList = new(DriveListService.DriveList);
            DriveListPartOne = new(DriveListService.DriveList.Take(listPartitionAtIndex));
            DriveListPartTwo = new(DriveListService.DriveList.TakeLast(DriveListService.DriveList.Count-listPartitionAtIndex));

            RefreshDriveTalents();
            DriveBonuses = new(DriveListService.DriveBonuses);
            CharacterCreationService.DriveBuilder.DriveSelectionChanged += (sender, EventArgs) => RefreshDriveTalents();
        }

        private void RefreshDriveTalents()
        {
            DriveTalents = new ObservableCollection<CharacterTalent>(DriveListService.GetDriveTalentOptions(SelectedCharacterDrive?.DriveName));
        }
        private bool _isSelectionLocked;
        public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }
    }
}

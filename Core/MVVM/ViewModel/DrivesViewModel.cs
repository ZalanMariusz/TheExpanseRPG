﻿using System.Collections.ObjectModel;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class DrivesViewModel : CharacterCreationViewModelBase
    {
        public CharacterDriveListService DriveListService { get; }
        public ObservableCollection<CharacterDrive> DriveList { get; }
        public ObservableCollection<IDriveBonus> DriveBonuses { get; }
        private ObservableCollection<CharacterTalent>? _driveTalents;
        public ObservableCollection<CharacterTalent>? DriveTalents { get { return _driveTalents; } set { _driveTalents = value; OnPropertyChanged(); } }

        public CharacterDrive? ChosenCharacterDrive
        {
            get { return CharacterCreationService!.ChosenDrive; }
            set
            {
                CharacterCreationService!.ChosenDrive = value;
                OnPropertyChanged();
                RefreshDriveTalents();
            }
        }

        public IDriveBonus? ChosenDriveBonus
        {
            get { return CharacterCreationService!.ChosenDriveBonus; }
            set
            {
                CharacterCreationService!.ChosenDriveBonus = value;
                OnPropertyChanged();
            }
        }

        public CharacterTalent? ChosenFocus
        {
            get { return CharacterCreationService!.ChosenDriveFocus; }
            set
            {
                CharacterCreationService!.ChosenDriveFocus = value;
                OnPropertyChanged();
            }

        }

        public DrivesViewModel(CharacterDriveListService characterDriveListService)
        {
            DriveListService = characterDriveListService;
            DriveList = new(DriveListService.DriveList);
            DriveBonuses = new(DriveListService.DriveBonuses);
        }

        private void RefreshDriveTalents()
        {
            DriveTalents = new ObservableCollection<CharacterTalent>(DriveListService.GetDriveTalentOptions(ChosenCharacterDrive?.DriveName));
        }
    }
}

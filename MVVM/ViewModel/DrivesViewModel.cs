using System.Collections.ObjectModel;
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
        public ObservableCollection<CharacterDrive> DriveList { get; }
        public ObservableCollection<IDriveBonus> DriveBonuses { get; }
        private ObservableCollection<CharacterTalent>? _driveTalents;
        public ObservableCollection<CharacterTalent>? DriveTalents { get { return _driveTalents; } set { _driveTalents = value; OnPropertyChanged(); } }

        public CharacterDrive? ChosenCharacterDrive
        {
            get { return CharacterCreationService!.ChosenCharacterDrive; }
            set
            {
                CharacterCreationService!.ChosenCharacterDrive = value;
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

        public DrivesViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
            DriveListService = CharacterCreationService.CharacterDriveListService;
            DriveList = new(DriveListService.DriveList);
            DriveBonuses = new(DriveListService.DriveBonuses);
        }

        private void RefreshDriveTalents()
        {
            DriveTalents = new ObservableCollection<CharacterTalent>(DriveListService.GetDriveTalentOptions(ChosenCharacterDrive?.DriveName));
        }
    }
}

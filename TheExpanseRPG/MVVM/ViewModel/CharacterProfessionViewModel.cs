using System.Collections.ObjectModel;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class CharacterProfessionViewModel : CharacterCreationViewModelBase
    {
        public ObservableCollection<CharacterProfession> OutsiderProfessions { get; }
        public ObservableCollection<CharacterProfession> LowerclassProfessions { get; }
        public ObservableCollection<CharacterProfession> MiddleclassProfessions { get; }
        public ObservableCollection<CharacterProfession> UpperclassProfessions { get; }
        public CharacterTalent? SelectedTalent
        {
            get { return CharacterCreationService.ProfessionBuilder.SelectedProfessionTalent; }
            set
            {
                CharacterCreationService.ProfessionBuilder.SelectedProfessionTalent = value;
                OnPropertyChanged();
            }
        }

        public AbilityFocus? SelectedFocus
        {
            get { return CharacterCreationService.ProfessionBuilder.SelectedProfessionFocus; }
            set
            {
                CharacterCreationService.ProfessionBuilder.SelectedProfessionFocus = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasProfessionFocusConflict));
            }
        }

        public CharacterProfession? SelectedProfession
        {
            get
            {
                return CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession;
            }
            set
            {
                if (!(value == null && CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession != null))
                {
                    CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedFocus));
                    OnPropertyChanged(nameof(SelectedTalent));
                }
            }
        }

        public bool HasProfessionFocusConflict
        {
            get { return CharacterCreationFocusConflictChecker.HasProfessionConflict(); }
        }
        private bool _isSelectionLocked;
        public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }
        public CharacterProfessionViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();

            OutsiderProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.ProfessionBuilder.OutsiderProfessions);
            LowerclassProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.ProfessionBuilder.LowerclassProfessions);
            MiddleclassProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.ProfessionBuilder.MiddleclassProfessions);
            UpperclassProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.ProfessionBuilder.UpperclassProfessions);
        }
    }
}

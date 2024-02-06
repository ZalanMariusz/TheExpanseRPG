﻿using System.Collections.ObjectModel;
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
            get { return CharacterCreationService.SelectedProfessionTalent; }
            set
            {
                CharacterCreationService.SelectedProfessionTalent = value;
                OnPropertyChanged();
            }
        }

        public AbilityFocus? SelectedFocus
        {
            get { return CharacterCreationService.SelectedProfessionFocus; }
            set
            {
                CharacterCreationService.SelectedProfessionFocus = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasProfessionFocusConflict));
            }
        }

        public CharacterProfession? SelectedProfession
        {
            get
            {
                return CharacterCreationService.SelectedCharacterProfession;
            }
            set
            {
                if (!(value == null && CharacterCreationService.SelectedCharacterProfession != null))
                {
                    CharacterCreationService.SelectedCharacterProfession = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedFocus));
                    OnPropertyChanged(nameof(SelectedTalent));
                }
            }
        }

        public bool HasProfessionFocusConflict
        {
            get { return CharacterCreationService.HasProfessionConflict(); }
        }
        private bool _isSelectionLocked;
        public bool IsSelectionLocked { get => _isSelectionLocked; set { _isSelectionLocked = value; OnPropertyChanged(); } }
        public CharacterProfessionViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();

            OutsiderProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.OutsiderProfessions);
            LowerclassProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.LowerclassProfessions);
            MiddleclassProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.MiddleclassProfessions);
            UpperclassProfessions = new ObservableCollection<CharacterProfession>(CharacterCreationService.UpperclassProfessions);
            //EventAggregator_ToDelete.Subscribe(this);
        }
    }
}

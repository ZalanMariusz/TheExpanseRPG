using System.Collections.ObjectModel;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterProfessionViewModel : CharacterCreationViewModelBase
    {
        public CharacterProfessionListService ProfessionListService { get; }
        public ObservableCollection<CharacterProfession> OutsiderProfessions { get; }
        public ObservableCollection<CharacterProfession> LowerclassProfessions { get; }
        public ObservableCollection<CharacterProfession> MiddleclassProfessions { get; }
        public ObservableCollection<CharacterProfession> UpperclassProfessions { get; }
        public CharacterTalent? ChosenTalent
        {
            get { return SelectedProfession?.ChosenTalent; }
            set
            {
                if (SelectedProfession != null)
                {
                    SelectedProfession.ChosenTalent = value; OnPropertyChanged();
                }
            }
        }

        public AbilityFocus? ChosenFocus
        {
            get { return SelectedProfession?.ChosenFocus; }
            set
            {
                if (SelectedProfession != null)
                {
                    SelectedProfession.ChosenFocus = value; OnPropertyChanged();
                }
            }
        }

        public CharacterProfession? SelectedProfession
        {
            get
            {
                return CharacterCreationService.ChosenCharacterProfession;
            }
            set
            {
                if (!(value == null && CharacterCreationService.ChosenCharacterProfession != null))
                {
                    CharacterCreationService.ChosenCharacterProfession = value;
                    OnPropertyChanged();
                }
            }
        }

        public CharacterProfessionViewModel(CharacterProfessionListService professionListService)
        {
            ProfessionListService = professionListService;

            OutsiderProfessions = new ObservableCollection<CharacterProfession>(
                ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Outsider));

            LowerclassProfessions = new ObservableCollection<CharacterProfession>(
                ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Lower));

            MiddleclassProfessions = new ObservableCollection<CharacterProfession>(
                ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Middle));

            UpperclassProfessions = new ObservableCollection<CharacterProfession>(
                ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Upper));
        }
    }
}

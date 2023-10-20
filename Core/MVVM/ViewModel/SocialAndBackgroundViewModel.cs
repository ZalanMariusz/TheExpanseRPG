using System.Collections.ObjectModel;
using System.Linq;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class SocialAndBackgroundViewModel : CharacterCreationViewModelBase
    {
        public CharacterSocialClass? SelectedCharacterSocialClass 
        { 
            get { return CharacterCreationService.CharacterSocialClass; } 
            set { CharacterCreationService.CharacterSocialClass = value; OnPropertyChanged(); RefreshAvailableBackgrounds(); ChangeSelectedSocialClass(); } 
        }
        public CharacterBackGround? SelectedBackground 
        { 
            get { return CharacterCreationService.ChosenCharacterBackground; } 
            set { CharacterCreationService.ChosenCharacterBackground = value; ClearChosenBonuses(); OnPropertyChanged(); } 
        }
        public ICharacterCreationBonus? ChosenBenefit
        {
            get { return CharacterCreationService.ChosenCharacterBackgroundBenefit; }
            set { CharacterCreationService.ChosenCharacterBackgroundBenefit = value; OnPropertyChanged(); }
        }


        public CharacterBackgroundListService BackgroundListService { get; set; }
        private ObservableCollection<CharacterBackGround> _availableBackgrounds;
        public string SocialClassDescription { get; set; }
        public ObservableCollection<CharacterBackGround> AvailableBackgrounds
        {
            get
            {
                return _availableBackgrounds;
            }
            set { _availableBackgrounds = value; }
        }
        //private CharacterBackGround _selectedBackground;



        private void ClearChosenBonuses()
        {
            ChosenBackgroundFocus = null;
            ChosenBackgroundTalent = null;
            OnPropertyChanged(nameof(ChosenBackgroundFocus));
            OnPropertyChanged(nameof(ChosenBackgroundTalent));
        }

        private AbilityFocus? _chosenBackgroundFocus;
        private CharacterTalent? _chosenBackgroundTalent;
        public AbilityFocus? ChosenBackgroundFocus { get { return _chosenBackgroundFocus; } set { _chosenBackgroundFocus = value; OnPropertyChanged(); } }
        public CharacterTalent? ChosenBackgroundTalent { get { return _chosenBackgroundTalent; } set { _chosenBackgroundTalent = value; OnPropertyChanged(); } }
        private string? _selectedTalentDescription;
        public string? SelectedCharacterSocialClassDescription { get { return _selectedTalentDescription; } set { _selectedTalentDescription = value; OnPropertyChanged(); } }
        public RelayCommand ChangeSelectedSocialClassCommand { get; set; }

        public SocialAndBackgroundViewModel(ScopedServiceFactory scopedServiceFactory, CharacterBackgroundListService characterBackgroundListService)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
            BackgroundListService = characterBackgroundListService;

            CharacterCreationService.PopulatePossibleSocialClass();
            ChangeSelectedSocialClassCommand = new RelayCommand(o => true, o => ChangeSelectedSocialClass());
        }
        private void RefreshAvailableBackgrounds()
        {
            AvailableBackgrounds = new ObservableCollection<CharacterBackGround>(
                BackgroundListService.CharacterBackgroundList.Where(x => x.MainSocialClass == SelectedCharacterSocialClass));
            OnPropertyChanged(nameof(AvailableBackgrounds));
        }
        public void ChangeSelectedSocialClass()
        {
            if (SelectedCharacterSocialClass != null)
            {
                switch (SelectedCharacterSocialClass)
                {
                    case CharacterSocialClass.Outsider:
                        SelectedCharacterSocialClassDescription = "More of a non-social class, outsiders tend to be outcasts, criminals, or non-conformists who can’t or won’t live according to society’s customs. They often lack access to things other people take for granted and learn to get by on their own, some times forming their own support networks and structures outside of those of mainstream society. Some outsiders reject the mainstream by choice, but in many cases, outsiders are pushed out by society’s biases.";
                        break;
                    case CharacterSocialClass.Lower:
                        SelectedCharacterSocialClassDescription = "Hard, usually physical, labor and precarious employment tend to rule the lives of lower class characters. Still, that work is often all that separates them from becoming outsiders, so they cling to it. Lower class characters often depend on family and friends to help keep them out of utter poverty. They might live in failing industrial areas, inner city slums, or hardscrabble farms. In all cases, they make do with what is available and find ways to stretch out resources until the next payday or job comes along.";
                        break;
                    case CharacterSocialClass.Middle:
                        SelectedCharacterSocialClassDescription = "A measure of comfort and security comes with the middle class. A steady job, often skilled labor or “white collar,” supplies the means to afford a few luxuries or non-essentials. Middle class characters might start off as a bit insular. They often separate themselves from the struggles of the lower social classes, focusing on the climb towards upper class status. Sometimes that climb leads to a slip. They tumble down to the lower class or even become outsiders. Some settle for stability instead, and prefer not to rock the boat.";
                        break;
                    case CharacterSocialClass.Upper:
                        SelectedCharacterSocialClassDescription = "Upper class characters sit at a society’s summit where they rarely need to worry about resources, except, of course, when they want more. Their concerns are often focused on the responsibilities and privileges associated with their status. Some are born into upper class privilege, inheriting wealth and opportunity, while others worked their way up to join the elite. In some societies, it’s almost impossible to work your way to upper class status, and even if you do, you might get less respect compared to hereditary “old money” peers.";
                        break;
                }
                OnPropertyChanged(nameof(SelectedCharacterSocialClassDescription));
            }
        }
    }
}

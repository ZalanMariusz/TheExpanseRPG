using System;
using System.Diagnostics;
using System.Printing;
using System.Windows;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class OriginSelectViewModel : ViewModelBase
    {
       
        public OriginSelectViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NextPage = new RelayCommand(o => SelectedOrigin != null, o => NavigationService.NavigateToInnerView<AttributeRollViewModel>());
        }

        private CharacterOrigin? _selectedOrigin;
        public CharacterOrigin? SelectedOrigin
        {
            get { return _selectedOrigin; }
            set
            {
                _selectedOrigin = value; OnPropertyChanged(); ChangeOriginDescription();
            }
        }

        public string? _selectedOriginDescription;
        public string? SelectedOriginDescription
        {
            get { return _selectedOriginDescription; }
            set { _selectedOriginDescription = value; OnPropertyChanged(); }
        }
        public RelayCommand NextPage { get; set; }
        private void ChangeOriginDescription()
        {
            switch (SelectedOrigin)
            {
                case CharacterOrigin.Earth:
                    SelectedOriginDescription = "With a population of some 30 billion, many Earthers are unemployed and live on government-provided Basic Assistance (generally known as just “Basic”) which provides for their essential food, housing, and medical needs, but little else. You are likely one of the few to leave Earth to find a new life elsewhere.";
                    break;
                case CharacterOrigin.Mars:
                    SelectedOriginDescription = "Born in the Martian Congressional Republic, your life has been influenced by the Martian dream: to terraform the Red Planet into a lush and life-sustaining garden. Like the generations before you, you know that you will likely never see the completion of this work in your lifetime.";
                    break;
                case CharacterOrigin.Belt:
                    SelectedOriginDescription = "You were born and raised in the Black, on a station or ship, and have lived most, if not all, of your life out in the Belt or beyond. Separated from death by nothing more than basic support systems your whole life, you have learned to be cautious and aware of your environment.";
                    break;
            }
        }
    }
}

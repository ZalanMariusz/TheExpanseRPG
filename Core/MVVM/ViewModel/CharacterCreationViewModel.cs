using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterCreationViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand ShowTalentListCommand { get; set; }
        public CharacterCreationViewModel(INavigationService navigationService, CharacterCreationService characterCreationService)
        {
            NavigationService = navigationService;
            CharacterCreationService = characterCreationService;

            NavigateToInnerView<OriginSelectViewModel>();

            NavigateToOriginSelect = new RelayCommand(o => true, o => NavigateToInnerView<OriginSelectViewModel>());
            NavigateToAttributeRoll = new RelayCommand(o => true, o => NavigateToInnerView<AbilityRollViewModel>());
            NavigateToSocialAndBackground = new RelayCommand(o => true, o => NavigateToInnerView<SocialAndBackgroundViewModel>());
            NavigateToCharacterProfessions = new RelayCommand(o => true, o => NavigateToInnerView<CharacterProfessionViewModel>());
            NavigateToDrives = new RelayCommand(o => true, o => NavigateToInnerView<DrivesViewModel>());

            NavigateBackToMain = new RelayCommand(o => true, ExecNavigationToPlayerMain);
            ShowTalentListCommand = new RelayCommand(o => true, ShowTalenList);
            OpenModals = new List<Control>();
        }
        public RelayCommand NavigateToOriginSelect { get; set; }
        public RelayCommand NavigateToAttributeRoll { get; set; }
        public RelayCommand NavigateToSocialAndBackground { get; set; }
        public RelayCommand NavigateToCharacterProfessions { get; set; }
        public RelayCommand NavigateToDrives { get; set; }
        public RelayCommand NavigateBackToMain { get; set; }

        private void ExecNavigationToPlayerMain(object sender)
        {
            NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender, true);
        }
        private void ShowTalenList(object sender)
        {
            NavigationService.NavigateToModal<TalentListWindow>(this, false);
        }
    }
}

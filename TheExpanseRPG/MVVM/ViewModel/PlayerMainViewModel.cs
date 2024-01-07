using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class PlayerMainViewModel : ViewModelBase
    {
        public RelayCommand NavigateToSplash { get; set; }
        public RelayCommand NavigateToCreateNewCharacter { get; set; }
        public PlayerMainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigateToSplash = new RelayCommand(o => true, ExecNavigationToSplash);
            NavigateToCreateNewCharacter = new RelayCommand(o => true, ExecNavigateToCreateNewCharacter);
        }
        private void ExecNavigationToSplash(object sender)
        {
            NavigationService.NavigateToNewWindow<WelcomeSplashWindow>((Window)sender);
        }

        private void ExecNavigateToCreateNewCharacter(object sender)
        {
            NavigationService.NavigateToNewWindow<CharacterCreationWindow>((Window)sender);
        }
    }
}

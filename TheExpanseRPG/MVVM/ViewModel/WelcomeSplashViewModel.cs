using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class WelcomeSplashViewModel : ViewModelBase
    {
        public RelayCommand NavigateToPlayerMain { get; set; }
        public RelayCommand NavigateToGmMain { get; set; }
        public WelcomeSplashViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigateToPlayerMain = new RelayCommand(o => true, ExecNavigationToPlayerMain);
            NavigateToGmMain = new RelayCommand(o => true, ExecNavigationToGmMain);
        }
        private void ExecNavigationToPlayerMain(object sender)
        {
            NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender);
        }
        private void ExecNavigationToGmMain(object sender)
        {
            NavigationService.NavigateToNewWindow<GmMainWindow>((Window)sender);
        }

    }
}

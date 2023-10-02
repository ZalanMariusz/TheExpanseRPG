using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterCreationViewModel : ViewModelBase
    {
        //private OriginSelectViewModel _originSelectViewModel;
        //private AttributeRollViewModel _attributeRollViewModel;
        //private SocialAndBackgroundViewModel _socialAndBackgroundViewModel;
        //private CharacterProfessionViewModel _characterProfessionViewModel;
        //private DriveAndGoalsViewModel _driveAndGolesViewModel;

        public CharacterCreationViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.NavigateToInnerView<OriginSelectViewModel>();

            NavigateToOriginSelect = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<OriginSelectViewModel>());
            NavigateToAttributeRoll = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<AttributeRollViewModel>());
            NavigateToSocialAndBackground = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<SocialAndBackgroundViewModel>());
            NavigateToCharacterProfessions = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<CharacterProfessionViewModel>());
            NavigateToDriveAndGoals = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<DriveAndGoalsViewModel>());

            NavigateBackToMain = new RelayCommand(o => true, ExecNavigationToPlayerMain);
        }
        public RelayCommand NavigateToOriginSelect { get; set; }
        public RelayCommand NavigateToAttributeRoll { get; set; }
        public RelayCommand NavigateToSocialAndBackground { get; set; }
        public RelayCommand NavigateToCharacterProfessions { get; set; }
        public RelayCommand NavigateToDriveAndGoals { get; set; }
        public RelayCommand NavigateBackToMain { get; set; }
        private void ExecNavigationToPlayerMain(object sender)
        {
            NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender,true);
        }

    }
}

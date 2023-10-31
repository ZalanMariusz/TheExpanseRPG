using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
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

            NavigateToCharacterCreationStep<OriginSelectViewModel>();

            NavigateToOriginSelect = new RelayCommand(o => true, o => NavigateToCharacterCreationStep<OriginSelectViewModel>());
            NavigateToAttributeRoll = new RelayCommand(o => true, o => NavigateToCharacterCreationStep<AbilityRollViewModel>());
            NavigateToSocialAndBackground = new RelayCommand(o => true, o => NavigateToCharacterCreationStep<SocialAndBackgroundViewModel>());
            NavigateToCharacterProfessions = new RelayCommand(o => true, o => NavigateToCharacterCreationStep<CharacterProfessionViewModel>());
            NavigateToDrives = new RelayCommand(o => true, o => NavigateToCharacterCreationStep<DrivesViewModel>());

            NavigateBackToMain = new RelayCommand(o => true, ExecNavigationToPlayerMain);
            ShowTalentListCommand = new RelayCommand(o => true, ShowTalenList);
            OpenModals = new List<Control>();
        }

        private void ShowTalenList(object sender)
        {
            NavigationService.NavigateToModal<TalentListWindow>(this, false);
        }

        private void NavigateToCharacterCreationStep<TViewModel>() where TViewModel : CharacterCreationViewModelBase
        {
            MapServiceFromViewModel();
            TViewModel? viewModelToNavigateTo = (TViewModel?)InnerViewModels.FirstOrDefault(x => x.GetType() == typeof(TViewModel));
            NavigationService.NavigateToInnerView<TViewModel>(this);
            MapViewModelFromService();
        }

        private void MapServiceFromViewModel()
        {
            if (CurrentInnerViewModel != null)
            {
                CharacterCreationService = ((ICharacterCreationViewModel)CurrentInnerViewModel).CharacterCreationService;
            }
        }
        private void MapViewModelFromService()
        {
            if (CurrentInnerViewModel != null)
            {
                ((ICharacterCreationViewModel)CurrentInnerViewModel).CharacterCreationService = CharacterCreationService;
            }
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
    }
}

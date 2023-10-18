using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterCreationViewModel : CharacterCreationViewModelBase
    {
        //public CharacterCreationService CharacterCreationService { get; set; }
        public RelayCommand ShowTalentListCommand { get; set; }
        private readonly ScopedServiceFactory _scopeFactory;
        public CharacterCreationViewModel(INavigationService navigationService, ScopedServiceFactory serviceScopeFactory)
        {
            NavigationService = navigationService;
            _scopeFactory = serviceScopeFactory;
            CharacterCreationService = (CharacterCreationService)_scopeFactory.GetScopedService<CharacterCreationService>();

            NavigationService.NavigateToInnerView<OriginSelectViewModel>(this);

            NavigateToOriginSelect = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<OriginSelectViewModel>(this));
            NavigateToAttributeRoll = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<AbilityRollViewModel>(this));
            NavigateToSocialAndBackground = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<SocialAndBackgroundViewModel>(this));
            NavigateToCharacterProfessions = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<CharacterProfessionViewModel>(this));
            NavigateToDriveAndGoals = new RelayCommand(o => true, o => NavigationService.NavigateToInnerView<DriveAndGoalsViewModel>(this));

            NavigateBackToMain = new RelayCommand(o => true, ExecNavigationToPlayerMain);
            ShowTalentListCommand = new RelayCommand(o => true, ShowTalenList);
            OpenModals = new List<Control>();
        }

        private void ShowTalenList(object sender)
        {
            NavigationService.NavigateToModal<TalentListWindow>(this, false);
        }

        public RelayCommand NavigateToOriginSelect { get; set; }
        public RelayCommand NavigateToAttributeRoll { get; set; }
        public RelayCommand NavigateToSocialAndBackground { get; set; }
        public RelayCommand NavigateToCharacterProfessions { get; set; }
        public RelayCommand NavigateToDriveAndGoals { get; set; }
        public RelayCommand NavigateBackToMain { get; set; }
        private void ExecNavigationToPlayerMain(object sender)
        {
            NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender, true);
            foreach (Control openModal in OpenModals)
            {
                if (openModal is Window window)
                {
                    window.Close();
                }
            }
            OpenModals.Clear();
            _scopeFactory.DisposeScope<CharacterCreationService>();
        }
        //public override void AddInnerViewModel(IViewModelBase viewModel)
        //{
        //    base.AddInnerViewModel(viewModel);
        //    if (viewModel is ICharacterCreationViewModel)
        //    {
        //        ((ICharacterCreationViewModel)viewModel).AddCharacterCreationService(CharacterCreationService);
        //    }
        //}
    }
}

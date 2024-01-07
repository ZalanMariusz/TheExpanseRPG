using System.Linq;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class CharacterCreationViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand ShowTalentListCommand { get; set; }
        private readonly ScopedServiceFactory _scopedServiceFactory;
        public CharacterOrigin? SelectedOrigin
        {
            get { return CharacterCreationService.SelectedCharacterOrigin; }
        }
        public bool HasOriginSelectionConflict
        {
            get { return CharacterCreationService.HasOriginConflict(); }
        }
        public bool HasSocialOrBackgroundSelectionConflict
        {
            get { return CharacterCreationService.HasBackgroundConflict(); }
        }
        public bool HasProfessionSelectionConflict
        {
            get { return CharacterCreationService.HasProfessionConflict(); }
        }
        public CharacterSocialClass? SelectedSocialClass
        {
            get { return CharacterCreationService.SelectedCharacterSocialClass; }
        }
        public CharacterBackGround? SelectedBackground
        {
            get { return CharacterCreationService.SelectedCharacterBackground; }
        }
        public CharacterProfession? SelectedProfession
        {
            get { return CharacterCreationService.SelectedCharacterProfession; }
        }
        public CharacterDrive? SelectedDrive
        {
            get { return CharacterCreationService.SelectedCharacterDrive; }
        }
        public string OriginConflicts
        {
            get
            {
                return HasOriginSelectionConflict ? string.Join(", ", CharacterCreationService.GetOriginFocusConflicts().ToArray()) : string.Empty;
            }
        }
        public string SocialOrBackgroundConflicts
        {
            get
            {
                return HasSocialOrBackgroundSelectionConflict ? string.Join(", ", CharacterCreationService.GetBackgroundFocusConflicts().Union(CharacterCreationService.GetBackgroundBenefitConflicts()).ToArray()) : string.Empty;
            }
        }
        public string ProfessionConflicts
        {
            get
            {
                return HasProfessionSelectionConflict ? string.Join(", ", CharacterCreationService.GetProfessionFocusConflicts().ToArray()) : string.Empty;
            }
        }
        public CharacterCreationViewModel(INavigationService navigationService, ScopedServiceFactory scopedServiceFactory)
        {
            _scopedServiceFactory = scopedServiceFactory;
            NavigationService = navigationService;
            CharacterCreationService = (CharacterCreationService)_scopedServiceFactory.GetScopedService<CharacterCreationService>();

            NavigateToView<OriginSelectViewModel>();

            NavigateToOriginSelect = new(o => true, o => NavigateToView<OriginSelectViewModel>());
            NavigateToAttributeRoll = new(o => true, o => NavigateToView<AbilityRollViewModel>());
            NavigateToSocialAndBackground = new(o => true, o => NavigateToView<SocialAndBackgroundViewModel>());
            NavigateToCharacterProfessions = new(o => true, o => NavigateToView<CharacterProfessionViewModel>());
            NavigateToDrives = new(o => true, o => NavigateToView<DrivesViewModel>());
            NavigateToCharacterFinalization = new(o => true, o => NavigateToView<CharacterFinalizationViewModel>());

            NavigateBackToMain = new RelayCommand(o => true, ExecNavigationToPlayerMain);
            ShowTalentListCommand = new RelayCommand(o => true, o => ShowTalenList());
            OpenModals = new();

            EventAggregator.LinkedPropertyChanged += (sender, propertyName) =>
            {
                if (propertyName is "SelectedOrigin"
                or "SelectedSocialClass"
                or "SelectedBackground"
                or "SelectedProfession"
                or "SelectedDrive"
                or "HasOriginSelectionConflict"
                or "HasSocialOrBackgroundSelectionConflict"
                or "HasProfessionSelectionConflict"
                or "OriginConflicts"
                or "SocialOrBackgroundConflicts"
                or "ProfessionConflicts"
                )
                {
                    OnPropertyChanged(propertyName);
                }
            };

        }
        public RelayCommand NavigateToOriginSelect { get; set; }
        public RelayCommand NavigateToAttributeRoll { get; set; }
        public RelayCommand NavigateToSocialAndBackground { get; set; }
        public RelayCommand NavigateToCharacterProfessions { get; set; }
        public RelayCommand NavigateToDrives { get; set; }
        public RelayCommand NavigateBackToMain { get; set; }
        public RelayCommand NavigateToCharacterFinalization { get; set; }


        private void ExecNavigationToPlayerMain(object sender)
        {
            _scopedServiceFactory.DisposeScope<CharacterCreationService>();
            NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender, true);
            EventAggregator.UnSubscribeAll();
        }
        private void ShowTalenList()
        {
            NavigationService.NavigateToModal<TalentListWindow>(this, false);
        }

        private void NavigateToView<T>() where T : CharacterCreationViewModelBase
        {
            NavigateToInnerView<T>();
        }
    }
}

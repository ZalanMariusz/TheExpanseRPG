using System;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;

public class CharacterCreationViewModel : CharacterCreationViewModelBase
{


    private readonly ScopedServiceFactory _scopedServiceFactory;

    public CharacterOrigin? SelectedOrigin => CharacterCreationService.SelectedCharacterOrigin;
    public bool HasOriginSelectionConflict => CharacterCreationService.HasOriginConflict();
    public bool HasSocialOrBackgroundSelectionConflict => CharacterCreationService.HasBackgroundConflict();
    public bool HasProfessionSelectionConflict => CharacterCreationService.HasProfessionConflict();
    public CharacterSocialClass? SelectedSocialClass => CharacterCreationService.SelectedCharacterSocialClass;
    public CharacterBackGround? SelectedBackground => CharacterCreationService.SelectedCharacterBackground;
    public CharacterProfession? SelectedProfession => CharacterCreationService.SelectedCharacterProfession;
    public CharacterDrive? SelectedDrive => CharacterCreationService.SelectedCharacterDrive;
    public string OriginConflicts => string.Join(", ", CharacterCreationService.GetOriginFocusConflicts());
    public string ProfessionConflicts => string.Join(", ", CharacterCreationService.GetProfessionFocusConflicts());
    public string SocialOrBackgroundConflicts => AggregateBackgroundConflicts();

    public RelayCommand ShowTalentListCommand { get; set; }
    public RelayCommand ShowFocusListCommand { get; set; }
    public RelayCommand NavigateToOriginSelectCommand { get; set; }
    public RelayCommand NavigateToAttributeRollCommand { get; set; }
    public RelayCommand NavigateToSocialAndBackgroundCommand { get; set; }
    public RelayCommand NavigateToCharacterProfessionsCommand { get; set; }
    public RelayCommand NavigateToDrivesCommand { get; set; }
    public RelayCommand NavigateBackToMainCommand { get; set; }
    public RelayCommand NavigateToCharacterFinalizationCommand { get; set; }

    public CharacterCreationViewModel(INavigationService navigationService, ScopedServiceFactory scopedServiceFactory)
    {
        _scopedServiceFactory = scopedServiceFactory;
        NavigationService = navigationService;
        CharacterCreationService = (CharacterCreationService)_scopedServiceFactory.GetScopedService<CharacterCreationService>();

        NavigateToOriginSelectCommand = new(o => true, o => NavigateToNotifierCharacterCreationStep<OriginSelectViewModel>());
        NavigateToAttributeRollCommand = new(o => true, o => NavigateToInnerView<AbilityRollViewModel>());
        NavigateToSocialAndBackgroundCommand = new(o => true, o => NavigateToNotifierCharacterCreationStep<SocialAndBackgroundViewModel>());
        NavigateToCharacterProfessionsCommand = new(o => true, o => NavigateToNotifierCharacterCreationStep<CharacterProfessionViewModel>());
        NavigateToDrivesCommand = new(o => true, o => NavigateToNotifierCharacterCreationStep<DrivesViewModel>());
        NavigateToCharacterFinalizationCommand = new(o => true, o => NavigateToCharacterFinalizationView());
        NavigateBackToMainCommand = new RelayCommand(o => true, ExecNavigationToPlayerMain);
        ShowTalentListCommand = new RelayCommand(o => true, o => ShowTalenList());
        ShowFocusListCommand = new RelayCommand(o => true, o => ShowFocusList());
        
        
        OpenModals = new();

        CharacterCreationService.OriginChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedOrigin)); };
        CharacterCreationService.OriginChanged += RefreshConflictProperties;

        CharacterCreationService.SocialClassChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedSocialClass)); };

        CharacterCreationService.BackgroundChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedBackground)); };
        CharacterCreationService.SelectedBackgroundBenefitChanged += RefreshConflictProperties;

        CharacterCreationService.SelectedBackgroundFocusChanged += RefreshConflictProperties;

        CharacterCreationService.ProfessionChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedProfession)); };
        CharacterCreationService.ProfessionFocusChanged += RefreshConflictProperties;

        CharacterCreationService.DriveSelectionChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedDrive)); };
        NavigateToOriginSelectCommand.Execute(null);
    }

    private void ShowFocusList()
    {
        NavigationService.NavigateToModal<FocusListWindow>(this, false);
    }

    private string AggregateBackgroundConflicts()
    {
        return string.Join(", ", CharacterCreationService.GetBackgroundFocusConflicts().Union(CharacterCreationService.GetBackgroundBenefitConflicts()));
    }

    private void RefreshConflictProperties(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(HasOriginSelectionConflict));
        OnPropertyChanged(nameof(HasSocialOrBackgroundSelectionConflict));
        OnPropertyChanged(nameof(HasProfessionSelectionConflict));

        OnPropertyChanged(nameof(OriginConflicts));
        OnPropertyChanged(nameof(SocialOrBackgroundConflicts));
        OnPropertyChanged(nameof(ProfessionConflicts));
    }

    private void NavigateToNotifierCharacterCreationStep<TCharacterCreationViewModel>() where TCharacterCreationViewModel : CharacterCreationViewModelBase
    {
        NavigateToInnerView<TCharacterCreationViewModel>();
    }

    private void NavigateToCharacterFinalizationView()
    {
        IViewModelBase? finalizationVM = GetInnerViewModel<CharacterFinalizationViewModel>();
        NavigateToInnerView<CharacterFinalizationViewModel>();
        if (finalizationVM is null) //ensure exactly one subscription
        {
            finalizationVM = GetInnerViewModel<CharacterFinalizationViewModel>();
            (finalizationVM as CharacterFinalizationViewModel)!.CharacterCreated += NavigateBackAfterCreation;
        }
    }

    private void NavigateBackAfterCreation(object? sender, System.EventArgs e)
    {
        NavigateBackToMainCommand.Execute(sender);
    }
    private void ExecNavigationToPlayerMain(object sender)
    {
        _scopedServiceFactory.DisposeScope<CharacterCreationService>();
        NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender, true);
    }
    private void ShowTalenList()
    {
        NavigationService.NavigateToModal<TalentListWindow>(this, false);
    }
}
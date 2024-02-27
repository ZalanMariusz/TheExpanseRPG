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


    private ScopedServiceFactory ScopedServiceFactory { get; }

    public CharacterOrigin? SelectedOrigin => CharacterCreationService.OriginBuilder.SelectedCharacterOrigin;
    public bool HasOriginSelectionConflict => CharacterCreationFocusConflictChecker.HasOriginConflict();
    public bool HasSocialOrBackgroundSelectionConflict => CharacterCreationFocusConflictChecker.HasBackgroundConflict();
    public bool HasProfessionSelectionConflict => CharacterCreationFocusConflictChecker.HasProfessionConflict();
    public CharacterSocialClass? SelectedSocialClass => CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterSocialClass;
    public CharacterBackGround? SelectedBackground => CharacterCreationService.SocialAndBackgroundBuilder.SelectedCharacterBackground;
    public CharacterProfession? SelectedProfession => CharacterCreationService.ProfessionBuilder.SelectedCharacterProfession;
    public CharacterDrive? SelectedDrive => CharacterCreationService.DriveBuilder.SelectedCharacterDrive;
    public string OriginConflicts => string.Join(", ", CharacterCreationFocusConflictChecker.GetOriginFocusConflicts());
    public string ProfessionConflicts => string.Join(", ", CharacterCreationFocusConflictChecker.GetProfessionFocusConflicts());
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
        NavigationService = navigationService;
        ScopedServiceFactory = scopedServiceFactory;
        CharacterCreationService = ScopedServiceFactory.GetScopedService<ICharacterCreationService>();

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

        CharacterCreationService.OriginBuilder.OriginChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedOrigin)); };
        CharacterCreationService.SocialAndBackgroundBuilder.SocialClassChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedSocialClass)); };
        CharacterCreationService.SocialAndBackgroundBuilder.BonusSelectionChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedBackground)); };
        CharacterCreationService.ProfessionBuilder.SelectedProfessionChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedProfession)); };
        CharacterCreationService.DriveBuilder.DriveSelectionChanged += (sender, args) => { OnPropertyChanged(nameof(SelectedDrive)); };

        CharacterCreationService.OriginBuilder.OriginChanged += RefreshConflictProperties;
        CharacterCreationService.SocialAndBackgroundBuilder.BonusSelectionChanged += RefreshConflictProperties;
        CharacterCreationService.ProfessionBuilder.BonusSelectionChanged += RefreshConflictProperties;

        NavigateToOriginSelectCommand.Execute(null);
    }

    private void ShowFocusList()
    {
        NavigationService.NavigateToModal<FocusListWindow>(this, false);
    }

    private static string AggregateBackgroundConflicts()
    {
        return string.Join(", ", CharacterCreationFocusConflictChecker.GetBackgroundFocusConflicts()
            .Union(CharacterCreationFocusConflictChecker.GetBackgroundBenefitConflicts()));
    }

    private void RefreshConflictProperties(object? sender, string? e)
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

    private void NavigateBackAfterCreation(object? sender, EventArgs e)
    {
        NavigateBackToMainCommand.Execute(sender);
    }
    private void ExecNavigationToPlayerMain(object sender)
    {
        ScopedServiceFactory.DisposeScope<ICharacterCreationService>();
        NavigationService.NavigateToNewWindow<PlayerMainWindow>((Window)sender, true);
    }
    private void ShowTalenList()
    {
        NavigationService.NavigateToModal<TalentListWindow>(this, false);
    }
}
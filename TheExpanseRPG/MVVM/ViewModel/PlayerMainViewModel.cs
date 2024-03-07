using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.Services;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;

public class PlayerMainViewModel : ViewModelBase
{
    public RelayCommand NavigateToSplash { get; set; }
    public RelayCommand NavigateToCreateNewCharacter { get; set; }
    public RelayCommand RefreshCommand { get; set; }
    public RelayCommand DeleteCharacterCommand { get; set; }
    public RelayCommand OpenCharacterSheetCommand { get; set; }
    public ObservableCollection<ExpanseCharacter> CharacterList { get; set; }
    private ExpanseCharacter? _selectedCharacter;
    public ExpanseCharacter? SelectedCharacter
    {
        get { return _selectedCharacter; }
        set { _selectedCharacter = value; OnPropertyChanged(); OpenCharacterSheet(null); }
    }

    private ICharacterListService CharacterListService { get; set; }
    private PopupService PopupService { get; set; }
    public PlayerMainViewModel(INavigationService navigationService, ICharacterListService characterListService, PopupService popupService)
    {
        NavigationService = navigationService;
        CharacterListService = characterListService;
        PopupService = popupService;
        CharacterList = new(CharacterListService.GetCharacterList());

        NavigateToSplash = new(o => true, ExecNavigationToSplash);
        NavigateToCreateNewCharacter = new(o => true, ExecNavigateToCreateNewCharacter);
        RefreshCommand = new(o => true, o => OnPropertyChanged(nameof(CharacterList)));
        DeleteCharacterCommand = new(o => true, DeleteCharacter);
        OpenCharacterSheetCommand = new(o => true, OpenCharacterSheet);
        //CharacterList = characterListService.CharacterList;
    }

    private void OpenCharacterSheet(object? param)
    {
        if (param != null)
        {
            SelectedCharacter = (ExpanseCharacter)param;
        }
        else
        {
            if (SelectedCharacter is null)
            {
                SetCurrentInnerViewModel(null);
                OnPropertyChanged(nameof(InnerViewModels));
            }
            else
            {
                NavigationService.NavigateToInnerView<CharacterDetailsViewModel>(this);
                ((CharacterDetailsViewModel)InnerViewModels.First()).Character = SelectedCharacter;
            }
        }
    }

    private void ExecNavigationToSplash(object sender)
    {
        NavigationService.NavigateToNewWindow<WelcomeSplashWindow>((Window)sender, true);
    }

    private void ExecNavigateToCreateNewCharacter(object sender)
    {
        NavigationService.NavigateToNewWindow<CharacterCreationWindow>((Window)sender, true);
    }
    private void DeleteCharacter(object param)
    {
        ExpanseCharacter characterToDelete = (ExpanseCharacter)param;

        if (PopupService.ShowPopup($"Are you sure you want to delete: {characterToDelete.Name}?") == MessageBoxResult.OK)
        {
            if (characterToDelete == SelectedCharacter)
            {
                SelectedCharacter = null;
            }
            File.Delete($".\\Characters\\{characterToDelete.Name}.json");
            CharacterList.Remove(characterToDelete);
            OnPropertyChanged(nameof(CharacterList));
        };

    }
}

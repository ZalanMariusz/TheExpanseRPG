using System.Collections.ObjectModel;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel;

public class FocusListViewModel : ViewModelBase
{
    public ObservableCollection<AbilityFocus> AccurayFocuses { get; }
    public ObservableCollection<AbilityFocus> CommunicationFocuses { get; }
    public ObservableCollection<AbilityFocus> ConstitutionFocuses { get; }
    public ObservableCollection<AbilityFocus> DexterityFocuses { get; }

    public ObservableCollection<AbilityFocus> FightingFocuses { get; }
    public ObservableCollection<AbilityFocus> IntelligenceFocuses { get; }
    public ObservableCollection<AbilityFocus> PerceptionFocuses { get; }
    public ObservableCollection<AbilityFocus> StrengthFocuses { get; }
    public ObservableCollection<AbilityFocus> WillpowerFocuses { get; }


    public FocusListViewModel(IAbilityFocusListService focusListService)
    {
        AccurayFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Accuracy));
        CommunicationFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Communication));
        ConstitutionFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Constitution));
        DexterityFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Dexterity));
        FightingFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Fighting));
        IntelligenceFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Intelligence));
        PerceptionFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Perception));
        StrengthFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Strength));
        WillpowerFocuses = new ObservableCollection<AbilityFocus>(focusListService.GetAbilityFocusList(CharacterAbilityName.Willpower));
    }
}

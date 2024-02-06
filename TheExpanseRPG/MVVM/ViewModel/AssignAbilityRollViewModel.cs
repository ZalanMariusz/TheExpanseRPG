using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class AssignAbilityRollViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand RollAbilityValues { get; set; }
        public RelayCommand ClearAbility { get; set; }
        private ObservableCollection<int?> _assignableAbilityValues = new();
        public ObservableCollection<int?> AssignableAbilityValues { get { return _assignableAbilityValues; } set { _assignableAbilityValues = value; OnPropertyChanged(); } }
        private readonly PopupService _popupService;
        public int? Accuracy { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Constitution { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Fighting { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Communication { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Dexterity { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Intelligence { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Perception { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Strength { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Willpower { get => GetCharacterAbilityValue(); set => AssignAbilityScore(value); }
        public AbilityRollType LastUsedRollType { get => CharacterCreationService.LastUsedRollType; }
        public bool CanAbilityBeReset(string abilityName)
        {
            return CharacterCreationService.SelectedAbilityRollType == CharacterCreationService.LastUsedRollType &&
                GetCharacterAbilityValue(abilityName) is not null;
        }
        public AssignAbilityRollViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
            _popupService = popupService;
            AssignableAbilityValues = new ObservableCollection<int?>();
            RollAbilityValues = new RelayCommand(o => true, o => RollAssignableList());
            ClearAbility = new RelayCommand(o => true, ClearAbilityValue);

            CharacterCreationService.LastUsedRollTypeChanged += (sender, args) => OnPropertyChanged(nameof(LastUsedRollType));
            CharacterCreationService.LastUsedRollTypeChanged += (sender, args) => AssignableAbilityValues.Clear();
        }

        private void AssignAbilityScore(int? newValue, [CallerMemberName] string abilityName = "")
        {
            RefreshAssignableValuesList(abilityName, newValue);
            CharacterCreationService.AssignAbilityScore(abilityName, newValue);
            OnPropertyChanged(abilityName);

        }
        private void ClearAbilityValue(object obj)
        {
            string abilityName = (obj as string)!;
            AssignAbilityScore(null, abilityName);
        }

        public void RollAssignableList()
        {
            if ((CharacterCreationService.RollsShouldBeReset(AbilityRollType.RollAndAssign) &&
                _popupService.ShowPopup(WPFStringResources.PopupRollTypeChangeResetsCurrentConfirm) == MessageBoxResult.OK) ||
                !CharacterCreationService.RollsShouldBeReset(AbilityRollType.RollAndAssign))
            {
                CharacterCreationService.RollAssignableAbilityList();
                CharacterCreationService.ResetAbilities();
                AssignableAbilityValues = new ObservableCollection<int?>(CharacterCreationService.AbilityValuesToAssign);
                foreach (CharacterAbility ability in CharacterCreationService.CharacterAbilityBlock.AbilityList)
                {
                    OnPropertyChanged(ability.AbilityName.ToString());
                }
            }
        }
        private void RefreshAssignableValuesList(string abilityName, int? newValue)
        {
            int? abilityPropertyValue = (int?)GetType().GetProperty(abilityName)!.GetValue(this);
            if (abilityPropertyValue.HasValue)
            {
                AssignableAbilityValues.Add(abilityPropertyValue);
            }
            AssignableAbilityValues.Remove(newValue);
            OnPropertyChanged(nameof(AssignableAbilityValues));
        }

    }
}

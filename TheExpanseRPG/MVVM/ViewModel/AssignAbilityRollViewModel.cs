using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;
using TheExpanseRPG.Services;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class AssignAbilityRollViewModel : CharacterAbilityRollTypeViewModel
    {
        public RelayCommand RollAbilityValues { get; set; }
        public RelayCommand ClearAbility { get; set; }
        private ObservableCollection<int?> _assignableAbilityValues = new();
        public ObservableCollection<int?> AssignableAbilityValues { get { return _assignableAbilityValues; } set { _assignableAbilityValues = value; OnPropertyChanged(); } }
        public new int? Accuracy { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Constitution { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Fighting { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Communication { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Dexterity { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Intelligence { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Perception { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Strength { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public new int? Willpower { get => GetCharacterAbilityValue(); set => AssignAbilityScore(value); }
        public AbilityRollType LastUsedRollType => CharacterCreationService.AbilityBlockBuilder.LastUsedRollType;
        public bool CanAbilityBeReset(string abilityName)
        {
            return CharacterCreationService.AbilityBlockBuilder.SelectedAbilityRollType == CharacterCreationService.AbilityBlockBuilder.LastUsedRollType &&
                GetCharacterAbilityValue(abilityName) is not null;
        }
        public AssignAbilityRollViewModel(ScopedServiceFactory scopedServiceFactory, PopupService popupService) : base(popupService)
        {
            CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
            AssignableAbilityValues = new ObservableCollection<int?>();

            RollAbilityValues = new RelayCommand(o => true, o => RollAssignableList());
            ClearAbility = new RelayCommand(o => true, ClearAbilityValue);

            CharacterCreationService.AbilityBlockBuilder.LastUsedRollTypeChanged += (sender, args) => OnPropertyChanged(nameof(LastUsedRollType));
            CharacterCreationService.AbilityBlockBuilder.LastUsedRollTypeChanged += (sender, args) => AssignableAbilityValues.Clear();
        }

        private void AssignAbilityScore(int? newValue, [CallerMemberName] string abilityName = "")
        {
            RefreshAssignableValuesList(abilityName, newValue);
            CharacterCreationService.AbilityBlockBuilder.AssignAbilityScore(abilityName, newValue);
            OnPropertyChanged(abilityName);

        }
        private void ClearAbilityValue(object obj)
        {
            string abilityName = (obj as string)!;
            AssignAbilityScore(null, abilityName);
        }

        public void RollAssignableList()
        {
            if (!RollsShouldBeReset(AbilityRollType.RollAndAssign) ||
                ShowRollResetPopup() == MessageBoxResult.OK)
            {
                CharacterCreationService.AbilityBlockBuilder.RollAssignableAbilityList();
                AssignableAbilityValues = new(CharacterCreationService.AbilityBlockBuilder.AbilityValuesToAssign);
                NotifyAbilityPropertiesChanged();
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

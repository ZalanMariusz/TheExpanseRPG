using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AssignAbilityRollViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand RollAbilityValues { get; set; }
        public RelayCommand ClearAbility { get; set; }
        private ObservableCollection<int?> _assignableAbilityValues = new();
        public ObservableCollection<int?> AssignableAbilityValues { get { return _assignableAbilityValues; } set { _assignableAbilityValues = value; OnPropertyChanged(); } }

        public int? Accuracy { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Constitution { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Fighting { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Communication { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Dexterity { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Intelligence { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Perception { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Strength { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Willpower { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }

        public AssignAbilityRollViewModel()
        {
            AssignableAbilityValues = new ObservableCollection<int?>();
            RollAbilityValues = new RelayCommand(o => true, o => RollAssignableList());
            ClearAbility = new RelayCommand(o => true, ClearAbilityValue);
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
            CharacterCreationService.RollAssignableAbilityList();
            CharacterCreationService.ClearAbilities(AbilityRollType.RollAndAssign);
            AssignableAbilityValues = new ObservableCollection<int?>(CharacterCreationService.AttributeValuesToAssign);
            foreach (CharacterAbility ability in CharacterCreationService.CharacterAbilityBlock.AbilityList)
            {
                OnPropertyChanged(ability.AbilityName.ToString());
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
        }

    }
}

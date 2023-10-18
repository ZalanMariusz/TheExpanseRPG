using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AssignAbilityRollViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand RollAbilityValues { get; set; }
        public RelayCommand ClearAbility { get; set; }
        private ObservableCollection<int?> _rolledAbilities = new();
        public ObservableCollection<int?> AssignableAbilityValues { get { return _rolledAbilities; } set { _rolledAbilities = value; OnPropertyChanged(); } }

        private readonly ScopedServiceFactory _scopeFactory;

        public int? Accuracy { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Constitution { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Fighting { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Communication { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Dexterity { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Intelligence { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Perception { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Strength { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }
        public int? Willpower { get { return GetCharacterAbilityValue(); } set { AssignAbilityScore(value); } }

        public AssignAbilityRollViewModel(ScopedServiceFactory serviceScopeFactory)
        {
            _scopeFactory = serviceScopeFactory;
            CharacterCreationService = (CharacterCreationService)_scopeFactory.GetScopedService<CharacterCreationService>();

            AssignableAbilityValues = new ObservableCollection<int?>();
            RollAbilityValues = new RelayCommand(o => true, o => RollAssignableList());
            ClearAbility = new RelayCommand(o => true, ClearAbilityValue);
        }

        private void AssignAbilityScore(int? newValue, [CallerMemberName] string abilityName = "")
        {
            RefreshObservableList(abilityName, newValue);
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
            AssignableAbilityValues = new ObservableCollection<int?>(CharacterCreationService.AttributeValuesToAssign);
            foreach (CharacterAbility ability in CharacterCreationService.CharacterAbilityBlock.AbilityList)
            {
                OnPropertyChanged(ability.AbilityName.ToString());
            }
            OnPropertyChanged(nameof(AssignableAbilityValues));
        }
        private void RefreshObservableList(string abilityName, int? newValue)
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

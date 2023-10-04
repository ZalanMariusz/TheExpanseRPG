using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AssignAttributeRollViewModel : ViewModelBase
    {
        public RelayCommand RollAttributeValues { get; set; }
        public RelayCommand ClearAttribute { get; set; }
        private ObservableCollection<int?> _rolledAttributes;
        public ObservableCollection<int?> RolledAttributes { get { return _rolledAttributes; } set { _rolledAttributes = value; OnPropertyChanged(); } }
        public int? Accuracy { get { return _accuracy; } set { SetAttributeValue(ref _accuracy, value); } }
        public int? Constitution { get { return _constitution; } set { SetAttributeValue(ref _constitution, value); } }
        public int? Fighting { get { return _fighting; } set { SetAttributeValue(ref _fighting, value); } }
        public int? Communication { get { return _communication; } set { SetAttributeValue(ref _communication, value); } }
        public int? Dexterity { get { return _dexterity; } set { SetAttributeValue(ref _dexterity, value); } }
        public int? Intelligence { get { return _intelligence; } set { SetAttributeValue(ref _intelligence, value); } }
        public int? Perception { get { return _perception; } set { SetAttributeValue(ref _perception, value); } }
        public int? Strength { get { return _strength; } set { SetAttributeValue(ref _strength, value); } }
        public int? Willpower { get { return _willpower; } set { SetAttributeValue(ref _willpower, value); } }
        private int? _accuracy;
        private int? _constitution;
        private int? _fighting;
        private int? _communication;
        private int? _dexterity;
        private int? _intelligence;
        private int? _perception;
        private int? _strength;
        private int? _willpower;
        public AssignAttributeRollViewModel()
        {
            RolledAttributes = new ObservableCollection<int?>();
            RollAttributeValues = new RelayCommand(o => true, o => RollAttributes());
            ClearAttribute = new RelayCommand(o => true, ClearAttributeValue);
        }

        private void ClearAttributeValue(object obj)
        {
            string attributeName = obj as string;
            PropertyInfo attributeToClear = GetType().GetProperty(attributeName!)!;
            attributeToClear!.SetValue(this, null);
        }

        public void RollAttributes()
        {
            ClearAssignedAttributes();
            RolledAttributes.Clear();
            foreach (var attributeName in Enum.GetNames<CharacterAttributeName>())
            {
                PopulateScoreList();
            }
        }

        private void ClearAssignedAttributes()
        {
            Accuracy = null;
            Constitution = null;
            Fighting = null;
            Communication = null;
            Dexterity = null;
            Intelligence = null;
            Perception = null;
            Strength = null;
            Willpower = null;
        }

        public void PopulateScoreList()
        {
            RollResult rollResult = DiceRollService.Roll3D6();
            int rolledAttributeValue = GetAttributeValueFromRoll(rollResult.GetRollResultSumValue());
            RolledAttributes.Add(rolledAttributeValue);
        }
        private static int GetAttributeValueFromRoll(int rollResult)
        {
            return rollResult switch
            {
                3 => -2,
                4 or 5 => -1,
                6 or 7 or 8 => 0,
                9 or 10 or 11 => 1,
                12 or 13 or 14 => 2,
                15 or 16 or 17 => 3,
                _ => 4,
            };
        }


        private void SetAttributeValue(ref int? oldAttributeValue, int? newAttributeValue, [CallerMemberName] string propertyname = "")
        {
            if (oldAttributeValue != null)
            {
                RolledAttributes.Add(oldAttributeValue);
            }
            oldAttributeValue = newAttributeValue;
            RolledAttributes.Remove(newAttributeValue);
            OnPropertyChanged(propertyname);
        }

    }
}

using System.Linq;
using System.Reflection;
using System;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AllRandomAttributeRollViewModel : ViewModelBase
    {
        public RelayCommand RollAllRandomAbilityCommand { get; set; }
        public int? Accuracy { get { return _accuracy; } set { _accuracy = value; OnPropertyChanged(); } }
        public int? Constitution { get { return _constitution; } set { _constitution = value; OnPropertyChanged(); } }
        public int? Fighting { get { return _fighting; } set { _fighting = value; OnPropertyChanged(); } }
        public int? Communication { get { return _communication; } set { _communication = value; OnPropertyChanged(); } }
        public int? Dexterity { get { return _dexterity; } set { _dexterity = value; OnPropertyChanged(); } }
        public int? Intelligence { get { return _intelligence; } set { _intelligence = value; OnPropertyChanged(); } }
        public int? Perception { get { return _perception; } set { _perception = value; OnPropertyChanged(); } }
        public int? Strength { get { return _strength; } set { _strength = value; OnPropertyChanged(); } }
        public int? Willpower { get { return _willpower; } set { _willpower = value; OnPropertyChanged(); } }
        private int? _accuracy;
        private int? _constitution;
        private int? _fighting;
        private int? _communication;
        private int? _dexterity;
        private int? _intelligence;
        private int? _perception;
        private int? _strength;
        private int? _willpower;
        public int? AccuracyRoll { get { return _accuracyRoll; } set { _accuracyRoll = value; OnPropertyChanged(); } }
        public int? ConstitutionRoll { get { return _constitutionRoll; } set { _constitutionRoll = value; OnPropertyChanged(); } }
        public int? FightingRoll { get { return _fightingRoll; } set { _fightingRoll = value; OnPropertyChanged(); } }
        public int? CommunicationRoll { get { return _communicationRoll; } set { _communicationRoll = value; OnPropertyChanged(); } }
        public int? DexterityRoll { get { return _dexterityRoll; } set { _dexterityRoll = value; OnPropertyChanged(); } }
        public int? IntelligenceRoll { get { return _intelligenceRoll; } set { _intelligenceRoll = value; OnPropertyChanged(); } }
        public int? PerceptionRoll { get { return _perceptionRoll; } set { _perceptionRoll = value; OnPropertyChanged(); } }
        public int? StrengthRoll { get { return _strengthRoll; } set { _strengthRoll = value; OnPropertyChanged(); } }
        public int? WillpowerRoll { get { return _willpowerRoll; } set { _willpowerRoll = value; OnPropertyChanged(); } }
        private int? _accuracyRoll;
        private int? _constitutionRoll;
        private int? _fightingRoll;
        private int? _communicationRoll;
        private int? _dexterityRoll;
        private int? _intelligenceRoll;
        private int? _perceptionRoll;
        private int? _strengthRoll;
        private int? _willpowerRoll;
        private DiceRollService _diceRollService;

        public DiceRollService DiceRollService
        {
            get { return _diceRollService; }
            set { _diceRollService = value; }
        }

        public AllRandomAttributeRollViewModel(DiceRollService diceRollService)
        {
            DiceRollService = diceRollService;
            RollAllRandomAbilityCommand = new RelayCommand(o => true, o => RollAllRandom());
        }
        public void RollAllRandom()
        {
            foreach (var attributeName in Enum.GetNames<CharacterAttributeName>())
            {
                RollAndSetAttribute(attributeName);
            }
        }

        public void RollAndSetAttribute(object attributeName)
        {
            RollResult rollResult = DiceRollService.Roll3D6();
            SetAttributeValue(attributeName, rollResult);
        }
        private void SetAttributeValue(object attributeName, RollResult roll)
        {
            int attributeValue = GetAttributeValueFromRoll(roll.GetRollResultSumValue());
            PropertyInfo attributeProperty = GetType().GetProperties().First(x => x.Name == attributeName.ToString());

            string attributePropertyName = attributeProperty.Name;
            string attributeRollPropertyName = string.Concat(attributePropertyName, "Roll");

            attributeProperty.SetValue(this, attributeValue);
            GetType().GetProperties().First(x => x.Name == attributeRollPropertyName.ToString()).SetValue(this, roll.GetRollResultSumValue());
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
    }
}

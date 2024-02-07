using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{
    public class IncomeQueriedEventArgs : EventArgs
    {
        public IncomeQueriedEventArgs(int? Income, int? Modifiers)
        {
            ApplyModifiers(Income, Modifiers);
        }

        private static int? ApplyModifiers(int? Income, int? Modifiers)
        {
            return Income + Modifiers;
        }
    }

    public class Income : ICharacterCreationBonus
    {
        private int _value;
        public int Value
        {
            get
            {
                return _value + _modifiers.Sum();
            }
            set
            {
                _value = value;
            }
        }

        private readonly List<int> _modifiers = new();
        public List<int> Modifiers { get { return _modifiers; } }
        public string CreationBonusName { get { return $"+{Value} Income"; } }

        public Income(int value)
        {
            Value = value;
        }

        public void AddModifier(object sender, int value)
        {
            Modifiers.Add(value);
        }
        public void Remove(object sender, int value)
        {
            Modifiers.Remove(value);
        }

        public ICharacterCreationBonus ShallowCopy()
        {
            return (Income)ShallowCopy();
        }
    }
}

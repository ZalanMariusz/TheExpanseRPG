using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{
    public class Income : ICharacterCreationBonus
    {
        public int Value { get; set; }

        public string CreationBonusName { get { return $"+{Value} Income"; } }

        public Income(int value)
        {
            Value = value;
        }

        public ICharacterCreationBonus ShallowCopy()
        {
            return (Income)ShallowCopy();
        }
    }
}

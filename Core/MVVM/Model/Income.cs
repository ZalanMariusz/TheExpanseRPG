using TheExpanseRPG.Core.MVVM.Model.Interfaces;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class Income : ICharacterCreationBonus
    {
        public int Value { get; set; }
        public string BenefitName { get { return $"+{Value} Income"; } }
        public Income(int value)
        {
            Value = value;
        }
    }
}

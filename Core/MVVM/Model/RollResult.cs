using System.Collections.Generic;
using System.Linq;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class RollResult
    {
        public List<Die> Dice { get; set; }
        public List<int>? RollModifier { get; set; }
        public RollResult(List<int>? rollModifier = null)
        {
            RollModifier = rollModifier;
            Dice = new List<Die>();
        }
        public int GetRollResultSumValue()
        {
            return Dice.Sum(x => x.RollValue) + GetModifierSum();
        }
        public Die? GetDramaDie()
        {
            return Dice.FirstOrDefault(x => x.IsDramaDie == true);
        }
        public int GetModifierSum()
        {
            return RollModifier == null ? 0 : RollModifier.Sum();
        }
    }
}

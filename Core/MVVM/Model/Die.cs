using System;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class Die
    {
        public bool IsDramaDie { get; private set; }
        public int RollValue { get; private set; }
        public Die(bool isDramaDie = false)
        {
            IsDramaDie = isDramaDie;
        }
        public Die RollDie()
        {
            RollValue = Random.Shared.Next(1, 7);
            return this;
        }
        public Die RollD3()
        {
            RollDie();
            RollValue = (int)(Math.Ceiling((double)RollValue) / 3.0);
            return this;
        }
    }
}

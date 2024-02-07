using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{
    public class Die
    {
        public bool IsDramaDie { get; }
        public int RollValue { get; private set; }
        public int DieValue { get; private set; }
        private readonly IRandomGenerator _randomGenerator;
        public Die(IRandomGenerator randomGenerator, bool isDramaDie = false)
        {
            _randomGenerator = randomGenerator;
            IsDramaDie = isDramaDie;
        }
        public Die RollDie()
        {
            DieValue = _randomGenerator.GetRandomInteger(1, 7);
            RollValue = DieValue;
            return this;
        }
        public Die RollD3()
        {
            RollDie();
            RollValue = (int)(Math.Ceiling(DieValue / 2.0));
            return this;
        }
    }
}

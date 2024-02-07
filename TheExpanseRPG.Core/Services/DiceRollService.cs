using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class DiceRollService : IExpanseService, IDiceRollService
    {
        private readonly IRandomGenerator _randomGenerator;
        public DiceRollService(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }
        //public int RollModifier { get; set; } = 0;
        public RollResult Roll3D6(bool hasDramaDie = false)
        {
            return RollND6(3, hasDramaDie);
        }

        public RollResult RollND6(int diceNumber, bool hasDramaDie = false)
        {
            List<Die> diceList = new();
            int dramaDieIndex = hasDramaDie ? _randomGenerator.GetRandomInteger(0, diceNumber) : -1;
            for (int i = 0; i < diceNumber; i++)
            {
                diceList.Add(RollD6(dramaDieIndex == i));
            }
            return new(diceList);
        }

        public RollResult RollND3(int diceNumber, bool hasDramaDie = false)
        {
            List<Die> diceList = new();
            int dramaDieIndex = hasDramaDie ? _randomGenerator.GetRandomInteger(0, diceNumber) : -1;
            for (int i = 0; i < diceNumber; i++)
            {
                diceList.Add(RollD3(dramaDieIndex == i));
            }
            return new(diceList);
        }
        private Die RollD6(bool hasDramaDie = false)
        {
            Die RollResult = new Die(_randomGenerator, hasDramaDie).RollDie();
            return RollResult;
        }
        private Die RollD3(bool hasDramaDie = false)
        {
            Die RollResult = new Die(_randomGenerator, hasDramaDie).RollD3();
            return RollResult;
        }
    }
}

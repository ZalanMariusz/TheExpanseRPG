using System;
using System.Collections.Generic;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class DiceRollService : IExpanseService
    {
        //public int RollModifier { get; set; } = 0;
        public static RollResult Roll3D6(List<int>? rollModifier = null, bool hasDramaDie = false)
        {
            return RollND6(3, rollModifier, hasDramaDie);
        }

        public static RollResult RollND6(int diceNumber, List<int>? rollModifier = null, bool hasDramaDie = false)
        {
            RollResult rollResult = new(rollModifier);
            int dramaDieIndex = hasDramaDie ? Random.Shared.Next(0, diceNumber) : -1;
            for (int i = 0; i < diceNumber; i++)
            {
                rollResult.Dice.Add(RollD6(dramaDieIndex == i));
            }
            return rollResult;
        }

        public static Die RollD6(bool hasDramaDie = false)
        {
            Die RollResult = new Die(hasDramaDie).RollDie();
            return RollResult;
        }
        public static Die RollD3(bool hasDramaDie = false)
        {
            Die RollResult = new Die(hasDramaDie).RollD3();
            return RollResult;
        }
    }
}

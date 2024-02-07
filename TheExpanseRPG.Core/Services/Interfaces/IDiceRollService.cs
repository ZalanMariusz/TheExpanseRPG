using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface IDiceRollService
    {
        RollResult Roll3D6(bool hasDramaDie = false);
        RollResult RollND3(int diceNumber, bool hasDramaDie = false);
        RollResult RollND6(int diceNumber, bool hasDramaDie = false);
    }
}
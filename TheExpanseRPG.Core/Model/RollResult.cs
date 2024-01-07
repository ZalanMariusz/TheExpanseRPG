namespace TheExpanseRPG.Core.Model;

public class RollResult
{
    private readonly List<Die> _dice;

    public RollResult(List<Die> dice)
    {
        _dice = dice ?? throw new ArgumentNullException(nameof(dice));
    }

    public int GetRollResultSumValue()
    {
        return _dice.Sum(x => x.RollValue) + GetModifierSum();
    }

    public Die? GetDramaDie()
    {
        return _dice.FirstOrDefault(x => x.IsDramaDie == true);
    }
    private int GetModifierSum()
    {
        return 0;
    }
}

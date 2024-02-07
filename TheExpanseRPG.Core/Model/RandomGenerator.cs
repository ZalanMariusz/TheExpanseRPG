using TheExpanseRPG.Core.Model.Interfaces;
namespace TheExpanseRPG.Core.Model;
public class RandomGenerator : IRandomGenerator
{
    public int GetRandomInteger(int lowLimit, int highLimit)
    {
        return Random.Shared.Next(lowLimit, highLimit);
    }
}

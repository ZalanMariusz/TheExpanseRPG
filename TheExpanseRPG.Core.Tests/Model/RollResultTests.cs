using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Tests.Model;

public class RollResultTests
{
    [Fact]
    public void Constructor_WhenDiceIsNull_ThrowsArgumentNullException()
    {
        var action = () => new RollResult(null!);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GetRollResultSumValue_ShouldBeZero()
    {
        static RollResult action() => new(new());
        action().GetRollResultSumValue().Should().Be(0);
    }

    [Fact]
    public void GetDramaDie_EmptyDieListHasNoDramaDie()
    {
        static RollResult action() => new(new());
        action().GetDramaDie().Should().BeNull();
    }

    [Fact]
    public void GetDramaDie_DieListHasNoDramaDie()
    {
        Mock<IRandomGenerator> randomGenerator = new();
        List<Die> dieList = new()
            {
                new Die(randomGenerator.Object),
                new Die(randomGenerator.Object),
                new Die(randomGenerator.Object)
            };

        static RollResult action(List<Die> dieList) => new(dieList);
        action(dieList).GetDramaDie().Should().BeNull();
    }

    [Fact]
    public void GetDramaDie_DieListHasDie()
    {
        Mock<IRandomGenerator> randomGenerator = new();
        List<Die> dieList = new()
            {
                new Die(randomGenerator.Object),
                new Die(randomGenerator.Object),
                new Die(randomGenerator.Object,true)
            };

        static RollResult action(List<Die> dieList) => new(dieList);
        action(dieList).GetDramaDie().Should().NotBeNull();
    }

}


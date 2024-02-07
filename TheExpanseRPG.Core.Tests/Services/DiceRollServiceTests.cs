using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.Tests.Services;

public class DiceRollServiceTests
{
    private readonly DiceRollService _sut;
    private readonly Mock<IRandomGenerator> randomGenerator;
    public DiceRollServiceTests()
    {
        randomGenerator = new();
        _sut = new DiceRollService(randomGenerator.Object);
    }

    [Fact]
    public void Roll3D6_ReturnsGreaterThenOrEqualToThree()
    {
        randomGenerator.Setup(x => x.GetRandomInteger(1, 7)).Returns(1);
        _sut.Roll3D6().GetRollResultSumValue().Should().BeGreaterThanOrEqualTo(3);
    }
    [Fact]
    public void Roll3D6_ReturnsLessThenOrEqualToEightTeen()
    {
        randomGenerator.Setup(x => x.GetRandomInteger(1, 7)).Returns(6);
        _sut.Roll3D6().GetRollResultSumValue().Should().BeLessThanOrEqualTo(18);
    }
    [Fact]
    public void Roll3D6_HasNoDramaDie()
    {
        _sut.Roll3D6().GetDramaDie().Should().BeNull();
    }
    [Fact]
    public void Roll3D6_HasDramaDie()
    {
        _sut.Roll3D6(true).GetDramaDie().Should().NotBeNull();
    }
    [InlineData(2)]
    [InlineData(6)]
    [InlineData(3)]
    [Theory]
    public void RollND6_RollCannotBeLessThanDiceNumber(int diceNumber)
    {
        randomGenerator.Setup(x => x.GetRandomInteger(1, 7)).Returns(1);

        _sut.RollND6(diceNumber).GetRollResultSumValue().Should().BeGreaterThanOrEqualTo(diceNumber);
    }
    [InlineData(2)]
    [InlineData(6)]
    [InlineData(3)]
    [Theory]
    public void RollND6_RollCannotBeGreaterThenNTimesSix(int diceNumber)
    {
        int expected = diceNumber * 6;
        randomGenerator.Setup(x => x.GetRandomInteger(1, 7)).Returns(6);

        _sut.RollND6(diceNumber).GetRollResultSumValue().Should().BeLessThanOrEqualTo(expected);
    }
    [InlineData(2)]
    [InlineData(6)]
    [InlineData(3)]
    [Theory]
    public void RollND3_RollCannotBeLessThanDiceNumber(int diceNumber)
    {
        randomGenerator.Setup(x => x.GetRandomInteger(1, 7)).Returns(1);

        _sut.RollND3(diceNumber).GetRollResultSumValue().Should().BeLessThanOrEqualTo(diceNumber);
    }
    [InlineData(2)]
    [InlineData(6)]
    [InlineData(3)]
    [Theory]
    public void RollND6_RollCannotBeGreaterThenNTimesThree(int diceNumber)
    {
        int expected = diceNumber * 3;
        randomGenerator.Setup(x => x.GetRandomInteger(1, 7)).Returns(6);

        _sut.RollND3(diceNumber).GetRollResultSumValue().Should().BeLessThanOrEqualTo(expected);
    }

}

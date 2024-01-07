using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class DieTests
    {
        [Fact]
        public void Constructor_IsDramaDieTrue()
        {
            Mock<IRandomGenerator> randomGenerator = new();
            var die = new Die(randomGenerator.Object, true);
            die.IsDramaDie.Should().BeTrue();
        }
        [Fact]
        public void RollD3_RollThreeShouldBeWorthTwo()
        {
            Mock<IRandomGenerator> randomGenerator = new();
            randomGenerator.Setup(random => random.GetRandomInteger(1, 7)).Returns(() => 3);
            Die die = new(randomGenerator.Object);
            die.RollD3().RollValue.Should().Be(2);
        }
        [Fact]
        public void RollD6_RollIsLessThenSeven()
        {
            Mock<IRandomGenerator> randomGenerator = new();
            randomGenerator.Setup(random => random.GetRandomInteger(1, 7)).Returns(Random.Shared.Next(1,7));
            var die = new Die(randomGenerator.Object).RollDie();

            die.RollValue.Should().BeLessThan(7);
        }
    }
}

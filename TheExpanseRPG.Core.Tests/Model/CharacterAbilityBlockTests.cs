using FluentAssertions;
using FluentAssertions.Common;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class CharacterAbilityBlockTests
    {
        readonly CharacterAbilityBlock _sut;
        public CharacterAbilityBlockTests()
        {
            _sut = new();
        }

        [Fact]
        public void GetStrength_ReturnsStrength()
        {
            CharacterAbilityName.Strength.Should().Be(_sut.GetStrength().AbilityName);
        }
        [Fact]
        public void GetConstitution_ReturnsConstitution()
        {
            CharacterAbilityName.Constitution.Should().Be(_sut.GetConstitution().AbilityName);
        }
        [Fact]
        public void GetAccuracy_ReturnsAccuracy()
        {
            CharacterAbilityName.Accuracy.Should().Be(_sut.GetAccuracy().AbilityName);
        }
        [Fact]
        public void GetIntelligence_ReturnsIntelligence()
        {
            CharacterAbilityName.Intelligence.Should().Be(_sut.GetIntelligence().AbilityName);
        }
        [Fact]
        public void GetPerception_ReturnsPerception()
        {
            CharacterAbilityName.Perception.Should().Be(_sut.GetPerception().AbilityName);
        }
        [Fact]
        public void GetCommunication_ReturnsCommunication()
        {
            CharacterAbilityName.Communication.Should().Be(_sut.GetCommunication().AbilityName);
        }
        [Fact]
        public void GetDexterity_ReturnsDexterity()
        {
            CharacterAbilityName.Dexterity.Should().Be(_sut.GetDexterity().AbilityName);
        }
        [Fact]
        public void GetFighting_ReturnsFighting()
        {
            CharacterAbilityName.Fighting.Should().Be(_sut.GetFighting().AbilityName);
        }
        [Fact]
        public void GetWillpower_ReturnsWillpower()
        {
            CharacterAbilityName.Willpower.Should().Be(_sut.GetWillpower().AbilityName);
        }

    }
}

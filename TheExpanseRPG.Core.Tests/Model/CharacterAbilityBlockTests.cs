using FluentAssertions;
using FluentAssertions.Common;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class CharacterAbilityBlockTests
    {
        readonly CharacterAbilityBlock _block;
        readonly AbilityFocus _focus;
        public CharacterAbilityBlockTests()
        {
            _block= new();
            _focus = new(CharacterAbilityName.Strength, "testfocus");
        }
        //[Fact]
        //public void Constructor_InitiatedAbilitiesHaveNoFocuses()
        //{
        //    int expectedCount = 0;
        //    int actualCount = 0;
        //    foreach (var ability in _block.AbilityList)
        //    {
        //        actualCount += ability.Focuses.Count;
        //    }
        //    actualCount.Should().Be(expectedCount);
        //}
        //[Fact]
        //public void AddFocus_HasFocusIsTrue()
        //{
        //    _block.AddFocus(_focus);
        //    _block.HasFocus(_focus).Should().BeTrue();
        //}
        //[Fact]
        //public void HasFocus_ReturnsFalseIfFocusDoesNotExist()
        //{
        //    _block.HasFocus(_focus).Should().BeFalse();
        //}
        [Fact]
        public void GetStrength_ReturnsStrength()
        {
            CharacterAbilityName.Strength.Should().Be(_block.GetStrength().AbilityName);
        }
        [Fact]
        public void GetConstitution_ReturnsConstitution()
        {
            CharacterAbilityName.Constitution.Should().Be(_block.GetConstitution().AbilityName);
        }
        [Fact]
        public void GetAccuracy_ReturnsAccuracy()
        {
            CharacterAbilityName.Accuracy.Should().Be(_block.GetAccuracy().AbilityName);
        }
        [Fact]
        public void GetIntelligence_ReturnsIntelligence()
        {
            CharacterAbilityName.Intelligence.Should().Be(_block.GetIntelligence().AbilityName);
        }
        [Fact]
        public void GetPerception_ReturnsPerception()
        {
            CharacterAbilityName.Perception.Should().Be(_block.GetPerception().AbilityName);
        }
        [Fact]
        public void GetCommunication_ReturnsCommunication()
        {
            CharacterAbilityName.Communication.Should().Be(_block.GetCommunication().AbilityName);
        }
        [Fact]
        public void GetDexterity_ReturnsDexterity()
        {
            CharacterAbilityName.Dexterity.Should().Be(_block.GetDexterity().AbilityName);
        }
        [Fact]
        public void GetFighting_ReturnsFighting()
        {
            CharacterAbilityName.Fighting.Should().Be(_block.GetFighting().AbilityName);
        }
        [Fact]
        public void GetWillpower_ReturnsWillpower()
        {
            CharacterAbilityName.Willpower.Should().Be(_block.GetWillpower().AbilityName);
        }

    }
}

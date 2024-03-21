using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class CharacterAbilityTests
    {
        readonly int _abilityScore;
        readonly CharacterAbilityName _abilityName;
        readonly CharacterAbility _sut;
        public CharacterAbilityTests()
        {
            _abilityScore = 11;
            _abilityName = CharacterAbilityName.Strength;
            _sut = new (_abilityName,_abilityScore);
        }

        [Fact]
        public void Modifier_ModifiesAbilityValue()
        {
            int modifier = 4;
            int? expectedScore = _abilityScore + modifier;
            _sut.Modifier = modifier;

            _sut.AbilityValue.Should().Be(expectedScore);
        }

        [Fact]
        public void Modifier_BaseValueIsNotModified()
        {
            int modifier = 4;
            _sut.Modifier = modifier;
            _sut.BaseValue.Should().Be(_abilityScore);
        }
        [Fact]
        public void CreationBonusName_ReturnsWithValueOfOne()
        {
            string expected = $"+1 {_sut.AbilityName}";
            _sut.CreationBonusName.Should().Be(expected);
        }
    }

}

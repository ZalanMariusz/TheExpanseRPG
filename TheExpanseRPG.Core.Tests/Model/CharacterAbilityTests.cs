using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class CharacterAbilityTests
    {
        readonly string _focusName;
        readonly int _abilityScore;
        readonly CharacterAbilityName _abilityName;
        public CharacterAbilityTests()
        {
            _focusName = "testfocus";
            _abilityScore = 11;
            _abilityName = CharacterAbilityName.Strength;
        }

        [Fact]
        public void Modifier_ModifiesAbilityValue()
        {
            int modifier = 4;
            int? expectedScore = _abilityScore + modifier;
            CharacterAbility ability = new(_abilityName, _abilityScore)
            {
                Modifier = modifier
            };

            ability.AbilityValue.Should().Be(expectedScore);
        }

        [Fact]
        public void Modifier_BaseValueIsNotModified()
        {
            int modifier = 4;
            CharacterAbility ability = new(_abilityName, _abilityScore)
            {
                Modifier = modifier
            };
            ability.BaseValue.Should().Be(_abilityScore);
        }

        //[Fact]
        //public void AddFocus_AddedFocusExists()
        //{
        //    CharacterAbility ability = new(_abilityName);
        //    ability.AddFocus(new AbilityFocus(_abilityName, _focusName));

        //    ability.GetAbilityFocus(_focusName).Should().NotBeNull();
        //}
        //[Fact]
        //public void AddFocus_AddingAlreadyExistingFocusThrowsArgumentException()
        //{
        //    CharacterAbility ability = new(_abilityName);
        //    AbilityFocus focus = new(_abilityName, _focusName);
        //    ability.AddFocus(focus);
        //    focus = new AbilityFocus(_abilityName, _focusName);

        //    ability.Invoking(x => x.AddFocus(focus)).Should().Throw<ArgumentException>();
        //}
        //[Fact]
        //public void AddFocus_AddingFocusWithDifferentAbilityThrowsArgumentException()
        //{
        //    CharacterAbility ability = new(_abilityName);
        //    AbilityFocus focus = new(CharacterAbilityName.Accuracy, _focusName);

        //    ability.Invoking(x => x.AddFocus(focus)).Should().Throw<ArgumentException>();

        //}
        //[Fact]
        //public void GetAbilityFocus_FocusDoesNotExistsAndReturnsNull()
        //{
        //    CharacterAbility ability = new(_abilityName);
        //    ability.GetAbilityFocus(_focusName).Should().BeNull();
        //}
    }
}

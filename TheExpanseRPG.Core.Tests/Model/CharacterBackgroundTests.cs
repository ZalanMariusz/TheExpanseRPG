using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class CharacterBackgroundTests
    {
        readonly string backgroundName = "backgroundname";
        readonly string backgroundDescription = "backgrounddesc";
        readonly CharacterSocialClass mainSocialClass = CharacterSocialClass.Outsider;
        readonly CharacterAbility abilityBonus = new(CharacterAbilityName.Accuracy);
        readonly List<AbilityFocus> possibleAbilityFocuses = new();
        readonly List<CharacterTalent> possiblePlayerTalents = new();
        readonly List<ICharacterCreationBonus> backgroundBenefits = new();
        readonly CharacterBackGround _characterBackGround;

        public CharacterBackgroundTests()
        {
            _characterBackGround = new(
               backgroundName,
               backgroundDescription,
               mainSocialClass,
               abilityBonus,
               possibleAbilityFocuses,
               possiblePlayerTalents,
               backgroundBenefits
               );
        }
        [Fact]
        public void Constructor_BackgroundNameIsSet()
        {
            _characterBackGround.BackgroundName.Should().Be(backgroundName);
        }
        [Fact]
        public void Constructor_BackgroundDescriptionIsSet()
        {
            _characterBackGround.BackgroundDescription.Should().Be(backgroundDescription);
        }
        [Fact]
        public void Constructor_MainSocialClassIsSet()
        {
            _characterBackGround.MainSocialClass.Should().Be(mainSocialClass);
        }
        [Fact]
        public void Constructor_AbilityBonusIsSet()
        {
            _characterBackGround.AbilityBonus.Should().Be(abilityBonus);
        }
        [Fact]
        public void Constructor_PossibleAbilityFocuses()
        {
            _characterBackGround.PossibleAbilityFocuses.Should().BeEquivalentTo(possibleAbilityFocuses);
        }
        [Fact]
        public void Constructor_possiblePlayerTalentsIsSet()
        {
            _characterBackGround.PossiblePlayerTalents.Should().BeEquivalentTo(possiblePlayerTalents);
        }
        [Fact]
        public void Constructor_backgroundBenefitsIsSet()
        {
            _characterBackGround.BackgroundBenefits.Should().BeEquivalentTo(backgroundBenefits);
        }
    }
}

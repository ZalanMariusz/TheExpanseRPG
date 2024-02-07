using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model
{
    public class CharacterProfessionTests
    {
        readonly string professionName = "professionName";
        readonly string professionDescription = "professionDescription";
        readonly CharacterSocialClass professionSocialClass = CharacterSocialClass.Outsider;
        readonly List<AbilityFocus> focusChoices = new();
        readonly List<CharacterTalent> talentChoices = new();
        readonly CharacterProfession _profession;
        public CharacterProfessionTests()
        {
            _profession = new(
                professionName,
                professionDescription,
                professionSocialClass,
                focusChoices,
                talentChoices
                );
        }
        [Fact]
        public void Constructor_professionNameIsSet()
        {
            _profession.ProfessionName.Should().Be(professionName);
        }
        [Fact]
        public void Constructor_ProfessionDescriptionIsSet()
        {
            _profession.ProfessionDescription.Should().Be(professionDescription);
        }
        [Fact]
        public void Constructor_ProfessionSocialClassIsSet()
        {
            _profession.ProfessionSocialClass.Should().Be(professionSocialClass);
        }
        [Fact]
        public void Constructor_FocusChoicesIsSet()
        {
            _profession.FocusChoices.Should().BeEquivalentTo(focusChoices);
        }
        [Fact]
        public void Constructor_TalentChoicesIsSet()
        {
            _profession.TalentChoices.Should().BeEquivalentTo(talentChoices);
        }
    }
}

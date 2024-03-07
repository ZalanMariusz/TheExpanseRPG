using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Talents;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services
{
    public class CharacterCreationServiceTests
    {
        private readonly CharacterCreationService _sut;
        private readonly Mock<ICharacterAbilityBlockBuilder> _abilityBlockBuilder = new();
        private readonly Mock<ICharacterSocialAndBackgroundBuilder> _socialAndBackgroundBuilder = new();
        private readonly Mock<ICharacterProfessionBuilder> _characterPofessionBuilder = new();
        private readonly Mock<ICharacterDriveBuilder> _characterDriveBuilder = new();
        private readonly Mock<ICharacterOriginBuilder> _characterOriginBuilder = new();

        public CharacterCreationServiceTests()
        {

            _sut = new(
               _abilityBlockBuilder.Object,
               _socialAndBackgroundBuilder.Object,
               _characterPofessionBuilder.Object,
               _characterDriveBuilder.Object,
               _characterOriginBuilder.Object
                );
        }

        [Fact]
        public void GetTotalIncome_ReturnsNoviceAffluentBonus()
        {
            _sut.TalentBonuses.Add(new Affluent(new(), string.Empty, string.Empty, string.Empty, string.Empty));
            _sut.GetTotalIncome().Should().Be(2);
        }
        [Fact]
        public void GetTotalIncome_ReturnsExpertAffluentBonus()
        {
            _sut.TalentBonuses.Add(new Affluent(new(), string.Empty, string.Empty, string.Empty, string.Empty));
            _sut.TalentBonuses.ForEach(x => x.ImproveTalent());
            _sut.GetTotalIncome().Should().Be(3);
        }
        [Fact]
        public void GetTotalIncome_ReturnsMasterAffluentBonus()
        {
            _sut.TalentBonuses.Add(new Affluent(new(), string.Empty, string.Empty, string.Empty, string.Empty));
            _sut.TalentBonuses.ForEach(x => x.ImproveTalent());
            _sut.TalentBonuses.ForEach(x => x.ImproveTalent());
            _sut.GetTotalIncome().Should().Be(4);
        }
        [InlineData(CharacterSocialClass.Outsider, 0)]
        [InlineData(CharacterSocialClass.Lower, 2)]
        [InlineData(CharacterSocialClass.Middle, 4)]
        [InlineData(CharacterSocialClass.Upper, 6)]
        [Theory]
        public void GetTotalIncome_ReturnsProfessionBonus(CharacterSocialClass socialClass, int expected)
        {
            _characterPofessionBuilder.Setup(builder => builder.SelectedCharacterProfession).Returns(
                () => new(string.Empty, string.Empty, socialClass, new(), new()));
            _sut.GetTotalIncome().Should().Be(expected);
        }
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [Theory]
        public void GetTotalIncome_ReturnsIncomeBonuses(int incomeBonus, int expected)
        {
            _sut.IncomeBonuses.Add(new(incomeBonus));
            _sut.GetTotalIncome().Should().Be(expected);
        }
        [InlineData(CharacterSocialClass.Outsider, CharacterSocialClass.Outsider, 0)] //0+(0-0)
        [InlineData(CharacterSocialClass.Outsider, CharacterSocialClass.Upper, 3)] //0+(3-0)
        [InlineData(CharacterSocialClass.Lower, CharacterSocialClass.Outsider, 1)] //2+(0-1)
        [InlineData(CharacterSocialClass.Middle, CharacterSocialClass.Lower, 3)] //4+(1-2)
        [InlineData(CharacterSocialClass.Lower, CharacterSocialClass.Upper, 4)] //2+(3-1)
        [Theory]
        public void GetTotalIncome_CalculatesProfessionAndSocialClassDiff(CharacterSocialClass professionSocialClass, CharacterSocialClass selectedCharacterSocialClass, int expected)
        {
            _characterPofessionBuilder.Setup(builder => builder.SelectedCharacterProfession).Returns(
                () => new(string.Empty, string.Empty, professionSocialClass, new(), new()));

            _socialAndBackgroundBuilder.Setup(builder => builder.SelectedCharacterSocialClass).Returns(
                () => selectedCharacterSocialClass);

            _sut.GetTotalIncome().Should().Be(expected);
        }
        [InlineData(false, false, false, false, CharacterOrigin.Earth, "a", true)]
        [InlineData(true, false, false, false, CharacterOrigin.Earth, "a", false)]
        [InlineData(false, true, false, false, CharacterOrigin.Earth, "a", false)]
        [InlineData(false, false, true, false, CharacterOrigin.Earth, "a", false)]
        [InlineData(false, false, false, true, CharacterOrigin.Earth, "a", false)]
        [InlineData(false, false, false, false, null, "a", false)]
        [InlineData(false, false, false, false, CharacterOrigin.Earth, "", false)]
        [InlineData(false, false, false, false, CharacterOrigin.Earth, null, false)]
        [Theory]
        public void CanCreateCharacter_ReturnsTrueWithEverythingSelectedAndNameAndNoConflicts(
            bool isMissingBackgroundBonus,
            bool isMissingProfessionBonus,
            bool isMissingDriveBonus,
            bool isMissingAbilityRoll,
            CharacterOrigin? characterOrigin,
            string characterName,
            bool expected
            )
        {
            _socialAndBackgroundBuilder.Setup(builder => builder.IsMissingBackgroundBonus()).Returns(() => isMissingBackgroundBonus);
            _characterPofessionBuilder.Setup(builder => builder.IsMissingProfessionBonus()).Returns(() => isMissingProfessionBonus);
            _characterDriveBuilder.Setup(builder => builder.IsMissingDriveBonus()).Returns(() => isMissingDriveBonus);
            _abilityBlockBuilder.Setup(builder => builder.IsMissingAbilityRoll()).Returns(() => isMissingAbilityRoll);
            _characterOriginBuilder.Setup(builder => builder.SelectedCharacterOrigin).Returns(() => characterOrigin);

            _sut.CharacterName = characterName;
            _sut.CanCreateCharacter().Should().Be(expected);
        }
    }
}

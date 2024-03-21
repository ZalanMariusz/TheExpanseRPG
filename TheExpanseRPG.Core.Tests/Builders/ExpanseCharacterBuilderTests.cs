using FluentAssertions;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Tests.Model;

namespace TheExpanseRPG.Core.Tests.Builders
{
    public class ExpanseCharacterBuilderTests
    {

        public static IEnumerable<object[]> _driveBonuses => new List<object[]>()
        {
            new object[]{ new Relationship(),1,0,0,0 },
            new object[]{ new Membership(),0,1,0,0 },
            new object[]{ new Reputation(),0,0,1,0 },
            new object[]{ new Fortune() { Value = 5 },0,0,0,5 },
        };

        private readonly ExpanseCharacter _character;
        private readonly CharacterOrigin expectedOrigin = CharacterOrigin.Earth;
        private readonly CharacterSocialClass expectedSocialClass = CharacterSocialClass.Lower;
        private readonly CharacterBackGround expectedBackground = DummyDataGenerator.DummyBackground;
        private readonly CharacterProfession expectedProfession = DummyDataGenerator.DummyProfession;
        private readonly CharacterDrive expectedDrive = DummyDataGenerator.DummyDrive;
        private readonly string expectedStringLiteral = "magicString";
        private readonly int expectedIncome = 7;
        public ExpanseCharacterBuilderTests()
        {
            _character = ExpanseCharacterBuilder.StartCreateCharacter()
                .WithOrigin(expectedOrigin)
                .AndSocialClass(expectedSocialClass)
                .AndBackground(expectedBackground.BackgroundName)
                .AndProfession(expectedProfession.ProfessionName)
                .AndDrive(expectedDrive.DriveName)
                .WithDriveBonus(DummyDataGenerator.DummyTalent)
                .AddAbilityBlock(new())
                .WithAbilityBonuses(new() { DummyDataGenerator.DummyAbility })
                .AddFocuses(new() { DummyDataGenerator.DummyFocus })
                .AndTalents(new() { DummyDataGenerator.DummyTalent })
                .SetCharacterName(expectedStringLiteral)
                .AndDescription(expectedStringLiteral)
                .AndAvatar(expectedStringLiteral)
                .SetIncome(expectedIncome);

        }
        [Fact]
        public void SetsOrigin()
        {
            _character.Origin.Should().Be(expectedOrigin);
        }
        [Fact]
        public void SetsSocialClass()
        {
            _character.SocialClass.Should().Be(expectedSocialClass);
        }
        [Fact]
        public void SetsBackground()
        {
            _character.Background.Should().Be(expectedBackground.BackgroundName);
        }
        [Fact]
        public void SetsProfession()
        {
            _character.Background.Should().Be(expectedProfession.ProfessionName);
        }
        [Fact]
        public void SetsDrive()
        {
            _character.Drive.Should().Be(expectedDrive.DriveName);
        }
        [Fact]
        public void SetsAbilityBlock()
        {
            _character.Abilities.Should().NotBeNull();
            _character.Abilities.AbilityList.Should().HaveCount(9);
        }
        [Fact]
        public void SetsAbilityBonus()
        {
            CharacterAbilityName abilityName = DummyDataGenerator.DummyAbility.AbilityName;
            _character.Abilities.GetAbility(abilityName).AbilityValue.Should().Be(1);
        }
        [Fact]
        public void SetsFocuses()
        {
            _character.Focuses.Should().HaveCount(1);
        }
        [Fact]
        public void SetsTalents()
        {
            _character.Talents.Should().HaveCount(1);
        }
        [Fact]
        public void SetsCharacterName()
        {
            _character.Name.Should().Be(expectedStringLiteral);
        }
        [Fact]
        public void SetsCharacterDescription()
        {
            _character.Description.Should().Be(expectedStringLiteral);
        }
        [Fact]
        public void SetsCharacterAvatar()
        {
            _character.Avatar.Should().Be(expectedStringLiteral);
        }
        [Fact]
        public void SetsIncome()
        {
            _character.Income.Should().Be(expectedIncome);
        }
        [Theory]
        [MemberData(nameof(_driveBonuses))]
        public void DriveBonusIsSet(ICharacterCreationBonus driveBonus, int expectedRelCount, int expectedMemCount, int expectedRepuCount, int expectedFortune)
        {
            ExpanseCharacter _characterWithDriveBonus = ExpanseCharacterBuilder.StartCreateCharacter()
                .WithOrigin(expectedOrigin)
                .AndSocialClass(expectedSocialClass)
                .AndBackground(expectedBackground.BackgroundName)
                .AndProfession(expectedProfession.ProfessionName)
                .AndDrive(expectedDrive.DriveName)
                .WithDriveBonus(driveBonus)
                .AddAbilityBlock(new())
                .WithAbilityBonuses(new() { DummyDataGenerator.DummyAbility })
                .AddFocuses(new() { DummyDataGenerator.DummyFocus })
                .AndTalents(new() { DummyDataGenerator.DummyTalent })
                .SetCharacterName(expectedStringLiteral)
                .AndDescription(expectedStringLiteral)
                .AndAvatar(expectedStringLiteral)
                .SetIncome(expectedIncome);

            _characterWithDriveBonus.Relationships.Should().HaveCount(expectedRelCount);
            _characterWithDriveBonus.Memberships.Should().HaveCount(expectedMemCount);
            _characterWithDriveBonus.Reputations.Should().HaveCount(expectedRepuCount);
            _characterWithDriveBonus.Fortune.Should().Be(expectedFortune);
        }
    }
}

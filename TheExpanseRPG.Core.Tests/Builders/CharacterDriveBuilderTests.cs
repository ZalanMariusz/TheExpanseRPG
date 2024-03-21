using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Builders;

public class CharacterDriveBuilderTests
{
    readonly CharacterDriveBuilder _sut;
    readonly Mock<ICharacterDriveListService> _driveListService;
    readonly Mock<IRandomGenerator> _randomGenerator;

    public static readonly List<object[]> _tiesTestData = new()
    {
        new object[] {new Relationship()},
        new object[] {new Membership()},
        new object[] {new Reputation()}
    };

    public CharacterDriveBuilderTests()
    {
        _driveListService = new();
        _randomGenerator = new();
        _sut = new(_driveListService.Object, _randomGenerator.Object);
    }

    [Fact]
    public void IsMissingDriveBonus_TrueByDefault()
    {
        _sut.IsMissingDriveBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingDriveBonus_FalseIfAllIsSetWithNonTieBonus()
    {
        _sut.SelectedDriveBonus = new Fortune();
        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedDriveTalent = new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.IsMissingDriveBonus().Should().BeFalse();
    }
    [Theory]
    [MemberData(nameof(_tiesTestData))]
    public void IsMissingDriveBonus_TrueIfAllIsSetWithDefaultTieBonus(CharacterTie tie)
    {
        _sut.SelectedDriveBonus = tie;
        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedDriveTalent = new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.IsMissingDriveBonus().Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(_tiesTestData))]
    public void IsMissingDriveBonus_FalseIfAllIsSetWithTieBonusWithDescription(CharacterTie tie)
    {
        tie.Description = "description";
        _sut.SelectedDriveBonus = tie;
        _sut.SelectedDriveTalent = new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.IsMissingDriveBonus().Should().BeFalse();
    }
    [Fact]
    public void IsMissingDriveBonus_TrueIfDriveBonusIsNull()
    {
        _sut.SelectedDriveBonus = null;

        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedDriveTalent = new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.IsMissingDriveBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingDriveBonus_TrueIfDriveTalentIsNull()
    {
        _sut.SelectedDriveTalent = null;

        _sut.SelectedDriveBonus = new Fortune();
        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.IsMissingDriveBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingDriveBonus_TrueIfSelectedDriveIsNull()
    {
        _sut.SelectedCharacterDrive = null;

        _sut.SelectedDriveTalent = new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedDriveBonus = new Fortune();
        _sut.IsMissingDriveBonus().Should().BeTrue();
    }
    [Fact]
    public void DriveBonusDescription_ReturnsEmptyIfBonusIsNotATie()
    {
        _sut.SelectedDriveBonus = new Fortune();
        _sut.DriveBonusDescription.Should().BeEmpty();
    }
    [Fact]
    public void DriveBonusDescription_ReturnsEmptyIfBonusIsADefaultTie()
    {
        _sut.SelectedDriveBonus = new Relationship();
        _sut.DriveBonusDescription.Should().BeEmpty();
    }
    [Fact]
    public void DriveBonusDescription_ReturnsDescriptionIfBonusIsATieWithDescription()
    {
        string expected = "description";
        _sut.SelectedDriveBonus = new Relationship() { Description = expected };
        _sut.DriveBonusDescription.Should().Be(expected);
    }
    [Fact]
    public void DriveBonusDescription_DoesNotSetAnythingIfBonusIsNotATie()
    {
        _sut.SelectedDriveBonus = new Fortune();
        _sut.DriveBonusDescription = "description";
        _sut.DriveBonusDescription.Should().BeEmpty();
    }
    [Fact]
    public void DriveBonusDescription_DoesNotSetAnythingIfBonusIsNull()
    {
        _sut.SelectedDriveBonus = null;
        _sut.DriveBonusDescription = "description";
        _sut.DriveBonusDescription.Should().BeEmpty();
    }
    [Fact]
    public void DriveBonusDescription_SetsDescriptionIfBonusIsATie()
    {
        string expected = "description";
        _sut.SelectedDriveBonus = new Relationship();
        _sut.DriveBonusDescription = expected;
        _sut.DriveBonusDescription.Should().Be(expected);
    }
    [Fact]
    public void DriveBonusDescription_SetsToEmptyIfParamValueIsNull()
    {
        string? param = null;
        string expected = string.Empty;

        _sut.SelectedDriveBonus = new Relationship();
        _sut.DriveBonusDescription = param;
        _sut.DriveBonusDescription.Should().Be(expected);
    }
    [Fact]
    public void DriveSelectionChanged_FiringTest()
    {
        List<object?> invocationCount = new();
        _sut.DriveSelectionChanged += (sender, args) => invocationCount.Add(sender);
        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedCharacterDrive = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        invocationCount.Count.Should().Be(2);
    }
    [Fact]
    public void BonusSelectionChanged_FiringTest()
    {
        List<object?> invocationCount = new();
        _sut.BonusSelectionChanged += (sender, args) => invocationCount.Add(sender);
        _sut.SelectedDriveTalent = new(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty);
        _sut.SelectedDriveBonus = new Fortune();
        invocationCount.Count.Should().Be(2);
    }
    [Fact]
    public void FortuneBonus_BonusIsAdded()
    {
        _sut.SelectedDriveBonus = new Fortune();
        _sut.FortuneBonus.Should().Be(5);
    }
    [Fact]
    public void FortuneBonus_ReturnsZeroWithoutBonus()
    {
        _sut.SelectedDriveBonus = null;
        _sut.FortuneBonus.Should().Be(0);
    }
    [Fact]
    public void GenerateRandom_GeneratesEverything()
    {
        _driveListService.Setup(service => service.DriveList).Returns(new List<CharacterDrive>() { new CharacterDrive(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) });
        _driveListService.Setup(service => service.DriveBonuses).Returns(new List<ICharacterCreationBonus>() { new Fortune() });
        Dictionary<string, List<CharacterTalent>> driveTalents = new()
        {
            { "", new (){new CharacterTalent(string.Empty, new(), string.Empty, string.Empty, string.Empty, string.Empty) } }
        };
        _driveListService.Setup(service => service.DriveTalentList).Returns(driveTalents);

        _randomGenerator.Setup(random => random.GetRandomInteger(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
        _sut.GenerateRandom();

        _sut.SelectedCharacterDrive.Should().NotBeNull();
        _sut.SelectedDriveBonus.Should().NotBeNull();
        _sut.SelectedDriveTalent.Should().NotBeNull();
    }
}

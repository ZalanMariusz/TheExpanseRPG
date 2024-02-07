using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services;

public class CharacterDriveListServiceTests
{
    private readonly CharacterDriveListService _sut;
    public CharacterDriveListServiceTests()
    {
        Mock<ITalentListService> talentListService = new();
        SqliteDatabaseConnectorService dbConnector = new();

        _sut = new(dbConnector,talentListService.Object);
    }

    [Fact]
    public void Constructor_DriveListIsNotNull()
    {
        _sut.DriveList.Should().NotBeNull();
    }
    [Fact]
    public void Constructor_DriveListIsNotEmpty()
    {
        _sut.DriveList.Should().NotBeEmpty();
    }
    [Fact]
    public void GetDriveTalentOptions_NonExistentDriveReturnsEmptyTalentList()
    {
        _sut.GetDriveTalentOptions("xx").Should().BeEmpty();
    }
    [Fact]
    public void GetDriveTalentOptions_ExisingDriveReturnsTalentList()
    {
        _sut.GetDriveTalentOptions("Leader").Should().NotBeEmpty();
    }

}

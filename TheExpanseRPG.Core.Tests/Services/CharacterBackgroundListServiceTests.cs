using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services;

public class CharacterBackgroundListServiceTests
{
    readonly CharacterBackgroundListService _sut;
    public CharacterBackgroundListServiceTests()
    {
        Mock<IAbilityFocusListService> focustListService = new();
        Mock<ITalentListService> talentListService = new();
        SqliteDatabaseConnectorService dbConnector = new();

        _sut = new CharacterBackgroundListService(
            focustListService.Object,
            talentListService.Object,
            dbConnector
            );
    }
    [Fact]
    public void Constructor_BackgroundListIsNotNull()
    {
        _sut.CharacterBackgroundList.Should().NotBeNull();
    }
    [Fact]
    public void Constructor_BackgroundListIsNotEmpty()
    {
        _sut.CharacterBackgroundList.Should().NotBeEmpty();
    }

}

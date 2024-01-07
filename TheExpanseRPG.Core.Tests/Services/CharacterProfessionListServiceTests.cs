using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services;

public class CharacterProfessionListServiceTests
{
    private readonly CharacterProfessionListService _sut;
    public CharacterProfessionListServiceTests()
    {
        Mock<IAbilityFocusListService> focusListService = new();
        Mock<ITalentListService> talentListService = new();

        _sut = new(focusListService.Object, talentListService.Object, new SqliteDatabaseConnectorService());
    }
    [Fact]
    public void Constructor_ProfessionListIsNotNull()
    {
        _sut.ProfessionList.Should().NotBeNull();
    }
    [Fact]
    public void Constructor_ProfessionListIsNotEmpty()
    {
        _sut.ProfessionList.Should().NotBeEmpty();
    }
}

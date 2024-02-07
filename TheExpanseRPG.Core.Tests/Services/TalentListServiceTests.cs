using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services;

public class TalentListServiceTests
{
    private readonly TalentListService _sut;
    public TalentListServiceTests()
    {
        Mock<IAbilityFocusListService> focusListService = new();
        focusListService.Setup(x => x.GetFocusByName(It.IsAny<CharacterAbilityName>(), It.IsAny<string>()))
            .Returns(new AbilityFocus(CharacterAbilityName.Strength,"focus"));

        _sut = new TalentListService(focusListService.Object,new SqliteDatabaseConnectorService());
    }
    [Fact]
    public void Constructor_TalentListIsNotEmpty()
    {
        _sut.TalentList.Should().NotBeEmpty();
    }
    [Fact]
    public void GetTalent_ReturnsExistingTalent()
    {
        _sut.GetTalent("Doctor").Should().NotBeNull();
    }
    [Fact]
    public void GetTalent_ThrowsKeyNotFoundExceptionIfTalentDoesNotExists()
    {
        _sut.Invoking(x=>x.GetTalent("xx")).Should().Throw<KeyNotFoundException>();
    }
}

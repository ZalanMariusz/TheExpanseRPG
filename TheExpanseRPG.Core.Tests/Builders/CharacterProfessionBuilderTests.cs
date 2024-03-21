using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.Core.Tests.Model;

namespace TheExpanseRPG.Core.Tests.Builders;

public class CharacterProfessionBuilderTests
{
    readonly CharacterProfessionBuilder _sut;
    readonly Mock<ICharacterProfessionListService> _professionListService = new();
    readonly Mock<IRandomGenerator> _randomGenerator = new();
    public CharacterProfessionBuilderTests()
    {
        _professionListService.Setup(service => service.ProfessionList).Returns(new List<CharacterProfession>());
        _sut = new(_professionListService.Object, _randomGenerator.Object);
    }
    [Fact]
    public void ClearSelectedProfession_ClearsSelectedProfessionIfSocialClassIsLower()
    {
        _sut.SelectedCharacterProfession = DummyDataGenerator.DummyUpperProfession;
        _sut.ClearSelectedProfession(CharacterSocialClass.Lower);
        _sut.SelectedCharacterProfession.Should().BeNull();
    }
    [Fact]
    public void ClearSelectedProfession_DoesNotClearIfSocialClassIsHigher()
    {
        _sut.SelectedCharacterProfession = DummyDataGenerator.DummyOutsiderProfession;
        _sut.ClearSelectedProfession(CharacterSocialClass.Lower);
    }
    [Fact]
    public void Constructor_SocialClassProfListsAreInitialized()
    {
        _sut.OutsiderProfessions.Should().NotBeNull();
        _sut.LowerclassProfessions.Should().NotBeNull();
        _sut.MiddleclassProfessions.Should().NotBeNull();
        _sut.UpperclassProfessions.Should().NotBeNull();
    }
    [Fact]
    public void IsMissingProfessionBonus_TrueByDefault()
    {
        _sut.IsMissingProfessionBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingProfessionBonus_TrueBySettingOnlyFocus()
    {
        _sut.SelectedProfessionFocus = DummyDataGenerator.DummyFocus;
        _sut.IsMissingProfessionBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingProfessionBonus_TrueBySettingOnlyTalent()
    {
        _sut.SelectedProfessionTalent = DummyDataGenerator.DummyTalent;
        _sut.IsMissingProfessionBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingProfessionBonus_FalseBySettingTalentAndFocus()
    {
        _sut.SelectedProfessionTalent = DummyDataGenerator.DummyTalent;
        _sut.SelectedProfessionFocus = DummyDataGenerator.DummyFocus;
        _sut.IsMissingProfessionBonus().Should().BeFalse();
    }
    [Fact]
    public void SelectedProfessionChanged_ShouldBeInvoked()
    {
        List<object> invocationList = new();
        _sut.SelectedProfessionChanged += (sender, args) => invocationList.Add(sender!);

        _sut.SelectedCharacterProfession = DummyDataGenerator.DummyProfession;
        invocationList.Should().HaveCount(1);
    }
    [Fact]
    public void BonusSelectionChanged_ShouldBeInvoked()
    {
        List<object> invocationList = new();
        _sut.BonusSelectionChanged += (sender, args) => invocationList.Add(sender!);

        _sut.SelectedProfessionTalent = DummyDataGenerator.DummyTalent;
        _sut.SelectedProfessionFocus = DummyDataGenerator.DummyFocus;
        invocationList.Should().HaveCount(2);
    }
}

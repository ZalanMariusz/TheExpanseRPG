using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;
using TheExpanseRPG.Core.Tests.Model;

namespace TheExpanseRPG.Core.Tests.Builders;

public class CharacterSocialAndBackgroundBuilderTests
{
    CharacterSocialAndBackgroundBuilder _sut;

    Mock<ICharacterBackgroundListService> _backgroundListService;
    Mock<IRandomGenerator> _randomGenerator;
    public CharacterSocialAndBackgroundBuilderTests()
    {
        _backgroundListService = new Mock<ICharacterBackgroundListService>();
        _randomGenerator = new Mock<IRandomGenerator>();
        _sut = new CharacterSocialAndBackgroundBuilder(_backgroundListService.Object, _randomGenerator.Object);
    }
    [Fact]
    public void IsMissingBackgroundBonus_ReturnsTrueIfBenefitIsNotSelected()
    {
        _sut.SelectedBackgroundBenefit = null;
        _sut.IsMissingBackgroundBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingBackgroundBonus_ReturnsTrueIfFocusIsNotSelected()
    {
        _sut.SelectedBackgroundFocus = null;
        _sut.IsMissingBackgroundBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingBackgroundBonus_ReturnsTrueIfTalentIsNotSelected()
    {
        _sut.SelectedBackgroundTalent = null;
        _sut.IsMissingBackgroundBonus().Should().BeTrue();
    }
    [Fact]
    public void IsMissingBackgroundBonus_ReturnsFalseIfAllIsSelected()
    {
        _sut.SelectedBackgroundBenefit = new Fortune();
        _sut.SelectedBackgroundTalent = DummyDataGenerator.DummyTalent;
        _sut.SelectedBackgroundFocus = DummyDataGenerator.DummyFocus;
        _sut.IsMissingBackgroundBonus().Should().BeFalse();
    }
    [Fact]
    public void GetAbilityBonus_ReturnsNullWithoutSelectedBackground()
    {
        _sut.SelectedCharacterBackground = null;
        _sut.GetAbilityBonus().Should().BeNull();
    }
    [Fact]
    public void GetAbilityBonus_ReturnsAbilityBonus()
    {
        _sut.SelectedCharacterBackground = DummyDataGenerator.DummyBackground;
        _sut.GetAbilityBonus().Should().NotBeNull().And.BeOfType<CharacterAbility>();
    }
    [Fact]
    public void BonusSelectionChanged_ShouldBeInvoked()
    {
        List<object> invocationList = new();
        _sut.BonusSelectionChanged += (sender, args) => invocationList.Add(sender!);

        _sut.SelectedBackgroundBenefit = new Fortune();
        _sut.SelectedBackgroundTalent = DummyDataGenerator.DummyTalent;
        _sut.SelectedBackgroundFocus = DummyDataGenerator.DummyFocus;
        _sut.SelectedCharacterBackground = DummyDataGenerator.DummyBackground;

        invocationList.Should().HaveCount(4);
    }
    [Fact]
    public void SelectedCharacterSocialClassDescription_ReturnsNullWithoutSelectedSocialClass()
    {
        _sut.SelectedCharacterSocialClass = null;
        _sut.SelectedCharacterSocialClassDescription.Should().BeNull();
    }
    [Fact]
    public void SelectedCharacterSocialClassDescription_ReturnsNotNullWithSelectedSocialClass()
    {
        _sut.SelectedCharacterSocialClass = CharacterSocialClass.Outsider;
        _sut.SelectedCharacterSocialClassDescription.Should().NotBeNull();
    }
    [Fact]
    public void SocialClassChanged_ShouldBeInvoked()
    {
        List<object> invocationList = new();
        _sut.SocialClassChanged += (sender, args) => invocationList.Add(sender!);
        _sut.SelectedCharacterSocialClass = CharacterSocialClass.Outsider;
        invocationList.Should().HaveCount(1);
    }
}

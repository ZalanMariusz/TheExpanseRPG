using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model;
public class AbilityFocusTests
{
    private readonly AbilityFocus _focus;
    
    public AbilityFocusTests()
    {
        _focus = new(CharacterAbilityName.Accuracy, "-");
    }

    [Fact]
    public void Constructor_NewFocusIsNotImproved()
    {
        _focus.IsImproved.Should().BeFalse();
    }
    [Fact]
    public void ImproveFocus_FocusGetsImproved()
    {
        _focus.ImproveFocus();

        _focus.IsImproved.Should().BeTrue();
    }

    [Fact]
    public void ImproveFocus_DoubleImproveIsStillImproved()
    {
        _focus.ImproveFocus();
        _focus.ImproveFocus();

        _focus.IsImproved.Should().BeTrue();
    }
   
}

using FluentAssertions;
using TheExpanseRPG.Core.Model.Talents;

namespace TheExpanseRPG.Core.Tests.Model.Talents;

public class AffluentTests
{
    private readonly Affluent _sut;
    public AffluentTests()
    {
        _sut = new(new(), string.Empty, string.Empty, string.Empty, string.Empty);
    }
    [Fact]
    public void ApplyTalentEffect_AppliesEffect()
    {
        _sut.ApplyTalentEffect().Should().Be(2);

        _sut.ImproveTalent();
        _sut.ApplyTalentEffect().Should().Be(3);

        _sut.ImproveTalent();
        _sut.ApplyTalentEffect().Should().Be(4);

        _sut.ImproveTalent();
        _sut.ApplyTalentEffect().Should().Be(4);
    }

}

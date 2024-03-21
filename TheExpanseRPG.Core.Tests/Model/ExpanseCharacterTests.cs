using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model;

public class ExpanseCharacterTests
{
    private static ExpanseCharacter _sut = new()
    {
        Focuses = new()
        {
            new(CharacterAbilityName.Accuracy, "focus"),
            new(CharacterAbilityName.Constitution, "focus"),
            new(CharacterAbilityName.Communication, "focus"),
            new(CharacterAbilityName.Perception, "focus"),
            new(CharacterAbilityName.Dexterity, "focus"),
            new(CharacterAbilityName.Fighting, "focus"),
            new(CharacterAbilityName.Intelligence, "focus"),
            new(CharacterAbilityName.Strength, "focus"),
            new(CharacterAbilityName.Willpower, "focus")
        },

        Abilities = new()
        {
            AbilityList = new()
            {
                new (CharacterAbilityName.Accuracy,1),
                new (CharacterAbilityName.Constitution,2),
                new (CharacterAbilityName.Communication,3),
                new (CharacterAbilityName.Perception,4),
                new (CharacterAbilityName.Dexterity,5),
                new (CharacterAbilityName.Fighting,6),
                new (CharacterAbilityName.Intelligence,7),
                new (CharacterAbilityName.Strength,8),
                new (CharacterAbilityName.Willpower,9),
            }
        }
    };
    public static IEnumerable<object[]> _testFocusData = new List<object[]>()
    {
        new object[]{ _sut.AccuracyFocuses,1,CharacterAbilityName.Accuracy },
        new object[]{ _sut.ConstitutionFocuses,1,CharacterAbilityName.Constitution },
        new object[]{ _sut.CommunicationFocuses,1,CharacterAbilityName.Communication },
        new object[]{ _sut.PerceptionFocuses,1,CharacterAbilityName.Perception },
        new object[]{ _sut.DexterityFocuses,1,CharacterAbilityName.Dexterity },
        new object[]{ _sut.FightingFocuses,1,CharacterAbilityName.Fighting },
        new object[]{ _sut.IntelligenceFocuses,1,CharacterAbilityName.Intelligence },
        new object[]{ _sut.StrengthFocuses,1,CharacterAbilityName.Strength },
        new object[]{ _sut.WillpowerFocuses,1,CharacterAbilityName.Willpower }
    };
    public static IEnumerable<object[]> _testAbilityData = new List<object[]>()
    {
        new object[]{ _sut.Accuracy,1},
        new object[]{ _sut.Constitution,2},
        new object[]{ _sut.Communication,3},
        new object[]{ _sut.Perception,4},
        new object[]{ _sut.Dexterity,5},
        new object[]{ _sut.Fighting,6},
        new object[]{ _sut.Intelligence,7},
        new object[]{ _sut.Strength,8},
        new object[]{ _sut.Willpower,9}
    };
    public ExpanseCharacterTests()
    {
        _sut.DefenseModifiers.Clear();
        _sut.ArmorModifiers.Clear();
        _sut.IncomeModifiers.Clear();
        _sut.SpeedModifiers.Clear();
        _sut.ToughnessModifiers.Clear();
    }

    [Theory]
    [MemberData(nameof(_testFocusData))]
    public void AbilityFocuses_ReturnsValidFocuses(List<AbilityFocus> listToTest, int expectedMinCount, CharacterAbilityName expectedAbilityName)
    {
        listToTest.Should().HaveCountGreaterThanOrEqualTo(expectedMinCount);
        listToTest.All(x => x.AbilityName == expectedAbilityName).Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(_testAbilityData))]
    public void Ability_ReturnsValidAbilityScore(int actualScore, int expectedScore)
    {
        actualScore.Should().Be(expectedScore);
    }
    [Fact]
    public void Speed_ReturnsSpeedWithModifiers()
    {
        _sut.SpeedModifiers.Add(5);
        _sut.Speed.Should().Be(15);
    }
    [Fact]
    public void Toughness_ReturnsToughnessWithModifiers()
    {
        _sut.ToughnessModifiers.Add(5);
        _sut.Toughness.Should().Be(5);
    }
    [Fact]
    public void Defense_ReturnsDefenseWithModifiers()
    {
        _sut.DefenseModifiers.Add(5);
        _sut.Defense.Should().Be(15);
    }

    [Fact]
    public void Armor_ReturnsArmorWithModifiers()
    {
        _sut.ArmorModifiers.Add(5);
        _sut.ToughnessModifiers.Add(5);
        _sut.Armor.Should().Be(10);
    }
    [Fact]
    public void Income_ReturnsIncomeWithModifiers()
    {
        _sut.IncomeModifiers.Add(5);
        _sut.ModifyIncomeByValue(3);
        _sut.Income.Should().Be(8);
    }


}

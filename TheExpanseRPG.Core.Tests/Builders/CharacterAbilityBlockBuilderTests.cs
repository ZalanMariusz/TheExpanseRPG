﻿using FluentAssertions;
using Moq;
using System.Reflection;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Builders;

public class CharacterAbilityBlockBuilderTests
{
    public static IEnumerable<object[]> Abilities => new List<object[]>()
    {
        new object[]{ CharacterAbilityName.Accuracy.ToString() },
        new object[]{ CharacterAbilityName.Perception.ToString() },
        new object[]{ CharacterAbilityName.Communication.ToString() },
        new object[]{ CharacterAbilityName.Constitution.ToString() },
        new object[]{ CharacterAbilityName.Dexterity.ToString() },
        new object[]{ CharacterAbilityName.Strength.ToString() },
        new object[]{ CharacterAbilityName.Willpower.ToString() },
        new object[]{ CharacterAbilityName.Fighting.ToString() },
        new object[]{ CharacterAbilityName.Intelligence.ToString() }
    };


    //private readonly Mock<IDiceRollService> _diceRollService = new Mock<IDiceRollService>();
    private readonly Mock<IRandomGenerator> _randomGenerator;
    private readonly IDiceRollService _diceRollService;
    private readonly CharacterAbilityBlockBuilder _sut;
    public CharacterAbilityBlockBuilderTests()
    {
        _randomGenerator = new Mock<IRandomGenerator>();
        _diceRollService = new DiceRollService(_randomGenerator.Object);
        _sut = new(_diceRollService);
        /*
            RollsShouldBeReset
        */
    }
    [Fact]
    public void IsMissingAbilityRoll_ByDefaultMissesRoll()
    {
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Fact]
    public void IsMissingAbilityRoll_RollingAllRandomDoesNotMissRoll()
    {
        _randomGenerator.Setup(gen => gen.GetRandomInteger(0, 7)).Returns(1);
        _sut.SelectedAbilityRollType = AbilityRollType.AllRandom;
        _sut.RollAllRandom();
        _sut.IsMissingAbilityRoll().Should().BeFalse();
    }
    [Fact]
    public void IsMissingAbilityRoll_RollingAssigneableListMissesRoll()
    {
        _randomGenerator.Setup(gen => gen.GetRandomInteger(0, 7)).Returns(1);
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Fact]
    public void IsMissingAbilityRoll_SettingToDistributeMissesRoll()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Fact]
    public void IsMissingAbilityRoll_SpendingAllDistributablePointsDoesNotMissRoll()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        while (_sut.PointsToDistribute != 0)
        {
            _sut.IncreaseAbilityFromPool(((CharacterAbilityName)Random.Shared.Next(9)).ToString());
        }
        _sut.IsMissingAbilityRoll().Should().BeFalse();
    }
    [Fact]
    public void IsMissingAbilityRoll_NotSpendingAllDistributablePointsMissesRoll()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        while (_sut.PointsToDistribute != 1)
        {
            _sut.IncreaseAbilityFromPool(((CharacterAbilityName)Random.Shared.Next(9)).ToString());
        }
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Fact]
    public void IsMissingAbilityRoll_RollAndAssignMissesRollWhenNotEverythingIsAssigned()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        _sut.AssignAbilityScore("Accuracy", _sut.AbilityValuesToAssign[Random.Shared.Next(_sut.AbilityValuesToAssign.Count)]);
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Fact]
    public void IsMissingAbilityRoll_RollAndAssignDoesNotMissRollWhenEverythingIsAssigned()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        foreach (var item in Enum.GetValues<CharacterAbilityName>())
        {
            _sut.AssignAbilityScore(item.ToString(), _sut.AbilityValuesToAssign[Random.Shared.Next(_sut.AbilityValuesToAssign.Count)]);
        }
        _sut.IsMissingAbilityRoll().Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanIncrease_CanNotIncreaseWhenNotOnDistribute(string abilityName)
    {
        _sut.CanIncrease(abilityName).Should().BeFalse();
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanIncrease_CanNotIncreaseWhenAbilityIsOnMax(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();

        _sut.IncreaseAbilityFromPool(abilityName);
        _sut.IncreaseAbilityFromPool(abilityName);
        _sut.IncreaseAbilityFromPool(abilityName);

        _sut.CanIncrease(abilityName).Should().BeFalse();
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanIncrease_CanNotIncreaseWhenPoolIsZero(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        while (_sut.PointsToDistribute != 0)
        {
            _sut.IncreaseAbilityFromPool(((CharacterAbilityName)Random.Shared.Next(9)).ToString());
        }

        _sut.CanIncrease(abilityName).Should().BeFalse();
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanIncrease_CanIncreaseWhenPoolIsNotZeroAndRollTypeIsSetAndAbilityIsNotOnMax(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();

        _sut.CanIncrease(abilityName).Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanDecrease_CanNotDecreaseWhenNotOnDistribute(string abilityName)
    {
        _sut.CanDecrease(abilityName).Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanDecrease_CanNotCanDecreaseWhenAbilityIsOnZero(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();

        if (_sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)) == 0)
        {
            _sut.CanDecrease(abilityName).Should().BeFalse();
        }
        else
        {
            Action act = () => _sut.CanDecrease(abilityName);
            act.Should().Throw<InvalidOperationException>();
        }

    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanDecrease_CanNotDecreaseWhenPoolIsMax(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();

        if (_sut.PointsToDistribute == 12)
        {
            _sut.CanDecrease(abilityName).Should().BeFalse();
        }
        else
        {
            Action act = () => _sut.CanDecrease(abilityName);
            act.Should().Throw<InvalidOperationException>();
        }
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void CanDecrease_CanDecreaseWhenPoolIsNotMaxAndRollTypeIsSetAndAbilityIsNotZero(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IncreaseAbilityFromPool(abilityName);
        _sut.CanDecrease(abilityName).Should().BeTrue();
    }
    [Fact]
    public void RollAssigneableList_DoesNotRollWithInvalidRollType()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.AllRandom;
        _sut.RollAssignableAbilityList();
        _sut.AbilityValuesToAssign.Should().BeEmpty();
    }
    [Fact]
    public void RollAssigneableList_RollsAssigneableListWithNineValues()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        _sut.AbilityValuesToAssign.Count.Should().Be(9);
    }
    [Fact]
    public void RollAssigneableList_DoubleRollDoesNotDupeInList()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        _sut.RollAssignableAbilityList();
        _sut.AbilityValuesToAssign.Count.Should().Be(9);
    }

    [Fact]
    public void RollAllRandom_DoesNotRollWithInvalidRollType()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAllRandom();
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void IncreaseAbilityFromPool_DoesNotIncreaseWhenCanIncreaseIsFalse(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.AllRandom;
        _sut.IncreaseAbilityFromPool(abilityName);
        _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)).Should().NotBe(1);
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void IncreaseAbilityFromPool_DoesIncreaseWhenCanIncreaseIsTrue(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IncreaseAbilityFromPool(abilityName);
        _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)).Should().Be(1);
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void DecreaseAbilityFromPool_DoesNotDecreaseWhenCanDecreaseIsFalse(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();

        int? expected = _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)) - 1;

        _sut.DecreaseAbilityFromPool(abilityName);
        _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)).Should().NotBe(expected);
    }

    [Theory]
    [MemberData(nameof(Abilities))]
    public void DecreaseAbilityFromPool_DoesDecreaseWhenCanDecreaseIsTrue(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IncreaseAbilityFromPool(abilityName);

        int? expected = _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)) - 1;

        _sut.DecreaseAbilityFromPool(abilityName);
        _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)).Should().Be(expected);
    }
    [Fact]
    public void SetRolltypeToDistribute_DoesNotSetToDistributeIfSelectedRollTypeIsInvalid()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.AllRandom;
        _sut.SetRollTypeToDistribute();
        _sut.LastUsedRollType.Should().NotBe(AbilityRollType.DistributePoints);
        _sut.SelectedAbilityRollType.Should().NotBe(AbilityRollType.DistributePoints);
    }
    [Fact]
    public void SetRolltypeToDistribute_SetsToDistributeIfSelectedRollTypeIsValid()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.LastUsedRollType.Should().Be(AbilityRollType.DistributePoints);
        _sut.SelectedAbilityRollType.Should().Be(AbilityRollType.DistributePoints);
    }
    [Fact]
    public void ResetAbilities_ResetsAbilityPool()
    {
        _sut.ResetAbilities();
        _sut.PointsToDistribute.Should().Be(12);
    }
    [Theory]
    [InlineData(AbilityRollType.AllRandom, null)]
    [InlineData(AbilityRollType.DistributePoints, 0)]
    [InlineData(AbilityRollType.RollAndAssign, null)]
    public void ResetAbilities_ResetsAbilities(AbilityRollType rollType, int? expectedDefault)
    {
        _sut.SelectedAbilityRollType = rollType;
        _sut.ResetAbilities();
        _sut.CharacterAbilityBlock.AbilityList.All(x => x.AbilityValue == expectedDefault).Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void GetAbilityBonuses_ReturnsNullByDefault(string abilityName)
    {
        _sut.GetAbilityBonuses(Enum.Parse<CharacterAbilityName>(abilityName)).Should().BeNull();
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void GetAbilityBonuses_ReturnsAbilityBonusIfPresent(string abilityName)
    {
        _sut.AbilityBonuses.Add(new CharacterAbility(Enum.Parse<CharacterAbilityName>(abilityName)));
        _sut.AbilityBonuses.Add(new CharacterAbility(Enum.Parse<CharacterAbilityName>(abilityName)));
        _sut.GetAbilityBonuses(Enum.Parse<CharacterAbilityName>(abilityName)).Should().Be(2);
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void GetAbilityTotal_ReturnsAbilityValue(string abilityName)
    {
        _sut.RollAllRandom();
        CharacterAbilityName ability = Enum.Parse<CharacterAbilityName>(abilityName);
        _sut.AbilityBonuses.Add(new CharacterAbility(ability));
        int? expected = _sut.CharacterAbilityBlock.GetAbility(abilityName).AbilityValue + 1;
        _sut.GetAbilityTotal(ability).Should().Be(expected);
    }
    [Theory]
    [MemberData(nameof(Abilities))]
    public void AssignAbilityScore_AssignsValueToAbility(string abilityName)
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        int? expected = _sut.AbilityValuesToAssign[Random.Shared.Next(9)];
        _sut.AssignAbilityScore(abilityName, expected);
        _sut.GetAbilityTotal(Enum.Parse<CharacterAbilityName>(abilityName)).Should().Be(expected);
    }
    [Fact]
    public void AssignAbilityScore_AssignmentRemovesFromAssignableList()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        _sut.AssignAbilityScore("Accuracy", _sut.AbilityValuesToAssign[0]);
        _sut.AbilityValuesToAssign.Count.Should().Be(8);
    }
    [Fact]
    public void AssignAbilityScore_AssignmentSwapsIfAlreadyAssigned()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;

        _sut.RollAssignableAbilityList();
        _sut.AssignAbilityScore("Accuracy", _sut.AbilityValuesToAssign[0]);
        _sut.AssignAbilityScore("Accuracy", _sut.AbilityValuesToAssign[0]);

        _sut.GetAbilityTotal(CharacterAbilityName.Accuracy).Should().NotBeNull();
        _sut.AbilityValuesToAssign.Count.Should().Be(8);
    }
    [Fact]
    public void RollsShouldBeReset_ReturnsFalseWhenLastUsedIsTheSameAsParameter()
    {
        _sut.RollAllRandom();
        _sut.RollsShouldBeReset(AbilityRollType.AllRandom).Should().BeFalse();
    }

    [Fact]
    public void RollsShouldBeReset_RollAndAssignReturnsTrueAfterAllRandom()
    {
        _sut.RollAllRandom();
        _sut.RollsShouldBeReset(AbilityRollType.RollAndAssign).Should().BeTrue();
    }
    [Fact]
    public void RollsShouldBeReset_RollAndAssignReturnsFalseAfterDistribute()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.RollsShouldBeReset(AbilityRollType.RollAndAssign).Should().BeFalse();
    }
    [Fact]
    public void RollsShouldBeReset_RollAndAssignReturnsTrueAfterDistributeAndUsedPool()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IncreaseAbilityFromPool("Accuracy");
        _sut.RollsShouldBeReset(AbilityRollType.RollAndAssign).Should().BeTrue();
    }
    [Fact]
    public void RollsShouldBeReset_DistributePointsReturnsTrueIfItsNotTheLastUsed()
    {
        _sut.RollAllRandom();
        _sut.RollsShouldBeReset(AbilityRollType.DistributePoints).Should().BeTrue();
    }

    [Fact]
    public void RollsShouldBeReset_RollAndAssignReturnsTrueIfLastUsedIsAllRandom()
    {
        _sut.RollAllRandom();
        _sut.RollsShouldBeReset(AbilityRollType.RollAndAssign).Should().BeTrue();
    }
    [Fact]
    public void RollsShouldBeReset_RollAndAssignReturnsTruePointsHaveBeenDistributed()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IncreaseAbilityFromPool("Accuracy");
        _sut.RollsShouldBeReset(AbilityRollType.RollAndAssign).Should().BeTrue();
    }
    [Fact]
    public void RollsShouldBeReset_RollAndAssignReturnsFalseIfPointsHaveNotBeenDistributed()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.RollsShouldBeReset(AbilityRollType.RollAndAssign).Should().BeFalse();
    }
    [Fact]
    public void RollsShouldBeReset_AllRandomReturnsTrueWhenAPointIsDistributed()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        _sut.SetRollTypeToDistribute();
        _sut.IncreaseAbilityFromPool("Willpower");
        _sut.RollsShouldBeReset(AbilityRollType.AllRandom).Should().BeTrue();
    }
    [Fact]
    public void RollsShouldBeReset_AllRandomReturnsTrueRollAndAssignedIsSet()
    {
        _sut.SelectedAbilityRollType = AbilityRollType.RollAndAssign;
        _sut.RollAssignableAbilityList();
        _sut.RollsShouldBeReset(AbilityRollType.AllRandom).Should().BeTrue();
    }



    [InlineData(3, -2)]
    [InlineData(4, -1)]
    [InlineData(5, -1)]
    [InlineData(6, 0)]
    [InlineData(7, 0)]
    [InlineData(8, 0)]
    [InlineData(9, 1)]
    [InlineData(10, 1)]
    [InlineData(11, 1)]
    [InlineData(12, 2)]
    [InlineData(13, 2)]
    [InlineData(14, 2)]
    [InlineData(15, 3)]
    [InlineData(16, 3)]
    [InlineData(17, 3)]
    [InlineData(18, 4)]
    [InlineData(39, 4)]
    [Theory]
    public void GetAbilityValueFromRollTests(int roll, int expected)
    {
        MethodInfo method = _sut.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(x => x.Name == "GetAbilityValueFromRoll" && x.IsPrivate)
            .First();
        var retval = method.Invoke(_sut, new object[] { roll });
        retval.Should().Be(expected);
    }
    [Fact]
    public void LastUsedRolLTypeChanged_EventFiring()
    {
        List<object> invocationCount = new();
        _sut.LastUsedRollTypeChanged += (sender, args) => invocationCount.Add(sender!);
        _sut.RollAllRandom();
        invocationCount.Count.Should().Be(1);
    }
    [Fact]
    public void AbilityRollTypeChanged_EventFiring()
    {
        List<object> invocationCount = new();
        _sut.AbilityRollTypeChanged += (sender, args) => invocationCount.Add(sender!);
        _sut.SelectedAbilityRollType = AbilityRollType.AllRandom;
        invocationCount.Count.Should().Be(1);
    }
    [Fact]
    public void Speed_ByDefaultIsTen()
    {
        _sut.Speed.Should().Be(10);
    }
    [Fact]
    public void Defense_ByDefaultIsTen()
    {
        _sut.Defense.Should().Be(10);
    }
    [Fact]
    public void Toughness_ByDefaultIsZero()
    {
        _sut.Toughness.Should().Be(0);
    }
    [Fact]
    public void IsMissingAbilityRoll_NonDefinedAbilityRollTypeReturnsTrue()
    {
        PropertyInfo lastUsed = _sut.GetType().GetProperties().First(x => x.Name == nameof(CharacterAbilityBlockBuilder.LastUsedRollType));
        lastUsed.SetValue(_sut, (AbilityRollType)23);
        _sut.IsMissingAbilityRoll().Should().BeTrue();
    }
    [Fact]
    public void GetAbilityNameTotal_ReturnsActualAbilityTotal()
    {
        _sut.ResetAbilities();
        _sut.CharacterAbilityBlock.GetAccuracy().BaseValue = 0;
        _sut.CharacterAbilityBlock.GetCommunication().BaseValue = 1;
        _sut.CharacterAbilityBlock.GetConstitution().BaseValue = 2;
        _sut.CharacterAbilityBlock.GetDexterity().BaseValue = 3;
        _sut.CharacterAbilityBlock.GetFighting().BaseValue = 4;
        _sut.CharacterAbilityBlock.GetIntelligence().BaseValue = 5;
        _sut.CharacterAbilityBlock.GetPerception().BaseValue = 6;
        _sut.CharacterAbilityBlock.GetStrength().BaseValue = 7;
        _sut.CharacterAbilityBlock.GetWillpower().BaseValue = 8;

        _sut.GetAccuracyTotal().Should().Be(0);
        _sut.GetCommunicationTotal().Should().Be(1);
        _sut.GetConstitutionTotal().Should().Be(2);
        _sut.GetDexterityTotal().Should().Be(3);
        _sut.GetFightingTotal().Should().Be(4);
        _sut.GetIntelligenceTotal().Should().Be(5);
        _sut.GetPerceptionTotal().Should().Be(6);
        _sut.GetStrengthTotal().Should().Be(7);
        _sut.GetWillpowerTotal().Should().Be(8);
    }
    [Fact]
    public void GetAbilityBonuses_OnlyReturnsAbilityBonuses()
    {
        _sut.AbilityBonuses.Add(new Income(5));
        _sut.AbilityBonuses.Add(new CharacterAbility(CharacterAbilityName.Accuracy));
        _sut.GetAbilityBonuses(CharacterAbilityName.Accuracy).Should().Be(1);
    }
}

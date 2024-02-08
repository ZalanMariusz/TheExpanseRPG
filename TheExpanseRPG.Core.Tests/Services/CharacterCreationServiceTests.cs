using FluentAssertions;
using Moq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services
{
    public class CharacterCreationServiceTests
    {
        //    private readonly CharacterCreationService _sut;
        //    private readonly Mock<ITalentListService> _talentListService = new();
        //    private readonly Mock<IAbilityFocusListService> _abilityFocustListService = new();
        //    private readonly Mock<ICharacterBackgroundListService> _backgroundListService = new();
        //    private readonly Mock<IDiceRollService> _diceRollService = new();
        //    private readonly Mock<ICharacterProfessionListService> _professionListService = new();
        //    private readonly Mock<ICharacterDriveListService> _driveListService = new();

        //    public CharacterCreationServiceTests()
        //    {
        //        _diceRollService.Setup(x => x.Roll3D6(false)).Returns(new RollResult(new()));
        //        _professionListService.Setup(x => x.ProfessionList).Returns(new List<CharacterProfession>());

        //        _sut = new(
        //            _talentListService.Object,
        //            _abilityFocustListService.Object,
        //            _backgroundListService.Object,
        //            _diceRollService.Object,
        //            _professionListService.Object,
        //            _driveListService.Object
        //            );
        //    }
        //    [InlineData(AbilityRollType.RollAndAssign)]
        //    [InlineData(AbilityRollType.AllRandom)]
        //    [Theory]
        //    public void ResetAbilities_AllRandomAndAssignRollTypesClearsAbilitiesToNull(AbilityRollType rollType)
        //    {
        //        _sut.SelectedAbilityRollType = rollType;
        //        _sut.ResetAbilities();
        //        _sut.CharacterAbilityBlock.AbilityList.All(x => x.BaseValue is null).Should().BeTrue();
        //    }
        //    [Fact]
        //    public void ResetAbilities_DistributeRollTypeClearsAbilitiesToZero()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        _sut.ResetAbilities();
        //        _sut.CharacterAbilityBlock.AbilityList.All(x => x.BaseValue == 0).Should().BeTrue();
        //    }
        //    [Fact]
        //    public void RollAllRandomRollsBetweenMinusTwoAndFour()
        //    {
        //        _sut.RollAllRandom();
        //        _sut.CharacterAbilityBlock.AbilityList.All(x => x.BaseValue < 5 && x.BaseValue > -3).Should().BeTrue();
        //    }
        //    [Fact]
        //    public void RollAssignableAbilityList_RollsNineValues()
        //    {
        //        _sut.RollAssignableAbilityList();
        //        _sut.AbilityValuesToAssign.Any(x => x is null).Should().BeFalse();
        //    }
        //    [Fact]
        //    public void RollAssignableAbilityList_ClearsPreviousList()
        //    {
        //        _sut.RollAssignableAbilityList();
        //        _sut.RollAssignableAbilityList();
        //        _sut.AbilityValuesToAssign.Count().Should().Be(9);
        //    }
        //    [InlineData("Strength", 0, 0)]
        //    [InlineData("Accuracy", -2, -2)]
        //    [InlineData("Perception", 3, 3)]
        //    [InlineData("Willpower", null, null)]
        //    [Theory]
        //    public void AssignAbilityScore_AssignsScoreToAbility(string abilityName, int? score, int? expected)
        //    {
        //        _sut.AssignAbilityScore(abilityName, score);
        //        _sut.CharacterAbilityBlock.GetAbility(abilityName).BaseValue.Should().Be(expected);
        //    }

        //    [Fact]
        //    public void AssignAbilityScore_ClearsScoreFromAssignableList()
        //    {
        //        _sut.RollAssignableAbilityList();
        //        int? score = _sut.AbilityValuesToAssign[0];
        //        _sut.AssignAbilityScore("Strength", score);
        //        _sut.AbilityValuesToAssign.Count().Should().Be(8);
        //    }
        //    [Fact]
        //    public void AssignAbilityScore_PutsItemBackToAssignableListIfAssignmentIsReplacement()
        //    {
        //        _sut.RollAssignableAbilityList();
        //        int? score = _sut.AbilityValuesToAssign[0];
        //        _sut.AssignAbilityScore("Strength", score);
        //        score = _sut.AbilityValuesToAssign[0];
        //        _sut.AssignAbilityScore("Strength", score);
        //        _sut.AbilityValuesToAssign.Count().Should().Be(8);
        //    }
        //    [Fact]
        //    public void CanIncrease_ReturnsFalseByDefault()
        //    {
        //        _sut.CanIncrease("Strength").Should().BeFalse();
        //    }
        //    [Fact]
        //    public void CanIncrease_ReturnsTrueWithDistributePointsAsRollTypeByDefault()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        _sut.ResetAbilities();
        //        _sut.CanIncrease("Strength").Should().BeTrue();
        //    }
        //    [Fact]
        //    public void CanIncrease_ReturnsFalseIfAbilityIsThree()
        //    {
        //        _sut.CharacterAbilityBlock.GetStrength().BaseValue = 3;
        //        _sut.CanIncrease("Strength").Should().BeFalse();
        //    }
        //    [Fact]
        //    public void CanIncrease_ReturnsFalseIfPointsToDistributeIsZero()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        _sut.ResetAbilities();

        //        int iterations = _sut.PointsToDistribute;
        //        for (int i = 0; i < iterations; i++)
        //        {
        //            _sut.CharacterAbilityBlock.GetStrength().BaseValue = 0;
        //            _sut.IncreaseAbilityFromPool("Strength");
        //        }
        //        _sut.CanIncrease("Strength").Should().BeFalse();
        //    }
        //    [Fact]
        //    public void IncreaseAttributeFromPool_IncreasesStrengthFromZeroToOne()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        int expected = 1;
        //        _sut.ResetAbilities();
        //        _sut.IncreaseAbilityFromPool("Strength");
        //        _sut.CharacterAbilityBlock.GetStrength().BaseValue.Should().Be(expected);
        //    }
        //    [Fact]
        //    public void DecreaseAttributeFromPool_DecreasesStrengthFromOneToZero()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        _sut.ResetAbilities();
        //        _sut.IncreaseAbilityFromPool("Strength");
        //        int expected = 0;
        //        _sut.DecreaseAbilityFromPool("Strength");
        //        _sut.CharacterAbilityBlock.GetStrength().BaseValue.Should().Be(expected);
        //    }
        //    [Fact]
        //    public void IncreaseAttributeFromPool_IncreaseShouldDecreasePoolByOne()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        _sut.ResetAbilities();
        //        int expected = _sut.PointsToDistribute - 1;
        //        _sut.IncreaseAbilityFromPool("Strength");
        //        _sut.PointsToDistribute.Should().Be(expected);
        //    }
        //    [Fact]
        //    public void DecreaseAttributeFromPool_DecreaseShouldIncreasePoolByOne()
        //    {
        //        _sut.SelectedAbilityRollType = AbilityRollType.DistributePoints;
        //        _sut.ResetAbilities();

        //        _sut.IncreaseAbilityFromPool("Strength");
        //        _sut.IncreaseAbilityFromPool("Strength");
        //        _sut.IncreaseAbilityFromPool("Strength");

        //        int expected = _sut.PointsToDistribute + 1;
        //        _sut.DecreaseAbilityFromPool("Strength");

        //        _sut.PointsToDistribute.Should().Be(expected);
        //    }
        //}
    }
}

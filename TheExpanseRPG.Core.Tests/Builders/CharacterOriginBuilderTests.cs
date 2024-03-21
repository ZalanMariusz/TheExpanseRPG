using FluentAssertions;
using Moq;
using System.Data;
using System.Data.Common;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Builders
{
    public class CharacterOriginBuilderTests
    {
        readonly CharacterOriginBuilder _sut;
        readonly IAbilityFocusListService _abilityFocusListService;
        readonly Mock<ISqliteDatabaseConnectorService> _dbConnector = new();
        readonly Mock<IRandomGenerator> _randomGenerator = new();
        public CharacterOriginBuilderTests()
        {
            DataTable mockTable = new();
            mockTable.Columns.Add("AbilityId");
            mockTable.Columns.Add("FocusName");
            mockTable.Columns.Add("FocusDescription");
            mockTable.Rows.Add(new object?[] { 4, "Free-fall", string.Empty });
            _dbConnector.Setup(x => x.GetAbilityFocuses()).Returns(mockTable);
            _abilityFocusListService = new AbilityFocusListService(_dbConnector.Object);
            _sut = new(_abilityFocusListService, _randomGenerator.Object);
        }
        [Theory]
        [InlineData(CharacterOrigin.Mars)]
        [InlineData(CharacterOrigin.Earth)]
        public void GetOriginBonus_EarthAndMarsHasNoBonus(CharacterOrigin origin)
        {
            _sut.SelectedCharacterOrigin = origin;
            _sut.GetOriginBonus().Should().BeNull();
        }
        [Fact]
        public void GetOriginBonus_BeltersHaveFreeFallBonus()
        {
            string expected = "Dexterity (Free-fall)";
            _sut.SelectedCharacterOrigin = CharacterOrigin.Belt;
            _sut.GetOriginBonus()!.CreationBonusName.Should().Be(expected);
        }
        [Theory]
        [InlineData(CharacterOrigin.Belt,false)]
        [InlineData(CharacterOrigin.Earth,false)]
        [InlineData(CharacterOrigin.Mars,false)]
        [InlineData(null,true)]
        public void OriginDescriptions_AreInitializedProperly(CharacterOrigin? selectedOrigin,bool expected)
        {
            _sut.SelectedCharacterOrigin = selectedOrigin;
            string.IsNullOrEmpty(_sut.CurrentOriginDescription).Should().Be(expected);
        }
        [Fact]
        public void OriginChanged_FireTest()
        {
            List<object> invocationCount = new();
            _sut.OriginChanged += (sender, args) => invocationCount.Add(sender!);
            
            _sut.SelectedCharacterOrigin = CharacterOrigin.Mars;
            _sut.SelectedCharacterOrigin = null;

            invocationCount.Count.Should().Be(2);
        }
        [Fact]
        public void GenerateRandom_GeneratesData()
        {
            _randomGenerator.Setup(random => random.GetRandomInteger(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            _sut.GenerateRandom();
            _sut.SelectedCharacterOrigin.Should().NotBeNull();
        }
    }
}

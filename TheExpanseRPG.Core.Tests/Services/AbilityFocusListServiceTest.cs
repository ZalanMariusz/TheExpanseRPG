using FluentAssertions;
using Moq;
using System.Data;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Tests.Services;

public class AbilityFocusListServiceTest
{
    private static DataRow CreateFocusDataRow(DataTable table, string focusName, int abilityId)
    {
        DataRow row = table.NewRow();
        row["FocusName"] = focusName;
        row["AbilityId"] = abilityId;
        return row;
    }
    private static DataTable CreateFocusDataTable()
    {
        DataTable focusTable = new();
        focusTable.Columns.Add("FocusName", typeof(string));
        focusTable.Columns.Add("AbilityId", typeof(int));
        focusTable.Rows.Add(CreateFocusDataRow(focusTable, "focus", 0));

        return focusTable;
    }
    private readonly Mock<ISqliteDatabaseConnectorService> _sqliteConnector = new();
    private readonly AbilityFocusListService _sut;
    public AbilityFocusListServiceTest()
    {
        _sqliteConnector.Setup(connector => connector.GetAbilityFocuses()).Returns(() => CreateFocusDataTable());
        _sut = new(_sqliteConnector.Object);
    }

    [Fact]
    public void Constructor_FocusListIsInitiated()
    {
        _sut.FocusList.Count.Should().NotBe(0);
    }
    [Fact]
    public void GetFocusByName_ReturnsFocus()
    {
        _sut.GetFocusByName(CharacterAbilityName.Accuracy, "focus").Should().NotBeNull();
    }

    [Fact]
    public void GetFocusByName_ThrowsKeyNotFoundExceptionIfFocusDoesNotExists()
    {
        _sut.Invoking(x => x.GetFocusByName(CharacterAbilityName.Accuracy, "focus_"))
            .Should().Throw<KeyNotFoundException>();
    }
    [Fact]
    public void GetAbilityFocusList_ReturnsAbilityFocusList()
    {
        _sut.GetAbilityFocusList(CharacterAbilityName.Accuracy).Count.Should().Be(1);
    }
    [Fact]
    public void GetAbilityFocusList_ReturnsEmptyListIfAbilityHasNoFocus()
    {
        _sut.GetAbilityFocusList(CharacterAbilityName.Strength).Count.Should().Be(0);
    }

}

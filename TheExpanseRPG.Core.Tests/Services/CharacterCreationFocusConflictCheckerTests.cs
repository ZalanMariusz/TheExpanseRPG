using FluentAssertions;
using TheExpanseRPG.Core.Builders;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.Tests.Services;

public class CharacterCreationFocusConflictCheckerTests
{
    private const string BACKGROUNDBONUSNAME = nameof(CharacterSocialAndBackgroundBuilder.SelectedBackgroundFocus);
    private const string BACKGROUNDBENEFITNAME = nameof(CharacterSocialAndBackgroundBuilder.SelectedBackgroundBenefit);
    private const string PROFESSIONFOCUSNAME = nameof(CharacterProfessionBuilder.SelectedProfessionFocus);
    private const string ORIGINFOCUSNAME = nameof(CharacterOriginBuilder.SelectedCharacterOrigin);

    private static readonly AbilityFocus strFocus_1 = new(CharacterAbilityName.Strength, nameof(strFocus_1));
    private static readonly AbilityFocus strFocus_2 = new(CharacterAbilityName.Strength, nameof(strFocus_2));
    private static readonly AbilityFocus dexFocus_1 = new(CharacterAbilityName.Strength, nameof(dexFocus_1));
    private static readonly AbilityFocus dexFocus_2 = new(CharacterAbilityName.Strength, nameof(dexFocus_2));


    private readonly CharacterCreationFocusConflictChecker _sut = CharacterCreationFocusConflictChecker.Instance;

    public static IEnumerable<object[]> _testBonuses = new List<object[]>()
    {
        new object[] {
            new Dictionary<string, ICharacterCreationBonus>()
            {
                { BACKGROUNDBENEFITNAME,strFocus_2 },
                { BACKGROUNDBONUSNAME,strFocus_1 },
            },
            false
        },

        new object[] {
            new Dictionary<string, ICharacterCreationBonus>()
            {
                { BACKGROUNDBENEFITNAME,strFocus_2 },
                { BACKGROUNDBONUSNAME,strFocus_2 },
            },
            true
        },

        new object[] {
            new Dictionary<string, ICharacterCreationBonus>()
            {
                { ORIGINFOCUSNAME,dexFocus_1 },
                { BACKGROUNDBONUSNAME,dexFocus_1 },
            },
            true
        },

        new object[] {
            new Dictionary<string, ICharacterCreationBonus>()
            {
                { PROFESSIONFOCUSNAME,dexFocus_1 },
                { BACKGROUNDBONUSNAME,dexFocus_1 },
            },
            true
        },

    };

    public CharacterCreationFocusConflictCheckerTests()
    {
        _sut.AllBonuses.Clear();
    }
    [Theory]
    [MemberData(nameof(_testBonuses))]
    public void BackgroundFocusConflicts_HasConflictsReturnsConflicts(Dictionary<string, ICharacterCreationBonus> bonuses, bool expected)
    {
        _sut.AllBonuses = bonuses;
        _sut.HasConflitcts().Should().Be(expected);
    }
}

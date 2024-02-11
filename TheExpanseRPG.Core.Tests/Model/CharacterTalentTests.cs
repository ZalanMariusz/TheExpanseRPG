using FluentAssertions;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Tests.Model;

public class CharacterTalentTests
{
    readonly string talentName = "talentname";
    readonly List<List<ICharacterCreationBonus>> emptyRequirements = new();
    readonly string description = "description";
    readonly string noviceDescription = "novicedescription";
    readonly string expertDescription = "expertdescription";
    readonly string masterDescription = "masterdescription";
    readonly CharacterTalent _talent;
    public CharacterTalentTests()
    {
        _talent = new(
            talentName,
            emptyRequirements,
            description,
            noviceDescription,
            expertDescription,
            masterDescription
            );
    }
    [Fact]
    public void Constructor_EmptyRequirementStringIsParsedProperly()
    {
        string expected = "none";
        _talent.RequirementString.Should().Be(expected);
    }

    [Fact]
    public void Constructor_SingleAbilityRequirementItemIsParsedProperly()
    {
        string expected = "Strength 1 or higher";
        CharacterAbility ability = new(CharacterAbilityName.Strength, 1);
        List<ICharacterCreationBonus> OrRequirement = new() { ability };
        List<List<ICharacterCreationBonus>> requirementList = new() { OrRequirement };

        CharacterTalent talent = new(
           talentName,
           requirementList,
           description,
           noviceDescription,
           expertDescription,
           masterDescription
           );

        talent.RequirementString.Should().Be(expected);
    }
    [Fact]
    public void Constructor_SingleFocusRequirementItemIsParsedProperly()
    {
        AbilityFocus focus = new(CharacterAbilityName.Accuracy, "focus");
        string expected = "Accuracy(focus)";
        List<ICharacterCreationBonus> OrRequirement = new() { focus };
        List<List<ICharacterCreationBonus>> requirementList = new() { OrRequirement };

        CharacterTalent talent = new(
           talentName,
           requirementList,
           description,
           noviceDescription,
           expertDescription,
           masterDescription
           );

        talent.RequirementString.Should().Be(expected);
    }

    [Fact]
    public void Constructor_OrRequirementStringIsParsedProperly()
    {
        string expected = "Strength 1 or higher or Accuracy(focus)";
        CharacterAbility ability = new(CharacterAbilityName.Strength, 1);
        AbilityFocus focus = new(CharacterAbilityName.Accuracy, "focus");
        List<ICharacterCreationBonus> OrRequirement = new() { ability, focus };
        List<List<ICharacterCreationBonus>> requirementList = new() { OrRequirement };

        CharacterTalent talent = new(
           talentName,
           requirementList,
           description,
           noviceDescription,
           expertDescription,
           masterDescription
           );

        talent.RequirementString.Should().Be(expected);
    }
    [Fact]
    public void Constructor_AndRequirementStringIsParsed()
    {
        string expected = "Strength 1 or higher and Accuracy(focus)";
        CharacterAbility ability = new(CharacterAbilityName.Strength, 1);
        AbilityFocus focus = new(CharacterAbilityName.Accuracy, "focus");
        List<ICharacterCreationBonus> Req1 = new() { ability };
        List<ICharacterCreationBonus> Req2 = new() { focus };
        List<List<ICharacterCreationBonus>> AndRequirementList = new()
        {
            Req1,
            Req2
        };
        CharacterTalent talent = new(
           talentName,
           AndRequirementList,
           description,
           noviceDescription,
           expertDescription,
           masterDescription
           );

        talent.RequirementString.Should().Be(expected);
    }
    [Fact]
    public void ImproveTalent_TalentGetsToExpert()
    {
        _talent.ImproveTalent();
        _talent.Degree.Should().Be(TalentDegree.Expert);
    }
    [Fact]
    public void ImproveTalent_TalentGetsToMaster()
    {
        _talent.ImproveTalent();
        _talent.ImproveTalent();
    }
    [Fact]
    public void ImproveTalent_TalentCanNotBeImprovedOverMaster()
    {
        _talent.ImproveTalent();
        _talent.ImproveTalent();
        _talent.ImproveTalent();
        _talent.Degree.Should().Be(TalentDegree.Master);
    }
    //[Fact]
    //public void AreRequirementsMet_EmptyRequrementsAreAlwaysMet()
    //{
    //    CharacterAbilityBlock emptyBlock = new();
    //    _talent.AreRequirementsMet(emptyBlock).Should().BeTrue();
    //}

    //[Fact]
    //public void AreRequirementsMet_SingleAbilityRequirementIsMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 1);
    //    List<ICharacterCreationBonus> requirementList = new() { abilityRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 1;

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeTrue();
    //}
    //[Fact]
    //public void AreRequirementsMet_SingleAbilityRequirementIsNotMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    List<ICharacterCreationBonus> requirementList = new() { abilityRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 1;

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeFalse();
    //}
    //[Fact]
    //public void AreRequirementsMet_SingleFocusRequirementIsMet()
    //{
    //    AbilityFocus abilityFocus = new(CharacterAbilityName.Strength, "focus");
    //    List<ICharacterCreationBonus> requirementList = new() { abilityFocus };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.AddFocus(new(CharacterAbilityName.Strength, "focus"));

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeTrue();
    //}
    //[Fact]
    //public void AreRequirementsMet_SingleFocusRequirementIsNotMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    List<ICharacterCreationBonus> requirementList = new() { abilityRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeFalse();
    //}
    //[Fact]
    //public void AreRequirementsMet_TwoRequirementsInOneList_OnlyOneIsMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    AbilityFocus focusRequirement = new(CharacterAbilityName.Accuracy, "focus");
    //    List<ICharacterCreationBonus> requirementList = new() { abilityRequirement, focusRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 2;

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeTrue();
    //}
    //[Fact]
    //public void AreRequirementsMet_TwoRequirementsInOneList_BothAreMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    AbilityFocus focusRequirement = new(CharacterAbilityName.Accuracy, "focus");
    //    List<ICharacterCreationBonus> requirementList = new() { abilityRequirement, focusRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 2;
    //    abilityBlock.AddFocus(new AbilityFocus(CharacterAbilityName.Accuracy, "focus"));

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeTrue();
    //}
    //[Fact]
    //public void AreRequirementsMet_TwoRequirementsInOneList_NoneAreMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    AbilityFocus focusRequirement = new(CharacterAbilityName.Accuracy, "focus");
    //    List<ICharacterCreationBonus> requirementList = new() { abilityRequirement, focusRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 1;

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeFalse();
    //}
    //[Fact]
    //public void AreRequirementsMet_TwoRequirementLists_OneIsMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    AbilityFocus focusRequirement = new(CharacterAbilityName.Accuracy, "focus");
    //    List<ICharacterCreationBonus> requirementList_1 = new() { focusRequirement };
    //    List<ICharacterCreationBonus> requirementList_2 = new() { abilityRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList_1, requirementList_2 };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 2;

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeFalse();
    //}
    //[Fact]
    //public void AreRequirementsMet_TwoRequirementLists_AllAreMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    AbilityFocus focusRequirement = new(CharacterAbilityName.Accuracy, "focus");
    //    List<ICharacterCreationBonus> requirementList_1 = new() { focusRequirement };
    //    List<ICharacterCreationBonus> requirementList_2 = new() { abilityRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList_1, requirementList_2 };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 2;
    //    abilityBlock.AddFocus(new(CharacterAbilityName.Accuracy, "focus"));

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeTrue();
    //}
    //[Fact]
    //public void AreRequirementsMet_TwoRequirementLists_NoneAreMet()
    //{
    //    CharacterAbility abilityRequirement = new(CharacterAbilityName.Strength, 2);
    //    AbilityFocus focusRequirement = new(CharacterAbilityName.Accuracy, "focus");
    //    List<ICharacterCreationBonus> requirementList_1 = new() { focusRequirement };
    //    List<ICharacterCreationBonus> requirementList_2 = new() { abilityRequirement };
    //    List<List<ICharacterCreationBonus>> fullRequirements = new() { requirementList_1, requirementList_2 };

    //    CharacterAbilityBlock abilityBlock = new();
    //    abilityBlock.GetStrength().BaseValue = 1;

    //    CharacterTalent talent = new(
    //        talentName,
    //        fullRequirements,
    //        description,
    //        noviceDescription,
    //        expertDescription,
    //        masterDescription
    //    );
    //    talent.AreRequirementsMet(abilityBlock).Should().BeFalse();
}
    

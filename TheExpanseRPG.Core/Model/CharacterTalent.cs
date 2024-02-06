using System.Text;
using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public enum TalentDegree
{
    Novice,
    Expert,
    Master
}
public class CharacterTalent : ICharacterCreationBonus
{
    public string TalentName { get; }
    public TalentDegree Degree { get; private set; } = TalentDegree.Novice;
    [JsonIgnore]
    public List<List<ICharacterCreationBonus>> Requirements { get; }
    [JsonIgnore]
    public string Description { get; }
    [JsonIgnore]
    public string NoviceDescription { get; }
    [JsonIgnore]
    public string ExpertDescription { get; }
    [JsonIgnore]
    public string MasterDescription { get; }
    [JsonIgnore]
    public string RequirementString { get; }
    [JsonIgnore]
    public string CreationBonusName => TalentName;

    private readonly StringBuilder _stringBuilder = new();
    public CharacterTalent(
        string talentName,
        List<List<ICharacterCreationBonus>> requirements,
        string description,
        string noviceDescription,
        string expertDescription,
        string masterDescription
        )
    {
        TalentName = talentName;
        Requirements = requirements;
        Description = description;
        NoviceDescription = noviceDescription;
        ExpertDescription = expertDescription;
        MasterDescription = masterDescription;
        RequirementString = ParseRequirementString();
    }

    public void ImproveTalent()
    {
        if (Degree != TalentDegree.Master)
        {
            Degree++;
        }
    }

    private string ParseRequirementString()
    {
        foreach (List<ICharacterCreationBonus> requirementList in Requirements)
        {

            if (_stringBuilder.Length > 0)
            {
                _stringBuilder.Append(" and ");
            }

            string? partialRequirementSting = null;

            foreach (ICharacterCreationBonus benefit in requirementList)
            {
                if (!string.IsNullOrEmpty(partialRequirementSting))
                {
                    partialRequirementSting += " or ";
                }
                partialRequirementSting += benefit is CharacterAbility ability
                    ? $"{ability.AbilityName} {ability.BaseValue} or higher"
                    : $"{((AbilityFocus)benefit).AbilityName}({((AbilityFocus)benefit).FocusName})";
            }
            if (!string.IsNullOrEmpty(partialRequirementSting))
            {
                _stringBuilder.Append(partialRequirementSting);
            }
        }
        string result = _stringBuilder.Length > 0 ? _stringBuilder.ToString() : "none";
        return result;
    }

    public bool AreRequirementsMet(CharacterAbilityBlock abilityBlock)
    {
        if (RequirementString == "none")
        {
            return true;
        }

        return Requirements.All(requirement =>
            requirement.Any(requirementItem => IsSingleRequirementItemFulfilled(requirementItem, abilityBlock)));
    }

    private static bool IsSingleRequirementItemFulfilled(ICharacterCreationBonus requirement, CharacterAbilityBlock abilityBlock)
    {
        if (requirement is CharacterAbility requiredAbility)
        {
            CharacterAbility actualAbility = abilityBlock.GetAbility(requiredAbility.AbilityName);
            return requiredAbility.BaseValue <= actualAbility.BaseValue;
        }

        return abilityBlock.HasFocus((AbilityFocus)requirement);
    }

    public ICharacterCreationBonus ShallowCopy()
    {
        return (CharacterTalent)MemberwiseClone();
    }
}

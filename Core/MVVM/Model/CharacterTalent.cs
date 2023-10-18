using System;
using System.Collections.Generic;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public enum TalentDegree
    {
        Novice,
        Expert,
        Master
    }
    public class CharacterTalent
    {
        public string TalentName { get; }
        public TalentDegree? Degree { get; private set; }
        public List<List<CharacterAbility>> Requirements { get; }

        public string Description { get; }
        public string NoviceDescription { get; }
        public string ExpertDescription { get; }
        public string MasterDescription { get; }
        public string RequirementString { get; }
        public CharacterTalent(
            string talentName,
            List<List<CharacterAbility>> requirements,
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

        private string ParseRequirementString()
        {
            string retval = "";
            foreach (List<CharacterAbility> item in Requirements)
            {
                if (retval!="")
                {
                    retval = String.Concat(retval, " and ");
                }

                string? partialRequirementSting = null;

                foreach (CharacterAbility ability in item)
                {
                    if (!string.IsNullOrEmpty(partialRequirementSting)) 
                    {
                        partialRequirementSting = string.Concat(partialRequirementSting, " or ");
                    }
                    partialRequirementSting = string.Concat(partialRequirementSting, ability.AbilityName);
                    if (ability.Focuses.Count==0)
                    {
                        partialRequirementSting = string.Concat(partialRequirementSting, " ",ability.BaseValue, " or higher");
                    } 
                    else 
                    {
                        partialRequirementSting = string.Concat(partialRequirementSting, "(", ability.Focuses[0].FocusName, ")");
                    }
                }

                if (!string.IsNullOrEmpty(partialRequirementSting))
                {
                    retval = string.Concat(retval,partialRequirementSting);
                }
            }

            if (retval=="")
            {
                return "none";
            }
            return retval;
        }

        public bool RequirementsMet(CharacterAbilityBlock ability)
        {
            List<bool> evaluations = new();
            bool retval = true;
            if (Requirements != null)
            {
                foreach (List<CharacterAbility> list in Requirements)
                {
                    bool partialRequirement = false;
                    foreach (CharacterAbility requirement in list)
                    {
                        List<bool> partialPartialReq = new();
                        CharacterAbility abilityToTest = ability.GetAbility(requirement.AbilityName);
                        if (requirement.Focuses?.Count != 0)
                        {
                            partialPartialReq.Add(abilityToTest.Focuses.FirstOrDefault(x => x.FocusName == requirement.Focuses?[0].FocusName) != null);
                        }
                        else
                        {
                            partialPartialReq.Add(abilityToTest.BaseValue >= requirement.BaseValue);
                        }

                        foreach (var item in partialPartialReq)
                        {
                            partialRequirement = partialRequirement || item;
                        }
                    }
                    evaluations.Add(partialRequirement);
                }
                foreach (var item in evaluations)
                {
                    retval = retval && item;
                }
                return retval;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;
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
        public List<List<ICharacterCreationBonus>> Requirements { get; }

        public string Description { get; }
        public string NoviceDescription { get; }
        public string ExpertDescription { get; }
        public string MasterDescription { get; }
        public string RequirementString { get; }
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

        private string ParseRequirementString()
        {
            string retval = "";
            foreach (List<ICharacterCreationBonus> requirementItem in Requirements)
            {
                if (retval!="")
                {
                    retval = string.Concat(retval, " and ");
                }

                string? partialRequirementSting = null;

                foreach (ICharacterCreationBonus benefit in requirementItem)
                {
                    if (!string.IsNullOrEmpty(partialRequirementSting)) 
                    {
                        partialRequirementSting = string.Concat(partialRequirementSting, " or ");
                    }
                    
                    if (benefit is CharacterAbility)
                    {
                        partialRequirementSting = 
                            string.Concat(partialRequirementSting, ((CharacterAbility)benefit).AbilityName, " ", ((CharacterAbility)benefit).BaseValue, " or higher");
                    }
                    else
                    {
                        partialRequirementSting = 
                            string.Concat(partialRequirementSting, ((AbilityFocus)benefit).AbilityName, "(", ((AbilityFocus)benefit).FocusName, ")");
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

        public bool AreRequirementsMet(CharacterAbilityBlock abilityBlock)
        {
            List<bool> evaluations = new();
            bool retval = true;
            if (Requirements != null)
            {
                foreach (List<ICharacterCreationBonus> list in Requirements)
                {
                    bool partialRequirement = false;
                    foreach (ICharacterCreationBonus requirement in list)
                    {
                        List<bool> partialPartialReq = new();
                        
                        if (requirement is CharacterAbility)
                        {
                            CharacterAbility requirementItem = abilityBlock.GetAbility(((CharacterAbility)requirement).AbilityName);
                            partialPartialReq.Add(requirementItem.BaseValue >= abilityBlock.GetAbility(requirementItem.AbilityName).BaseValue);
                        }
                        else
                        {
                            partialPartialReq.Add(abilityBlock.HasFocus((AbilityFocus)requirement));
                        }

                        foreach (var item in partialPartialReq)
                        {
                            partialRequirement = partialRequirement || item;
                        }
                    }
                    evaluations.Add(partialRequirement);
                }
                foreach (bool partialRequirement in evaluations)
                {
                    retval = retval && partialRequirement;
                }
                return retval;
            }
            return true;
        }
    }
}

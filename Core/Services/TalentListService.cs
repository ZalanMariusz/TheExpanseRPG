using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Reflection;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class TalentListService : IExpanseService
    {
        public List<CharacterTalent> TalentList { get; }
        AbilityFocusListService FocusListService { get; }
        public TalentListService(AbilityFocusListService focusListService)
        {
            TalentList = new List<CharacterTalent>();
            FocusListService = focusListService;
            PopulateTalentList();
        }

        private void PopulateTalentList()
        {
            string path = Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory)!, @"Resources\ExpanseTalentList.txt");
            string[] talentListFile = File.ReadAllLines(path);
            List<string> talentBlock = new();
            foreach (string line in talentListFile)
            {
                if (line == "endtalent")
                {
                    TalentList.Add(ParseTalent(talentBlock));
                    talentBlock.Clear();
                }
                else
                {
                    talentBlock.Add(line);
                }
            }
        }

        private CharacterTalent ParseTalent(List<string> talentBlock)
        {
            return new CharacterTalent(
                ParseTalentName(talentBlock[0]),
                ParseTalentRequirement(talentBlock[1]),
                ParseTalentDescription(talentBlock[2]),
                ParseTalentNoviceDescription(talentBlock[3]),
                ParseTalentExpertDescription(talentBlock[4]),
                ParseTalentMasterDescription(talentBlock[5])
                );
        }

        private static string ParseTalentName(string line)
        {
            int listIndex = line.IndexOf(':');
            return line[(listIndex + 1)..];
        }
        
        //ok, i guess
        private List<List<CharacterAbility>> ParseTalentRequirement(string line)
        {
            int listIndex = line.IndexOf(':');
            string requirementsString = line[(listIndex + 1)..];
            List<List<CharacterAbility>> requirementList = new();
            List<string> partialRequirementStringList = new(requirementsString.Split("|"));

            foreach (var partialRequirementString in partialRequirementStringList)
            {
                List<CharacterAbility> partialRequirement = new();
                List<string> partialPartialRequirementStringList = new(partialRequirementString.Split(","));

                foreach (var partialPartialStringRequirement in partialPartialRequirementStringList)
                {
                    CharacterAbility requirementItem;
                    string[] abilityRequirement = partialPartialStringRequirement.Split(":");
                    string abilityName = abilityRequirement[0];
                    if (abilityName != "none")
                    {
                        if (int.TryParse(abilityRequirement[1], out int score))
                        {
                            requirementItem = new CharacterAbility(Enum.Parse<CharacterAbilityName>(abilityName), score);
                        }
                        else
                        {
                            string? abilityFocus = abilityRequirement[1];
                            requirementItem = new CharacterAbility(Enum.Parse<CharacterAbilityName>(abilityName));
                            AbilityFocus? focusToAdd = FocusListService.GetFocusByName(requirementItem.AbilityName, abilityFocus);
                            if (focusToAdd!=null)
                            {
                                requirementItem.Focuses.Add(focusToAdd);
                            }
                        }
                        partialRequirement.Add(requirementItem);
                    }
                }
                requirementList.Add(partialRequirement);
            }
            return requirementList;
        }

        private static string ParseTalentDescription(string line)
        {

            int listIndex = line.IndexOf(':');
            return line[(listIndex + 1)..];
        }

        private static string ParseTalentNoviceDescription(string line)
        {
            int listIndex = line.IndexOf(':');
            return line[(listIndex + 1)..];
        }

        private static string ParseTalentExpertDescription(string line)
        {
            int listIndex = line.IndexOf(':');
            return line[(listIndex + 1)..];
        }

        private static string ParseTalentMasterDescription(string line)
        {
            int listIndex = line.IndexOf(':');
            return line[(listIndex + 1)..];
        }
        public CharacterTalent GetTalent(string talentName)
        {
            CharacterTalent? playerTalent = TalentList.Find(x => x.TalentName == talentName);
            return playerTalent ?? throw new KeyNotFoundException($"{talentName} Talent not found ");
        }
    }
}

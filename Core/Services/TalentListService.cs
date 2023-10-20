using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class TalentListService : IExpanseService
    {
        public List<CharacterTalent> TalentList { get; }
        public AbilityFocusListService FocusListService { get; }
        private SqliteDatabaseConnectorService DBConnector { get; }
        public TalentListService(AbilityFocusListService focusListService, SqliteDatabaseConnectorService connectorService)
        {
            TalentList = new List<CharacterTalent>();
            FocusListService = focusListService;
            DBConnector = connectorService;
            PopulateTalentList(DBConnector.GetTalents(), DBConnector.GetTalentRequirements());
        }

        private void PopulateTalentList(DataTable talentList, DataTable talentRequirementList)
        {
            foreach (DataRow row in talentList.Rows)
            {
                EnumerableRowCollection requirements = talentRequirementList.AsEnumerable()
                    .Where(x => x.Field<string>("TalentName") == row[0].ToString());

                TalentList.Add(new CharacterTalent(
                    row[0].ToString()!,
                    ParseRequirements(requirements),
                    row[1].ToString()!,
                    row[2].ToString()!,
                    row[3].ToString()!,
                    row[4].ToString()!)
                    );
            }
        }

        private List<List<ICharacterCreationBonus>> ParseRequirements(EnumerableRowCollection talentRequirements)
        {

            List<List<ICharacterCreationBonus>> fullRequirements = new();
            foreach (DataRow row in talentRequirements)
            {
                List<ICharacterCreationBonus> partialRequirement = new List<ICharacterCreationBonus>();
                string[] requirementStringArray = row[1].ToString()!.Split(",");
                foreach (string requirementString in requirementStringArray)
                {
                    partialRequirement.Add(ParseRequirementString(requirementString));
                }
                fullRequirements.Add(partialRequirement);
            }
            return fullRequirements;
        }

        private ICharacterCreationBonus ParseRequirementString(string requirementString)
        {
            string[] requirement = requirementString.Split(":");
            CharacterAbilityName abilityName = (CharacterAbilityName)int.Parse(requirement[1]);

            if (requirement[0] == "A")
            {
                return new CharacterAbility(abilityName, int.Parse(requirement[2]));
            }
            else if (requirement[0] == "F")
            {
                return FocusListService.GetFocusByName(abilityName, requirement[2]);
            }

            throw new FormatException($"{requirementString} cannot be parsed");
        }


        public CharacterTalent GetTalent(string talentName)
        {
            CharacterTalent? playerTalent = TalentList.Find(x => x.TalentName == talentName);
            return playerTalent ?? throw new KeyNotFoundException($"{talentName} Talent not found");
        }
    }
}

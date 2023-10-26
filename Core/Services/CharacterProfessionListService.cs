using System;
using System.Collections.Generic;
using System.Data;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterProfessionListService
    {
        public List<CharacterProfession> ProfessionList { get; private set; }
        private AbilityFocusListService FocusListService { get; }
        private TalentListService TalentListService { get; }
        private SqliteDatabaseConnectorService DBConnector { get; }
        public CharacterProfessionListService(AbilityFocusListService focusListService, TalentListService talentListService, SqliteDatabaseConnectorService dbConnector)
        {
            ProfessionList = new List<CharacterProfession>();
            FocusListService = focusListService;
            TalentListService = talentListService;
            DBConnector = dbConnector;
            PopulateProfessionList(DBConnector.GetProfessions());
        }

        private void PopulateProfessionList(DataSet ProfessionDataset)
        {
            DataTable professions = ProfessionDataset.Tables["Professions"]!;
            DataTable professionTalents = ProfessionDataset.Tables["ProfessionTalents"]!;
            DataTable professionFocuses = ProfessionDataset.Tables["ProfessionFocuses"]!;

            foreach (DataRow profession in professions.Rows)
            {
                EnumerableRowCollection professionTalentByProfession = professionTalents.AsEnumerable()
                    .Where(x => x.Field<string>("ProfessionName") == profession["ProfessionName"].ToString());

                EnumerableRowCollection professionFocusByProfession = professionFocuses.AsEnumerable()
                    .Where(x => x.Field<string>("ProfessionName") == profession["ProfessionName"].ToString());

                ProfessionList.Add(new CharacterProfession(
                    profession["ProfessionName"].ToString()!,
                    profession["ProfessionDescription"].ToString()!,
                    (CharacterSocialClass)Enum.Parse(typeof(CharacterSocialClass), profession["SocialClassId"].ToString()!),
                    ParseProfessionFocuses(professionFocusByProfession),
                    ParseProfessionTalents(professionTalentByProfession)
                    ));
            }
        }

        private List<CharacterTalent> ParseProfessionTalents(EnumerableRowCollection professionTalentByProfession)
        {
            List<CharacterTalent> retval = new();
            foreach (DataRow talent in professionTalentByProfession)
            {
                retval.Add(TalentListService.GetTalent(talent["TalentName"].ToString()!));
            }
            return retval;
        }

        private List<AbilityFocus> ParseProfessionFocuses(EnumerableRowCollection professionFocusByProfession)
        {
            List<AbilityFocus> retval = new();
            foreach (DataRow focus in professionFocusByProfession)
            {
                retval.Add(FocusListService.GetFocusByName((CharacterAbilityName)Enum.Parse(typeof(CharacterAbilityName), focus["AbilityId"].ToString()!), focus["FocusName"].ToString()!));
            }
            return retval;
        }
    }
}

using System.Data;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class CharacterBackgroundListService : IExpanseService, ICharacterBackgroundListService
    {
        public List<CharacterBackGround> CharacterBackgroundList { get; }
        private IAbilityFocusListService FocusListService { get; }
        private ITalentListService TalentListService { get; }
        private ISqliteDatabaseConnectorService DBConnector { get; }

        public CharacterBackgroundListService(IAbilityFocusListService abilityFocusListService, ITalentListService talentListService, ISqliteDatabaseConnectorService dbConnector)
        {
            CharacterBackgroundList = new List<CharacterBackGround>();
            FocusListService = abilityFocusListService;
            TalentListService = talentListService;
            DBConnector = dbConnector;
            PopulateBackgroundList(DBConnector.GetBackgrounds());
        }

        private void PopulateBackgroundList(DataSet backgroundData)
        {
            DataTable backgroundFocuses = backgroundData.Tables["BackgroundFocuses"]!;
            DataTable backgroundTalents = backgroundData.Tables["BackgroundTalents"]!;
            DataTable backgroundBenefits = backgroundData.Tables["BackgroundBenefits"]!;

            foreach (DataRow backgroundMain in backgroundData.Tables["Backgrounds"]!.Rows)
            {
                EnumerableRowCollection focusesByBackgroundName = backgroundFocuses.AsEnumerable()
                    .Where(x => x.Field<string>("BackgroundName") == backgroundMain["BackgroundName"].ToString());

                EnumerableRowCollection talentsByBackgroundName = backgroundTalents.AsEnumerable()
                    .Where(x => x.Field<string>("BackgroundName") == backgroundMain["BackgroundName"].ToString());

                EnumerableRowCollection benefitsByBackgroundName = backgroundBenefits.AsEnumerable()
                    .Where(x => x.Field<string>("BackgroundName") == backgroundMain["BackgroundName"].ToString());

                CharacterBackgroundList.Add(
                    new CharacterBackGround(
                        backgroundMain["BackgroundName"].ToString()!,
                        backgroundMain["BackgroundDescription"].ToString()!,
                        (CharacterSocialClass)Enum.Parse(typeof(CharacterSocialClass), backgroundMain["MainSocialClass"].ToString()!),
                        new((CharacterAbilityName)Enum.Parse(typeof(CharacterAbilityName), backgroundMain["AbilityBonus"].ToString()!)),
                        ParseBackgroundFocuses(focusesByBackgroundName),
                        ParseBackgroundTalents(talentsByBackgroundName),
                        ParseBackgroundBenefits(benefitsByBackgroundName)
                        )
                    );
            }
        }

        private List<ICharacterCreationBonus> ParseBackgroundBenefits(EnumerableRowCollection benefitsByBackgroundName)
        {
            List<ICharacterCreationBonus> retval = new();
            foreach (DataRow benefit in benefitsByBackgroundName)
            {
                string flag = benefit["BenefitTypeFlag"].ToString()!;
                string[] benefitParams = benefit["BenefitString"].ToString()!.Split(':');
                switch (flag)
                {
                    case "A":
                        retval.Add(new CharacterAbility((CharacterAbilityName)int.Parse(benefitParams[0]), 1));
                        break;
                    case "F":
                        retval.Add(FocusListService.GetFocusByName((CharacterAbilityName)int.Parse(benefitParams[0]), benefitParams[1]));
                        break;
                    case "I":
                        retval.Add(new Income(int.Parse(benefitParams[0])));
                        break;
                }
            }
            return retval;
        }

        private List<CharacterTalent> ParseBackgroundTalents(EnumerableRowCollection talentsByBackgroundName)
        {
            List<CharacterTalent> retval = new();
            foreach (DataRow talent in talentsByBackgroundName)
            {
                retval.Add(TalentListService.GetTalent(talent["TalentName"].ToString()!));
            }
            return retval;
        }

        private List<AbilityFocus> ParseBackgroundFocuses(EnumerableRowCollection focusesByBackgroundName)
        {
            List<AbilityFocus> retval = new();
            foreach (DataRow focus in focusesByBackgroundName)
            {
                retval.Add(FocusListService.GetFocusByName((CharacterAbilityName)Enum.Parse(typeof(CharacterAbilityName), focus["AbilityId"].ToString()!), focus["FocusName"].ToString()!));
            }
            return retval;
        }
    }
}

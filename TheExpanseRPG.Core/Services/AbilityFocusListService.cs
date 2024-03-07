using System.Data;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class AbilityFocusListService : IExpanseService, IAbilityFocusListService
    {
        public List<AbilityFocus> FocusList { get; } = new List<AbilityFocus>();
        private ISqliteDatabaseConnectorService ConnectorService { get; }

        public AbilityFocusListService(ISqliteDatabaseConnectorService DBConnector)
        {
            ConnectorService = DBConnector;
            PopulateFocusList();
        }

        private void PopulateFocusList()
        {
            DataTable rawdata = ConnectorService.GetAbilityFocuses();
            foreach (DataRow row in rawdata.Rows)
            {
                CharacterAbilityName abilityName = IntToAbilityName(Convert.ToInt32(row[rawdata.Columns["AbilityId"]!]));
                FocusList.Add(new AbilityFocus(abilityName, row[rawdata.Columns["FocusName"]!].ToString()!, row[rawdata.Columns["FocusDescription"]!].ToString()!));
            }
        }
        public AbilityFocus GetFocusByName(CharacterAbilityName abilityName, string focusName)
        {
            AbilityFocus? retval = FocusList.FirstOrDefault(x => x.FocusName == focusName && x.AbilityName == abilityName);
            return retval ?? throw new KeyNotFoundException($"{abilityName}({focusName}) Focus not found");
        }
        public List<AbilityFocus> GetAbilityFocusList(CharacterAbilityName abilityName)
        {
            return FocusList.FindAll(x => x.AbilityName == abilityName);
        }

        private static CharacterAbilityName IntToAbilityName(int n)
        {
            return (CharacterAbilityName)n;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public class AbilityFocusListService : IExpanseService
    {
        public List<AbilityFocus> FocusList { get; } = new List<AbilityFocus>();
        private SqliteDatabaseConnectorService ConnectorService { get; }

        public AbilityFocusListService(SqliteDatabaseConnectorService DBConnector)
        {
            ConnectorService = DBConnector;
            PopulateFocusList();
        }

        private void PopulateFocusList()
        {
            DataTable rawdata = ConnectorService.GetAbilityFocuses();
            foreach (DataRow row in rawdata.Rows)
            {
                CharacterAbilityName abilityFocusName = IntToAbilityName(Convert.ToInt32(row[rawdata.Columns[0]]));
                FocusList.Add(new AbilityFocus(abilityFocusName, row[rawdata.Columns[1]].ToString()));
            }
        }

        private CharacterAbilityName IntToAbilityName(int n)
        {
            return (CharacterAbilityName)n;
        }

        public AbilityFocus GetFocusByName(CharacterAbilityName abilityName, string focusName)
        {
            AbilityFocus? retval = FocusList.Find(x => x.FocusName == focusName && x.AbilityName == abilityName);
            return retval ?? throw new KeyNotFoundException($"{abilityName}({focusName}) Focus not found");
        }
        public List<AbilityFocus> GetAbilityFocusList(CharacterAbilityName abilityName)
        {
            return FocusList.FindAll(x => x.AbilityName == abilityName);
        }
    }
}

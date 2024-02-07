using System.Data;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface ITalentListService
    {
        List<CharacterTalent> TalentList { get; }
        CharacterTalent GetTalent(string talentName);
    }
}
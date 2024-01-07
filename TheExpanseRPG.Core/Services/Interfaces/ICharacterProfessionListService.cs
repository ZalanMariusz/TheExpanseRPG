using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface ICharacterProfessionListService
    {
        List<CharacterProfession> ProfessionList { get; }
    }
}
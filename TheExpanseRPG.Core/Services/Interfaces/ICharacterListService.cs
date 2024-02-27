using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Services.Interfaces;

public interface ICharacterListService
{
    //List<ExpanseCharacter> CharacterList { get; }

    List<ExpanseCharacter> GetCharacterList();
}

using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Relationship : CharacterTie, ICharacterCreationBonus
{
    public string CreationBonusName => "Relationship";

    public ICharacterCreationBonus ShallowCopy()
    {
        return (Relationship)MemberwiseClone();
    }
}

using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Membership : CharacterTie, ICharacterCreationBonus
{
    public string CreationBonusName => "Membership";

    public ICharacterCreationBonus ShallowCopy()
    {
        return (Membership)MemberwiseClone();
    }
}

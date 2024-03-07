using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Reputation : CharacterTie, ICharacterCreationBonus
{
    public string CreationBonusName => "Reputation";

    public ICharacterCreationBonus ShallowCopy()
    {
        return (Reputation)MemberwiseClone();
    }
}

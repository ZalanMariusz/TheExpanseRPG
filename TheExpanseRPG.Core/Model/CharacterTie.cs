using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public abstract class CharacterTie : ICharacterCreationBonus
{
    public virtual string CreationBonusName { get; set; } = string.Empty;

    public abstract string Description { get; set; }

    public ICharacterCreationBonus ShallowCopy()
    {
        return (CharacterTie)MemberwiseClone();
    }
}

using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Membership : ICharacterCreationBonus
{
    public string CreationBonusName => "Membership";

    public ICharacterCreationBonus ShallowCopy()
    {
        return (Membership)MemberwiseClone();
    }
    public string? Description { get; set; } = string.Empty;
}

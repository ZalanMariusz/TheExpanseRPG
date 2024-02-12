using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Relationship : ICharacterCreationBonus
{
    public string CreationBonusName => "Relationship";

    public ICharacterCreationBonus ShallowCopy()
    {
        return (Relationship)MemberwiseClone();
    }
    public string? Description { get; set; } = string.Empty;
}

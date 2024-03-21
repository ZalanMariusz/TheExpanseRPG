using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Relationship : CharacterTie, ICharacterCreationBonus
{
    public override string CreationBonusName => "Relationship";

    public override string Description { get; set; } = string.Empty;
}

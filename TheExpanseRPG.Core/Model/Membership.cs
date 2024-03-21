using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;

public class Membership : CharacterTie, ICharacterCreationBonus
{
    public override string CreationBonusName => "Membership";

    public override string Description { get; set; } = string.Empty;
}

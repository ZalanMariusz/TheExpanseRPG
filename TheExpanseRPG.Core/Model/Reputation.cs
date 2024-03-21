namespace TheExpanseRPG.Core.Model;

public class Reputation : CharacterTie
{
    public override string CreationBonusName => "Reputation";

    public override string Description { get; set; } = string.Empty;
}

using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{
    public class Reputation : ICharacterCreationBonus
    {
        public string CreationBonusName => "Reputation";

        public ICharacterCreationBonus ShallowCopy()
        {
            return (Reputation)MemberwiseClone();
        }
    }
}

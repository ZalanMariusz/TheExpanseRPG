using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{
    public class Fortune : ICharacterCreationBonus
    {
        public int Value { get; set; }
        public string CreationBonusName => $"+{Value} Fortune";

        public ICharacterCreationBonus ShallowCopy()
        {
            return (Fortune)MemberwiseClone();
        }
    }
}

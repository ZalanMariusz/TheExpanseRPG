using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model.Talents
{
    public class Affluent : CharacterTalent
    {
        public Affluent(
            List<List<ICharacterCreationBonus>> requirements,
            string description,
            string noviceDescription,
            string expertDescription,
            string masterDescription) : base("Affluent", requirements, description, noviceDescription, expertDescription, masterDescription)
        {

        }

        public int ApplyTalentEffect()
        {
            return (int)Degree + 2;
        }

    }
}

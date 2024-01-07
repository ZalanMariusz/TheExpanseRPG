using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.Model.Talents
{
    public class Affluent : CharacterTalent
    {
        public Affluent(string talentName,
            List<List<ICharacterCreationBonus>> requirements,
            string description,
            string noviceDescription,
            string expertDescription,
            string masterDescription) : base(talentName, requirements, description, noviceDescription, expertDescription, masterDescription)
        {

        }

        public int ApplyTalentEffect()
        {
            return (int)Degree + 2;
        }

    }
}

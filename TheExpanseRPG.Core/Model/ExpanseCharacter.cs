using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Model
{
    public class ExpanseCharacter
    {
        public ExpanseCharacter()
        {
            Talents = new List<CharacterTalent>();
            Focuses = new List<AbilityFocus>();
        }
        public int Level { get; private set; } = 1;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CharacterAbilityBlock? Abilities { get; set; }
        public CharacterOrigin? Origin { get; set; }
        public string Background { get; set; }
        public List<AbilityFocus> Focuses { get; set; }
        public List<CharacterTalent> Talents { get; set; }
        public CharacterSocialClass? SocialClass { get; internal set; }
        public string Profession { get; internal set; }
        public string Drive { get; internal set; }
        public int Fortune { get; internal set; }
        public int? Income { get; internal set; }
        public int? Speed { get; internal set; }
        public int Toughness { get; internal set; }
        public int Defense { get; internal set; }
        public string Avatar { get; internal set; }

        public bool CanLearnTalent(CharacterTalent talent)
        {
            return talent.AreRequirementsMet(Abilities!);
        }
        //public string CharacterName { get; set; }
        //public string Abilities { get; set; }

        //public string BackGround { get; set; }
        //public string Profession { get; set; }
        //public string SocialClass { get; set; }
        //public int Income { get; set; }
        //public int Defense { get; set; }
        //public int Speed { get; set; }
        //public int Toughness { get; set; }
        //public int Fortune { get; set; }
        //public List<string> Goals { get; set; }
        //public string Drive { get; set; }


        //public ExpanseCharacter(
        //    string characterName,
        //    string origin,
        //    string socialClass,
        //    string backGround
        //    )
        //{
        //    CharacterName = characterName;
        //    SocialClass = socialClass;
        //    BackGround = backGround;
        //}

    }
}

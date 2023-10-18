using System.Collections.Generic;
using System.Net.Http.Headers;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.MVVM.Model
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
        public string? Description { get; }
        public CharacterAbilityBlock? Abilities { get; }
        public CharacterOrigin? Origin { get; }
        public CharacterBackGround? Background { get; }
        public List<AbilityFocus> Focuses { get; }
        public List<CharacterTalent> Talents { get; }
        public bool CanLearnTalent(CharacterTalent talent)
        {
            return talent.RequirementsMet(Abilities!);
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

using System.Collections.Generic;
using System.Web;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class ExpanseCharacter
    {
        public string Name { get; }
        public string Description { get; }
        public CharacterAttributeBlock Attributes { get; }
        public CharacterOrigin Origin {  get; }


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

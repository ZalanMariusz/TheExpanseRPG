using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Model
{
    public class ExpanseCharacter
    {
        public ExpanseCharacter()
        {
            Fortune = 15;
            Talents = new List<CharacterTalent>();
            Focuses = new List<AbilityFocus>();
        }
        public int Level { get; private set; } = 1;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CharacterAbilityBlock Abilities { get; set; } = new();
        public CharacterOrigin? Origin { get; set; }
        public string Background { get; set; } = string.Empty;
        public List<AbilityFocus> Focuses { get; set; }
        public List<CharacterTalent> Talents { get; set; }
        public CharacterSocialClass? SocialClass { get; set; }
        public string Profession { get; set; } = string.Empty;
        public string Drive { get; set; } = string.Empty;
        public int Fortune { get; set; }
        public int? Income { get; set; }
        public int? Speed { get; set; }
        public int Toughness { get; set; }
        public int Defense { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public List<Relationship> Relationships { get; set; } = new();
        public List<Membership> Memberships { get; set; } = new();
        public List<Reputation> Reputations { get; set; } = new();
        public bool CanLearnTalent(CharacterTalent talent)
        {
            return talent.AreRequirementsMet(Focuses, Abilities);
        }
    }
}

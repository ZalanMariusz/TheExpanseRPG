using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Model
{
    public class ExpanseCharacter
    {
        private const int THOUGHNESSBASE = 0;
        private const int DEFENSEBASE = 10;
        private const int SPEEDBASE = 10;
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
        public List<int> IncomeModifiers { get; } = new();
        [JsonIgnore]
        public int? Speed => SPEEDBASE + SpeedModifiers.Sum();
        public List<int> SpeedModifiers { get; } = new();
        [JsonIgnore]
        public int Toughness => THOUGHNESSBASE + ThoughnessModifiers.Sum();
        public List<int> ThoughnessModifiers { get; } = new();
        [JsonIgnore]
        public int Defense => DEFENSEBASE + DefenseModifiers.Sum();
        public List<int> DefenseModifiers { get; } = new();

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

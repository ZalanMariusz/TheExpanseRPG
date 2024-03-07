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

        public int Accuracy => (int)Abilities.GetAccuracy().AbilityValue!;
        public int Communication => (int)Abilities.GetCommunication().AbilityValue!;
        public int Constitution => (int)Abilities.GetConstitution().AbilityValue!;
        public int Dexterity => (int)Abilities.GetDexterity().AbilityValue!;
        public int Fighting => (int)Abilities.GetFighting().AbilityValue!;
        public int Intelligence => (int)Abilities.GetIntelligence().AbilityValue!;
        public int Perception => (int)Abilities.GetPerception().AbilityValue!;
        public int Strength => (int)Abilities.GetStrength().AbilityValue!;
        public int Willpower => (int)Abilities.GetWillpower().AbilityValue!;

        public List<AbilityFocus> AccuracyFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Accuracy).ToList();
        public List<AbilityFocus> CommunicationFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Communication).ToList();
        public List<AbilityFocus> ConstitutionFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Constitution).ToList();
        public List<AbilityFocus> DexterityFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Dexterity).ToList();
        public List<AbilityFocus> FightingFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Fighting).ToList();
        public List<AbilityFocus> IntelligenceFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Intelligence).ToList();
        public List<AbilityFocus> PerceptionFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Perception).ToList();
        public List<AbilityFocus> StrengthFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Strength).ToList();
        public List<AbilityFocus> WillpowerFocuses => Focuses.Where(x => x.AbilityName == CharacterAbilityName.Willpower).ToList();


        public int Level { get; private set; } = 1;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CharacterAbilityBlock Abilities { get; set; } = new();
        public CharacterOrigin? Origin { get; set; }
        public string Background { get; set; } = string.Empty;
        //[JsonConverter(typeof(AbilityFocusJsonConverter))]
        public List<AbilityFocus> Focuses { get; set; }
        public List<CharacterTalent> Talents { get; set; }
        public CharacterSocialClass? SocialClass { get; set; }
        public string Profession { get; set; } = string.Empty;
        public string Drive { get; set; } = string.Empty;
        public int Fortune { get; set; }
        public int? Income { get; set; }
        public List<int> IncomeModifiers { get; set; } = new();
        [JsonIgnore]
        public int? Speed => SPEEDBASE + SpeedModifiers.Sum();
        public List<int> SpeedModifiers { get; set; } = new();
        [JsonIgnore]
        public int Thoughness => THOUGHNESSBASE + ThoughnessModifiers.Sum();
        public List<int> ThoughnessModifiers { get; set; } = new();
        [JsonIgnore]
        public int Defense => DEFENSEBASE + DefenseModifiers.Sum();
        public List<int> DefenseModifiers { get; set; } = new();
        public int Armor => Thoughness + ArmorModifiers.Sum();
        public List<int> ArmorModifiers { get; set; } = new();
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

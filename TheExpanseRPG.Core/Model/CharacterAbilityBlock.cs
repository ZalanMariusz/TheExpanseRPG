using TheExpanseRPG.Core.Enums;

namespace TheExpanseRPG.Core.Model
{
    public class CharacterAbilityBlock
    {
        public List<CharacterAbility> AbilityList { get; set; }
        public CharacterAbilityBlock()
        {
            AbilityList = new List<CharacterAbility>();
            InitAbilities();
        }

        private void InitAbilities()
        {
            foreach (CharacterAbilityName ability in Enum.GetValues<CharacterAbilityName>())
            {
                AbilityList.Add(new CharacterAbility(ability) { });
            }
        }

        #region character attribute getters
        public CharacterAbility GetAbility(CharacterAbilityName attributeName)
        {
            CharacterAbility characterAttribute = AbilityList.FirstOrDefault(a => a.AbilityName == attributeName)!;
            return characterAttribute ?? throw (new KeyNotFoundException($"{attributeName} is not a valid Attribute Name."));
        }
        public CharacterAbility GetAbility(string attributeName)
        {
            if (Enum.TryParse(attributeName, out CharacterAbilityName abilityEnum))
            {
                return GetAbility(abilityEnum);
            }
            throw new KeyNotFoundException($"{attributeName} is not a valid Attribute Name.");
        }
        public CharacterAbility GetStrength()
        {
            return GetAbility(CharacterAbilityName.Strength);
        }
        public CharacterAbility GetConstitution()
        {
            return GetAbility(CharacterAbilityName.Constitution);
        }
        public CharacterAbility GetAccuracy()
        {
            return GetAbility(CharacterAbilityName.Accuracy);
        }
        public CharacterAbility GetIntelligence()
        {
            return GetAbility(CharacterAbilityName.Intelligence);
        }
        public CharacterAbility GetPerception()
        {
            return GetAbility(CharacterAbilityName.Perception);
        }
        public CharacterAbility GetCommunication()
        {
            return GetAbility(CharacterAbilityName.Communication);
        }
        public CharacterAbility GetDexterity()
        {
            return GetAbility(CharacterAbilityName.Dexterity);
        }
        public CharacterAbility GetFighting()
        {
            return GetAbility(CharacterAbilityName.Fighting);
        }
        public CharacterAbility GetWillpower()
        {
            return GetAbility(CharacterAbilityName.Willpower);
        }
        #endregion
    }
}

using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Builders
{
    public class ExpanseCharacterBuilder : IExpanseCharacterBuilder
    {
        private readonly ExpanseCharacter _character = new();
        public ExpanseCharacter Create()
        {
            _character.Speed = 10 + _character.Abilities.GetDexterity().AbilityValue;
            _character.Defense = 10 + (int)_character.Abilities.GetDexterity().AbilityValue!;
            _character.Toughness = (int)_character.Abilities.GetConstitution().AbilityValue!;
            return _character;
        }

        public IExpanseCharacterBuilder SetCharacterAbilityBlock(CharacterAbilityBlock abilityBlock)
        {
            _character.Abilities = abilityBlock;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterAvatar(string characterAvatar)
        {
            _character.Avatar = characterAvatar;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterBackground(CharacterOrigin? characterOrigin)
        {
            _character.Origin = characterOrigin;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterDescription(string characterDescription)
        {
            _character.Description = characterDescription;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterDrive(string driveName)
        {
            _character.Drive = driveName;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterFocuses(List<AbilityFocus> focuses)
        {
            _character.Focuses = focuses;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterName(string characterName)
        {
            _character.Name = characterName;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterOrigin(string backgroundName)
        {
            _character.Background = backgroundName;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterProfession(string professionName)
        {
            _character.Profession = professionName;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterSocialClass(CharacterSocialClass? selectedCharacterSocialClass)
        {
            _character.SocialClass = selectedCharacterSocialClass;
            return this;
        }

        public IExpanseCharacterBuilder SetCharacterTalents(List<CharacterTalent> talents)
        {
            _character.Talents = talents;
            return this;
        }
        public IExpanseCharacterBuilder SetCharacterAbilityBonuses(List<ICharacterCreationBonus> abilityBonuses)
        {
            foreach (var abilityBonus in abilityBonuses)
            {
                _character.Abilities.GetAbility(((CharacterAbility)abilityBonus).AbilityName).BaseValue++;
            }
            return this;
        }
    }
}

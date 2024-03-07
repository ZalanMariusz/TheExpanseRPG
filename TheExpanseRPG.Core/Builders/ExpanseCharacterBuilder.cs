using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Builders
{
    public class ExpanseCharacterBuilder :
        ICharacterOriginCreationStage,
        ICharacterSocialClassCreationStage,
        ICharacterBackgroundCreationStage,
        ICharacterProfessionCreationStage,
        IDriveCreationStage,
        IDriveBonusCreationStage,
        ICharacterAbilityBlockCreationStage,
        IAbilityBonusCreationStage,
        ICharacterFocusCreationStage,
        ICharacterTalentCreationStage,
        ICharacterNameCreationStage,
        ICharacterDescriptionCreationStage,
        ICharacterAvatarCreationStage,
        IINcomeCreationStage
    {
        private ExpanseCharacterBuilder() { }
        private readonly ExpanseCharacter _character = new();
        public static ICharacterOriginCreationStage StartCreateCharacter()
        {
            return new ExpanseCharacterBuilder();
        }
        public ICharacterSocialClassCreationStage WithOrigin(CharacterOrigin? origin)
        {
            _character.Origin = origin;
            return this;
        }

        public ICharacterBackgroundCreationStage AndSocialClass(CharacterSocialClass? socialClass)
        {
            _character.SocialClass = socialClass;
            return this;
        }

        public ICharacterProfessionCreationStage AndBackground(string backgroundName)
        {
            _character.Background = backgroundName;
            return this;
        }
        public IDriveCreationStage AndProfession(string professionName)
        {
            _character.Profession = professionName;
            return this;
        }
        public IDriveBonusCreationStage AndDrive(string driveName)
        {
            _character.Drive = driveName;
            return this;
        }
        public ICharacterAbilityBlockCreationStage WithDriveBonus(ICharacterCreationBonus? driveBonus)
        {
            if (driveBonus is (Relationship))
            {
                _character.Relationships.Add((driveBonus as Relationship)!);
            }
            if (driveBonus is (Membership))
            {
                _character.Memberships.Add((driveBonus as Membership)!);
            }
            if (driveBonus is (Reputation))
            {
                _character.Reputations.Add((driveBonus as Reputation)!);
            }
            if (driveBonus is (Fortune))
            {
                _character.Fortune += 5;
            }
            return this;
        }

        public IAbilityBonusCreationStage AddAbilityBlock(CharacterAbilityBlock abilityBlock)
        {
            _character.Abilities = abilityBlock;
            _character.SpeedModifiers.Add((int)abilityBlock.GetDexterity().AbilityValue!);
            _character.DefenseModifiers.Add((int)abilityBlock.GetDexterity().AbilityValue!);
            _character.ThoughnessModifiers.Add((int)abilityBlock.GetConstitution().AbilityValue!);

            return this;
        }
        public ICharacterFocusCreationStage WithAbilityBonuses(List<ICharacterCreationBonus> abilityBonuses)
        {
            foreach (var abilityBonus in abilityBonuses)
            {
                _character.Abilities.GetAbility(((CharacterAbility)abilityBonus).AbilityName).BaseValue++;
            }
            return this;
        }

        public ICharacterTalentCreationStage AddFocuses(List<AbilityFocus> focuses)
        {
            _character.Focuses = focuses;
            return this;
        }
        public ICharacterNameCreationStage AndTalents(List<CharacterTalent> talents)
        {
            _character.Talents = talents;
            return this;
        }

        public ICharacterDescriptionCreationStage SetCharacterName(string characterName)
        {
            _character.Name = characterName;
            return this;
        }
        public ICharacterAvatarCreationStage AndDescription(string description)
        {
            _character.Description = description;
            return this;
        }
        public IINcomeCreationStage AndAvatar(string avatarPath)
        {
            _character.Avatar = avatarPath;
            return this;
        }
        public ExpanseCharacter SetIncome(int income)
        {
            _character.Income = income;
            return _character;
        }


    }
    public interface ICharacterOriginCreationStage
    {
        ICharacterSocialClassCreationStage WithOrigin(CharacterOrigin? origin);
    }
    public interface ICharacterSocialClassCreationStage
    {
        ICharacterBackgroundCreationStage AndSocialClass(CharacterSocialClass? socialClass);
    }
    public interface ICharacterBackgroundCreationStage
    {
        ICharacterProfessionCreationStage AndBackground(string backgroundName);
    }
    public interface ICharacterProfessionCreationStage
    {
        IDriveCreationStage AndProfession(string professionName);
    }
    public interface IDriveCreationStage
    {
        IDriveBonusCreationStage AndDrive(string driveName);
    }
    public interface IDriveBonusCreationStage
    {
        ICharacterAbilityBlockCreationStage WithDriveBonus(ICharacterCreationBonus? driveBonus);
    }
    public interface ICharacterAbilityBlockCreationStage
    {
        IAbilityBonusCreationStage AddAbilityBlock(CharacterAbilityBlock abilityBlock);
    }
    public interface IAbilityBonusCreationStage
    {
        ICharacterFocusCreationStage WithAbilityBonuses(List<ICharacterCreationBonus> abilityBonuses);
    }

    public interface ICharacterFocusCreationStage
    {
        ICharacterTalentCreationStage AddFocuses(List<AbilityFocus> focuses);
    }
    public interface ICharacterTalentCreationStage
    {
        ICharacterNameCreationStage AndTalents(List<CharacterTalent> talents);
    }
    public interface ICharacterNameCreationStage
    {
        ICharacterDescriptionCreationStage SetCharacterName(string characterName);
    }
    public interface ICharacterDescriptionCreationStage
    {
        ICharacterAvatarCreationStage AndDescription(string characterDescription);
    }
    public interface ICharacterAvatarCreationStage
    {
        IINcomeCreationStage AndAvatar(string avatarPath);
    }
    public interface IINcomeCreationStage
    {
        ExpanseCharacter SetIncome(int income);
    }

}

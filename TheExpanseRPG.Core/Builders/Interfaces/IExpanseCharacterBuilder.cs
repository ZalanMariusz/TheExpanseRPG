using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Builders.Interfaces
{
    public interface IExpanseCharacterBuilder
    {
        IExpanseCharacterBuilder SetCharacterAbilityBlock(CharacterAbilityBlock abilityBlock);
        IExpanseCharacterBuilder SetCharacterFocuses(List<AbilityFocus> focuses);
        IExpanseCharacterBuilder SetCharacterTalents(List<CharacterTalent> talents);
        IExpanseCharacterBuilder SetCharacterName(string characterName);
        IExpanseCharacterBuilder SetCharacterDescription(string characterDescription);
        IExpanseCharacterBuilder SetCharacterOrigin(string backgroundName);
        IExpanseCharacterBuilder SetCharacterBackground(CharacterOrigin? characterOrigin);
        IExpanseCharacterBuilder SetCharacterSocialClass(CharacterSocialClass? selectedCharacterSocialClass);
        IExpanseCharacterBuilder SetCharacterProfession(string professionName);
        IExpanseCharacterBuilder SetCharacterDrive(string driveName);
        IExpanseCharacterBuilder SetCharacterAvatar(string characterAvatar);
        IExpanseCharacterBuilder SetCharacterAbilityBonuses(List<ICharacterCreationBonus> abilityBonuses);
        ExpanseCharacter Create();
    }
}

using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public interface ICharacterCreationService : IExpanseService
    {
        ICharacterAbilityBlockBuilder AbilityBlockBuilder { get; set; }
        Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; }
        string CharacterAvatar { get; set; }
        string CharacterDescription { get; set; }
        string CharacterName { get; set; }
        int Defense { get; }
        ICharacterDriveBuilder DriveBuilder { get; set; }
        List<AbilityFocus> FocusBonuses { get; set; }
        int Fortune { get; }
        ICharacterOriginBuilder OriginBuilder { get; set; }
        ICharacterProfessionBuilder ProfessionBuilder { get; set; }
        ICharacterSocialAndBackgroundBuilder SocialAndBackgroundBuilder { get; set; }
        int? Speed { get; }
        List<CharacterTalent> TalentBonuses { get; set; }
        int Toughness { get; }
        bool CanCreateCharacter();
        bool CharacterExists();
        void CreateCharacter();
        List<string> GetBackgroundBenefitConflicts();
        List<string> GetBackgroundFocusConflicts();
        List<string> GetOriginFocusConflicts();
        List<string> GetProfessionFocusConflicts();
        int? GetTotalIncome();
        bool HasBackgroundConflict();
        bool HasOriginConflict();
        bool HasProfessionConflict();
        void RandomizeCharacter();
    }
}
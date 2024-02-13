using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    public interface ICharacterCreationService : IExpanseService
    {
        
        ICharacterOriginBuilder OriginBuilder { get; }
        ICharacterSocialAndBackgroundBuilder SocialAndBackgroundBuilder { get; }
        ICharacterProfessionBuilder ProfessionBuilder { get; }
        ICharacterDriveBuilder DriveBuilder { get; }
        ICharacterAbilityBlockBuilder AbilityBlockBuilder { get; }
        
        int? GetTotalIncome();
        Dictionary<string, ICharacterCreationBonus> AllBonuses { get; set; }
        List<AbilityFocus> FocusBonuses { get; set; }
        List<CharacterTalent> TalentBonuses { get; set; }
        List<Income> IncomeBonuses { get; set; }
        string CharacterAvatar { get; set; }
        string CharacterName { get; set; }
        string CharacterDescription { get; set; }
        

        bool CanCreateCharacter();
        bool CharacterExists();
        void CreateCharacter();
        void RandomizeCharacter();
    }
}
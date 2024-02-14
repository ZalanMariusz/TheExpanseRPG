using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Builders.Interfaces
{
    public interface ICharacterSocialAndBackgroundBuilder
    {
        string? SelectedCharacterSocialClassDescription { get; }
        CharacterSocialClass? SelectedCharacterSocialClass { get; set; }
        CharacterBackGround? SelectedCharacterBackground { get; set; }
        ICharacterCreationBonus? SelectedBackgroundBenefit { get; set; }
        AbilityFocus? SelectedBackgroundFocus { get; set; }
        CharacterTalent? SelectedBackgroundTalent { get; set; }
        
        //string? SelectedCharacterSocialClassDescription { get; }
        List<SocialClassWrapper> SocialClassesWithDescription { get; set; }

        event EventHandler<string>? BonusSelectionChanged;
        event EventHandler<CharacterSocialClass?>? SocialClassChanged;

        void GenerateRandom();
        bool IsMissingBackgroundBonus();
        CharacterAbility? GetAbilityBonus();
    }
}
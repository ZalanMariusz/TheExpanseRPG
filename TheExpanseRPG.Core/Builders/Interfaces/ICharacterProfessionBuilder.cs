using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Builders.Interfaces;
public interface ICharacterProfessionBuilder
{
    List<CharacterProfession> LowerclassProfessions { get; }
    List<CharacterProfession> MiddleclassProfessions { get; }
    List<CharacterProfession> OutsiderProfessions { get; }
    List<CharacterProfession> UpperclassProfessions { get; }
    CharacterProfession? SelectedCharacterProfession { get; set; }
    AbilityFocus? SelectedProfessionFocus { get; set; }
    CharacterTalent? SelectedProfessionTalent { get; set; }


    event EventHandler<string>? BonusSelectionChanged;
    event EventHandler<string>? SelectedProfessionChanged;
    void ClearSelectedProfession(CharacterSocialClass? selectedCharacterSocialClass);
    void GenerateRandom(CharacterSocialClass? selectedCharacterSocialClass);
    bool IsMissingProfessionBonus();
}
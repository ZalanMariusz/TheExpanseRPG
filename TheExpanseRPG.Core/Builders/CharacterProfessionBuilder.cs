using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders;

internal class CharacterProfessionBuilder : ICharacterProfessionBuilder
{
    private ICharacterProfessionListService ProfessionListService { get; set; }
    private CharacterProfession? _selectedCharacterProfession;
    private CharacterTalent? _selectedProfessionTalent;
    private AbilityFocus? _selectedProfessionFocus;

    public event EventHandler<string>? SelectedProfessionChanged;
    public event EventHandler<string>? ProfessionFocusChanged;
    public event EventHandler<string>? ProfessionTalentChanged;
    public CharacterProfession? SelectedCharacterProfession
    {
        get => _selectedCharacterProfession;
        set
        {
            ClearProfessionBonuses(value);
            _selectedCharacterProfession = value;
            SelectedProfessionChanged?.Invoke(this, nameof(SelectedCharacterProfession));
        }
    }
    public CharacterTalent? SelectedProfessionTalent
    {
        get => _selectedProfessionTalent;
        set
        {
            _selectedProfessionTalent = value;
            ProfessionTalentChanged?.Invoke(this, nameof(SelectedProfessionTalent));
        }
    }
    public AbilityFocus? SelectedProfessionFocus
    {
        get => _selectedProfessionFocus;
        set
        {
            _selectedProfessionFocus = value;
            ProfessionFocusChanged?.Invoke(this, nameof(SelectedProfessionFocus));
        }
    }
    public List<CharacterProfession> OutsiderProfessions { get; private set; } = new();
    public List<CharacterProfession> LowerclassProfessions { get; private set; } = new();
    public List<CharacterProfession> MiddleclassProfessions { get; private set; } = new();
    public List<CharacterProfession> UpperclassProfessions { get; private set; } = new();

    public CharacterProfessionBuilder(ICharacterProfessionListService professionListService)
    {
        ProfessionListService = professionListService;
        InitializeProfessionLists();
    }

    private void ClearProfessionBonuses(CharacterProfession? newProfession)
    {
        if (newProfession != null && newProfession != SelectedCharacterProfession)
        {
            SelectedProfessionTalent = null;
            SelectedProfessionFocus = null;
        }
    }

    private void InitializeProfessionLists()
    {
        OutsiderProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Outsider));
        LowerclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Lower));
        MiddleclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Middle));
        UpperclassProfessions = new(ProfessionListService.ProfessionList.Where(x => x.ProfessionSocialClass == CharacterSocialClass.Upper));
    }

    public void ClearSelectedProfession(CharacterSocialClass? selectedCharacterSocialClass)
    {
        if (SelectedCharacterProfession != null)
        {
            if (SelectedCharacterProfession.ProfessionSocialClass > selectedCharacterSocialClass)
            {
                SelectedCharacterProfession = null;
            }
        }
    }

    public bool IsMissingProfessionBonus()
    {
        return SelectedProfessionFocus is null || SelectedProfessionTalent is null;
    }

    public void GenerateRandom(CharacterSocialClass? selectedCharacterSocialClass)
    {
        var possibleProfessions = ProfessionListService.ProfessionList.Where(p => p.ProfessionSocialClass <= selectedCharacterSocialClass).ToList();
        SelectedCharacterProfession = possibleProfessions[Random.Shared.Next(0, possibleProfessions.Count)];
        SelectedProfessionFocus = SelectedCharacterProfession.FocusChoices[Random.Shared.Next(0, SelectedCharacterProfession.FocusChoices.Count)];
        SelectedProfessionTalent = SelectedCharacterProfession.TalentChoices[Random.Shared.Next(0, SelectedCharacterProfession.TalentChoices.Count)];
    }
}

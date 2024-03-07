using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Model.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders;

public class CharacterSocialAndBackgroundBuilder : ICharacterSocialAndBackgroundBuilder
{
    private Dictionary<CharacterSocialClass, string?> _socialClassDescriptions = new();

    public string? SelectedCharacterSocialClassDescription
    {
        get => SelectedCharacterSocialClass is null ? null : _socialClassDescriptions[(CharacterSocialClass)SelectedCharacterSocialClass];
    }
    private CharacterSocialClass? _selectedCharacterSocialClass;
    public CharacterSocialClass? SelectedCharacterSocialClass
    {
        get { return _selectedCharacterSocialClass; }
        set
        {
            _selectedCharacterSocialClass = value;
            SocialClassChanged?.Invoke(this, SelectedCharacterSocialClass);
        }
    }
    public List<SocialClassWrapper> SocialClassesWithDescription { get; set; } = new();

    public event EventHandler<CharacterSocialClass?>? SocialClassChanged;
    public event EventHandler<string>? BonusSelectionChanged;

    private CharacterBackGround? _selectedCharacterBackground;
    public CharacterBackGround? SelectedCharacterBackground
    {
        get { return _selectedCharacterBackground; }
        set
        {
            _selectedCharacterBackground = value;
            BonusSelectionChanged?.Invoke(this, nameof(SelectedCharacterBackground));
        }
    }
    private ICharacterCreationBonus? _selectedBackgroundBenefit;
    public ICharacterCreationBonus? SelectedBackgroundBenefit
    {
        get { return _selectedBackgroundBenefit; }
        set
        {
            _selectedBackgroundBenefit = value;
            BonusSelectionChanged?.Invoke(this, nameof(SelectedBackgroundBenefit));
        }
    }
    public CharacterTalent? _selectedBackgroundTalent;
    public CharacterTalent? SelectedBackgroundTalent
    {
        get => _selectedBackgroundTalent;
        set
        {
            _selectedBackgroundTalent = value;
            BonusSelectionChanged?.Invoke(this, nameof(SelectedBackgroundTalent));
        }
    }
    public AbilityFocus? _selectedBackgroundFocus;
    public AbilityFocus? SelectedBackgroundFocus
    {
        get => _selectedBackgroundFocus;
        set
        {
            _selectedBackgroundFocus = value;
            BonusSelectionChanged?.Invoke(this, nameof(SelectedBackgroundFocus));
        }
    }
    private ICharacterBackgroundListService CharacterBackgroundListService { get; set; }
    private IRandomGenerator RandomGenerator { get; set; }
    public CharacterSocialAndBackgroundBuilder(ICharacterBackgroundListService characterBackgroundListService, IRandomGenerator randomGenerator)
    {
        CharacterBackgroundListService = characterBackgroundListService;
        RandomGenerator = randomGenerator;
        InitiliazeSocialClassDescriptions();
        InitiliazeSocialClassWrapperList();
    }
    private void InitiliazeSocialClassDescriptions()
    {
        _socialClassDescriptions = new()
        {
            { CharacterSocialClass.Outsider,ModelResources.DescriptionOutsiderClass},
            { CharacterSocialClass.Lower,ModelResources.DescriptionLowerClass},
            { CharacterSocialClass.Middle,ModelResources.DescriptionMiddleClass},
            { CharacterSocialClass.Upper,ModelResources.DescriptionUpperClass},
        };
    }
    private void InitiliazeSocialClassWrapperList()
    {
        SocialClassesWithDescription = new()
        {
            new(CharacterSocialClass.Outsider, ModelResources.DescriptionOutsiderClass),
            new(CharacterSocialClass.Lower, ModelResources.DescriptionLowerClass),
            new(CharacterSocialClass.Middle, ModelResources.DescriptionMiddleClass),
            new(CharacterSocialClass.Upper, ModelResources.DescriptionUpperClass),
        };
    }
    public bool IsMissingBackgroundBonus()
    {
        return SelectedBackgroundBenefit is null || SelectedBackgroundFocus is null || SelectedBackgroundTalent is null;
    }

    public void GenerateRandom()
    {
        SelectedCharacterSocialClass = (CharacterSocialClass)RandomGenerator.GetRandomInteger(0, 4);
        var possibleBackgrounds = CharacterBackgroundListService.CharacterBackgroundList.Where(bg => bg.MainSocialClass == SelectedCharacterSocialClass).ToList();
        SelectedCharacterBackground = possibleBackgrounds[RandomGenerator.GetRandomInteger(0, possibleBackgrounds.Count)];
        SelectedBackgroundFocus = SelectedCharacterBackground!.PossibleAbilityFocuses[RandomGenerator.GetRandomInteger(0, SelectedCharacterBackground.PossibleAbilityFocuses.Count)];
        SelectedBackgroundTalent = SelectedCharacterBackground.PossiblePlayerTalents[RandomGenerator.GetRandomInteger(0, SelectedCharacterBackground.PossiblePlayerTalents.Count)];
        SelectedBackgroundBenefit = SelectedCharacterBackground.BackgroundBenefits[RandomGenerator.GetRandomInteger(0, SelectedCharacterBackground.BackgroundBenefits.Count)];
    }

    public CharacterAbility? GetAbilityBonus()
    {
        return SelectedCharacterBackground?.AbilityBonus;
    }
}

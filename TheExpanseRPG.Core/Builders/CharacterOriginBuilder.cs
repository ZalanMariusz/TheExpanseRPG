using TheExpanseRPG.Core.Builders.Interfaces;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Builders;

public class CharacterOriginBuilder : ICharacterOriginBuilder
{

    private Dictionary<CharacterOrigin, string?> _originDescriptions = new();
    private IAbilityFocusListService FocusListService { get; }
    public event EventHandler<string>? OriginChanged;
    private CharacterOrigin? _selectedCharacterOrigin;
    public CharacterOrigin? SelectedCharacterOrigin
    {
        get => _selectedCharacterOrigin;
        set
        {
            _selectedCharacterOrigin = value;
            OriginChanged?.Invoke(this, nameof(SelectedCharacterOrigin));
        }
    }
    public string? CurrentOriginDescription
    {
        get { return SelectedCharacterOrigin is null ? null : _originDescriptions[(CharacterOrigin)SelectedCharacterOrigin]; }
    }

    public CharacterOriginBuilder(IAbilityFocusListService focusListService)
    {
        FocusListService = focusListService;
        InitializeOriginDescriptions();
    }

    public AbilityFocus? GetOriginBonus()
    {
        if (SelectedCharacterOrigin == CharacterOrigin.Belt)
        {
            return FocusListService.GetFocusByName(CharacterAbilityName.Dexterity, "Free-fall");
        }
        return null;
    }
    private void InitializeOriginDescriptions()
    {
        _originDescriptions = new()
        {
            { CharacterOrigin.Earth,ModelResources.DescriptionOriginEarth },
            { CharacterOrigin.Mars,ModelResources.DescriptionOriginMars },
            { CharacterOrigin.Belt,ModelResources.DescriptionOriginBelt }
        };
    }

    public void GenerateRandom()
    {
        SelectedCharacterOrigin = (CharacterOrigin)Random.Shared.Next(0, 3);
    }
}

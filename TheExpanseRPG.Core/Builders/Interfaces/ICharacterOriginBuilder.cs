using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Builders.Interfaces;

public interface ICharacterOriginBuilder
{
    string? CurrentOriginDescription { get; }
    CharacterOrigin? SelectedCharacterOrigin { get; set; }

    event EventHandler<string>? OriginChanged;

    void GenerateRandom();
    AbilityFocus? GetOriginBonus();
}
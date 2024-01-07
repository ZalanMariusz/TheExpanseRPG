using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface IAbilityFocusListService
    {
        List<AbilityFocus> FocusList { get; }

        List<AbilityFocus> GetAbilityFocusList(CharacterAbilityName abilityName);
        AbilityFocus GetFocusByName(CharacterAbilityName abilityName, string focusName);
    }
}
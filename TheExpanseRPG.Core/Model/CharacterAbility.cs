﻿using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model;
public class CharacterAbility : ICharacterCreationBonus
{
    public int? BaseValue { get; set; } = 0;
    public int Modifier { get; set; } = 0;
    [JsonIgnore]
    public int? AbilityValue { get => BaseValue + Modifier; }
    public CharacterAbilityName AbilityName { get; set; }
    //[JsonIgnore]
    //public List<AbilityFocus> Focuses { get; } = new();
    [JsonIgnore]
    public string CreationBonusName => $"+1 {AbilityName}";
    public CharacterAbility(CharacterAbilityName abilityName)
    {
        AbilityName = abilityName;
    }
    public CharacterAbility(CharacterAbilityName abilityName, int? baseScore)
    {
        AbilityName = abilityName;
        BaseValue = baseScore;
    }

    public ICharacterCreationBonus ShallowCopy()
    {
        return (CharacterAbility)MemberwiseClone();
    }
}

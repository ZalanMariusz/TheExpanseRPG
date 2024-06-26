﻿using System.Text.Json.Serialization;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Model.Interfaces;

namespace TheExpanseRPG.Core.Model
{
    public class AbilityFocus : ICharacterCreationBonus
    {
        public CharacterAbilityName AbilityName { get; }
        public string FocusName { get; }
        public bool IsImproved { get; private set; } = false;
        [JsonIgnore]
        public string CreationBonusName { get { return $"{AbilityName} ({FocusName})"; } }
        [JsonIgnore]
        public string FocusDescription { get; set; } = string.Empty;
        public AbilityFocus(CharacterAbilityName abilityName, string focusName, string focusDescription)
        {
            AbilityName = abilityName;
            FocusName = focusName;
            FocusDescription = focusDescription;
        }
        public AbilityFocus(CharacterAbilityName abilityName, string focusName)
        {
            AbilityName = abilityName;
            FocusName = focusName;
        }
        public void ImproveFocus()
        {
            IsImproved = true;
        }

        public ICharacterCreationBonus ShallowCopy()
        {
            return (AbilityFocus)MemberwiseClone();
        }
        public override string ToString()
        {
            return $"{AbilityName}({this.FocusName})";
        }
    }
}

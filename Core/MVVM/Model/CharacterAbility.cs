using System;
using System.Collections.Generic;
using System.Linq;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model.Interfaces;

namespace TheExpanseRPG.Core.MVVM.Model
{
    public class CharacterAbility : IBackgroundOrProfessionBenefit
    {
        public int? BaseValue { get; set; }
        public int? Modifier { get; set; } = 0;
        public int? AbilityValue { get => BaseValue + Modifier; }
        public CharacterAbilityName AbilityName { get; set; }
        public List<AbilityFocus> Focuses { get; private set; }

        public string BenefitName
        {
            get
            {
                return $"+1 {AbilityName}";
            }
        }

        public CharacterAbility(CharacterAbilityName abilityName)
        {
            AbilityName = abilityName;
            Focuses = new List<AbilityFocus>();
        }
        public CharacterAbility(CharacterAbilityName abilityName, int? baseScore)
        {
            AbilityName = abilityName;
            BaseValue = baseScore;
            Focuses = new List<AbilityFocus>();
        }
        public void AddFocus(AbilityFocus focus)
        {
            if (focus.AbilityName != AbilityName)
            {
                throw new Exception();
            }
            if (Focuses.FirstOrDefault(x => x.FocusName == focus.FocusName) != null)
            {
                Focuses.First(x => x.FocusName == focus.FocusName).ImproveFocus();
            }
            else
            {
                Focuses.Add(focus);
            }
        }

    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Factories;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AllRandomAbilityRollViewModel : CharacterCreationViewModelBase
    {
        public RelayCommand RollAllRandomAbilityCommand { get; set; }
        public int? Accuracy { get { return GetCharacterAbilityValue(); } }
        public int? Constitution { get { return GetCharacterAbilityValue(); } }
        public int? Fighting { get { return GetCharacterAbilityValue(); } }
        public int? Communication { get { return GetCharacterAbilityValue(); } }
        public int? Dexterity { get { return GetCharacterAbilityValue(); } }
        public int? Intelligence { get { return GetCharacterAbilityValue(); } }
        public int? Perception { get { return GetCharacterAbilityValue(); } }
        public int? Strength { get { return GetCharacterAbilityValue(); } }
        public int? Willpower { get { return GetCharacterAbilityValue(); } }

        public AllRandomAbilityRollViewModel()
        {
            //CharacterCreationService = characterCreationService;
            RollAllRandomAbilityCommand = new RelayCommand(o => true, o => RollAllRandom());
        }
        public void RollAllRandom()
        {
            CharacterCreationService!.RollAllRandom();
            foreach (CharacterAbilityName abilityName in Enum.GetValues<CharacterAbilityName>())
            {
                OnPropertyChanged(abilityName.ToString());
            }
        }
    }
}

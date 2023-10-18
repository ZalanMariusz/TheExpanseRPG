using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterCreationViewModelBase : ViewModelBase, ICharacterCreationViewModel
    {
        public required CharacterCreationService CharacterCreationService { get; set; }

        public void AddCharacterCreationService(CharacterCreationService characterCreationService)
        {
            CharacterCreationService = characterCreationService;
        }

        public int? GetCharacterAbilityValue([CallerMemberName]string abilityName = "")
        {
            return CharacterCreationService.CharacterAbilityBlock.GetAbility(abilityName).BaseValue;
        }
    }
}

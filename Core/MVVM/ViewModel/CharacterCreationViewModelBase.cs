using System;
using System.Linq;
using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterCreationViewModelBase : ViewModelBase, ICharacterCreationViewModel
    {
        private CharacterCreationService _characterCreationService;
        public required CharacterCreationService CharacterCreationService
        {
            get { return _characterCreationService; }
            set { _characterCreationService = value; AssignCharacterCreationServiceToInnerViewModels(); }
        }

        private void AssignCharacterCreationServiceToInnerViewModels()
        {
            foreach (ICharacterCreationViewModel vm in InnerViewModels.Cast<ICharacterCreationViewModel>())
            {
                vm.CharacterCreationService ??= CharacterCreationService;
            }
        }

        public int? GetCharacterAbilityValue([CallerMemberName] string abilityName = "")
        {
            return CharacterCreationService?.CharacterAbilityBlock?.GetAbility(abilityName).BaseValue;
        }
    }
}

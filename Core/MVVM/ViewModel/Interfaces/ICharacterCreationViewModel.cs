using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel.Interfaces
{
    public interface ICharacterCreationViewModel : IViewModelBase
    {
        CharacterCreationService CharacterCreationService { get; }
        public void AddCharacterCreationService(CharacterCreationService characterCreationService);
        public int? GetCharacterAbilityValue([CallerMemberName] string abilityName = "");
        //public void SetCharacterAbilityValue(int? value, [CallerMemberName] string abilityName = "");
    }
}

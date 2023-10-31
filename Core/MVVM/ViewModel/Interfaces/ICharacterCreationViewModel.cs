using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel.Interfaces
{
    public interface ICharacterCreationViewModel : IViewModelBase
    {
        CharacterCreationService CharacterCreationService { get; set; }
        public int? GetCharacterAbilityValue([CallerMemberName] string abilityName = "");
    }
}

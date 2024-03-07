using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.MVVM.ViewModel.Interfaces
{
    public interface ICharacterCreationViewModel : IViewModelBase
    {
        ICharacterCreationService CharacterCreationService { get; set; }
    }
}

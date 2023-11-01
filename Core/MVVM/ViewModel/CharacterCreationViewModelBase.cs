using System.Linq;
using System.Runtime.CompilerServices;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class CharacterCreationViewModelBase : ViewModelBase, ICharacterCreationViewModel
    {
        private CharacterCreationService? _characterCreationService;
        public CharacterCreationService CharacterCreationService
        {
            get { return _characterCreationService!; }
            set { _characterCreationService = value; }
        }

        public int? GetCharacterAbilityValue([CallerMemberName] string abilityName = "")
        {
            return CharacterCreationService?.CharacterAbilityBlock?.GetAbility(abilityName).BaseValue;
        }
        protected void AssignCharacterCreationService()
        {
            if (CurrentInnerViewModel != null && ((ICharacterCreationViewModel)CurrentInnerViewModel).CharacterCreationService == null)
            {
                ((ICharacterCreationViewModel)CurrentInnerViewModel).CharacterCreationService = CharacterCreationService;
            }
        }
        protected void NavigateToInnerView<TViewModel>() where TViewModel : ICharacterCreationViewModel
        {
            TViewModel? viewModelToNavigateTo = (TViewModel?)InnerViewModels.FirstOrDefault(x => x.GetType() == typeof(TViewModel));
            NavigationService.NavigateToInnerView<TViewModel>(this);
            AssignCharacterCreationService();
        }
    }
}

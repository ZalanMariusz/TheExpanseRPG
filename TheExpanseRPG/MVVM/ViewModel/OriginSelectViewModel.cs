using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class OriginSelectViewModel : CharacterCreationViewModelBase
    {
        public string? SelectedOriginDescription
        {
            get { return CharacterCreationService.OriginBuilder.CurrentOriginDescription; }
        }
        public CharacterOrigin? SelectedOrigin
        {
            get { return CharacterCreationService!.OriginBuilder.SelectedCharacterOrigin; }
            set
            {
                CharacterCreationService.OriginBuilder.SelectedCharacterOrigin = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedOriginDescription));
            }
        }
        public OriginSelectViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            CharacterCreationService = scopedServiceFactory.GetScopedService<ICharacterCreationService>();
        }
    }
}

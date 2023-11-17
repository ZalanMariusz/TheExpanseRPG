using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Factories;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class OriginSelectViewModel : CharacterCreationViewModelBase
    {
        public string? SelectedOriginDescription
        {
            get { return CharacterCreationService.CurrentOriginDescription; }
        }
        public CharacterOrigin? SelectedOrigin
        {
            get { return CharacterCreationService!.CharacterOrigin; }
            set
            {
                CharacterCreationService.CharacterOrigin = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedOriginDescription));
            }
        }
       
        public OriginSelectViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        }
    }
}

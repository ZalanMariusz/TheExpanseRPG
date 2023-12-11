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
            get { return CharacterCreationService!.SelectedCharacterOrigin; }
            set
            {
                CharacterCreationService.SelectedCharacterOrigin = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedOriginDescription));
                EventAggregator.PublishLinkedPropertyChanged();
                EventAggregator.PublishLinkedPropertyChanged("HasOriginSelectionConflict");
                EventAggregator.PublishLinkedPropertyChanged("HasSocialOrBackgroundSelectionConflict");
                EventAggregator.PublishLinkedPropertyChanged("HasProfessionSelectionConflict");
                EventAggregator.PublishLinkedPropertyChanged("OriginConflicts");
                EventAggregator.PublishLinkedPropertyChanged("SocialOrBackgroundConflicts");
                EventAggregator.PublishLinkedPropertyChanged("ProfessionConflicts");
            }
        }

        public OriginSelectViewModel(ScopedServiceFactory scopedServiceFactory)
        {
            CharacterCreationService = (CharacterCreationService)scopedServiceFactory.GetScopedService<CharacterCreationService>();
        }
    }
}

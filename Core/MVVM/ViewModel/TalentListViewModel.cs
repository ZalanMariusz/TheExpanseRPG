using System.Collections.ObjectModel;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.Services;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class TalentListViewModel : ViewModelBase
    {
        TalentListService TalentListService { get; }
        public ObservableCollection<CharacterTalent> TalentList { get; }
        public TalentListViewModel(TalentListService talentListService)
        {
            TalentListService = talentListService;
            TalentList = new ObservableCollection<CharacterTalent>(TalentListService.TalentList);
        }
    }
}

using System.Collections.ObjectModel;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class TalentListViewModel : ViewModelBase
    {
        ITalentListService TalentListService { get; }
        public ObservableCollection<CharacterTalent> TalentList { get; }
        public TalentListViewModel(ITalentListService talentListService)
        {
            TalentListService = talentListService;
            TalentList = new ObservableCollection<CharacterTalent>(TalentListService.TalentList);
        }
    }
}

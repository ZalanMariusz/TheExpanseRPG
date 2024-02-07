using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class TalentInfoViewModel : ViewModelBase
    {
        private CharacterTalent? _talent;
        public CharacterTalent? Talent { get => _talent; set { _talent = value; OnPropertyChanged(); } }
        public TalentInfoViewModel(CharacterTalent talent)
        {
            Talent = talent;
        }
    }
}

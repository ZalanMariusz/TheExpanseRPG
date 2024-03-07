using System.Collections.ObjectModel;
using System.Linq;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.MVVM.ViewModel;

public class CharacterDetailsViewModel : ViewModelBase
{
    private ExpanseCharacter? _character = new();
    public ExpanseCharacter Character
    {
        get { return _character!; }
        set { _character = value; OnPropertyChanged(null); }
    }
    public ObservableCollection<AbilityFocus> Focuses => new(Character!.Focuses.OrderBy(x => x.AbilityName).ToList());
}
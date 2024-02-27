using System.Linq;
using System.Windows;

namespace TheExpanseRPG.MVVM.View
{
    public partial class CharacterSheetWindow : Window
    {
        public CharacterSheetWindow()
        {
            Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            InitializeComponent();
        }
    }
}

using System.Linq;
using System.Windows;

namespace TheExpanseRPG.Popups
{
    /// <summary>
    /// Interaction logic for PopupBase.xaml
    /// </summary>
    public partial class PopupBase : Window
    {
        public PopupBase()
        {
            Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (Owner != null)
            {
                Width = Owner.Width;
                Height = Owner.Height;
            }
            InitializeComponent();
        }
    }
}

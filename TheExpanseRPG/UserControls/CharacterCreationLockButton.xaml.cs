using System.Windows;
using System.Windows.Controls;

namespace TheExpanseRPG.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterCreationLockButton.xaml
    /// </summary>
    public partial class CharacterCreationLockButton : UserControl
    {
        public CharacterCreationLockButton()
        {
            InitializeComponent();
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(CharacterCreationLockButton), new PropertyMetadata(false));
    }
}

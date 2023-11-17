using System.Windows.Controls;
using System.Windows.Media;

namespace TheExpanseRPG.MVVM.View
{
    /// <summary>
    /// Interaction logic for SocialAndBackgroundView.xaml
    /// </summary>
    public partial class SocialAndBackgroundView : UserControl
    {
        public SocialAndBackgroundView()
        {
            InitializeComponent();
        }

        private void bg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox)
            {
                if (((ComboBox)sender).SelectedItem == null)
                {
                    ((ComboBox)sender).Background = Brushes.Transparent;
                }
                else
                {
                    //19BC47
                    var brush = new SolidColorBrush(Color.FromArgb(255, 25, 188, 71));
                    ((ComboBox)sender).Background = brush;
                }
            }
        }


    }
}

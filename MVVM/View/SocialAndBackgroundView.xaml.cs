using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheExpanseRPG.MVVM.ViewModel;

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sender is ComboBox box)
            {
                if (box.SelectedItem == null)
                {
                    box.Background = Brushes.Transparent;
                    
                }
                else
                {
                    //19BC47
                    var brush = new SolidColorBrush(Color.FromArgb(255, 25, 188, 71));
                    box.Background = brush;
                }
            }
        }
    }
}

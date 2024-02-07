using System.Windows;
using System.Windows.Input;

namespace TheExpanseRPG.MVVM.View
{
    /// <summary>
    /// Interaction logic for TalentListWindow.xaml
    /// </summary>
    public partial class TalentListWindow : Window
    {
        public TalentListWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}

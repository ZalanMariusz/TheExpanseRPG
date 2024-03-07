using System.Windows;
using System.Windows.Input;

namespace TheExpanseRPG.MVVM.View
{
    /// <summary>
    /// Interaction logic for FocusListWindow.xaml
    /// </summary>
    public partial class FocusListWindow : Window
    {
        public FocusListWindow()
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

using System.Windows;
using System.Windows.Input;

namespace TheExpanseRPG.Core.MVVM.View
{
    public partial class WelcomeSplashWindow : Window
    {
        public WelcomeSplashWindow()
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

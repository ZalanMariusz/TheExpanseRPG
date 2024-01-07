using System.Windows;

namespace TheExpanseRPG.MVVM.View
{
    public partial class PlayerMainWindow : Window
    {
        public PlayerMainWindow()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

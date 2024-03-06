using System.Windows;
using System.Windows.Input;

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
            //if (!App.IsNavigating)
            //{
            //    Application.Current.Shutdown();
            //}
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WIP");
        }
    }
}

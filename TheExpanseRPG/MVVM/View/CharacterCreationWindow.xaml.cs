using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace TheExpanseRPG.MVVM.View
{
    /// <summary>
    /// Interaction logic for CharacterCreationWindow.xaml
    /// </summary>
    public partial class CharacterCreationWindow : Window
    {
        public CharacterCreationWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!App.IsNavigating)
            {
                Application.Current.Shutdown();
            }
        }
    }
}

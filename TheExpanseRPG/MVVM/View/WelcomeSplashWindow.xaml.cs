﻿using System.Windows;
using System.Windows.Input;

namespace TheExpanseRPG.MVVM.View
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WIP");
        }
    }
}

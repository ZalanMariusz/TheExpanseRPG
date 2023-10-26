using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheExpanseRPG.UserControls;

namespace TheExpanseRPG.Core.MVVM.View
{
    public partial class CharacterProfessionView : UserControl
    {
        public CharacterProfessionView()
        {
            InitializeComponent();
        }
        private void ProfessionSelectorList_GotFocus(object sender, RoutedEventArgs e)
        {
            ProfessionSelectorList senderSelector = (sender as ProfessionSelectorList)!;
            DependencyObject parent = senderSelector.Parent;
            List<Control> selectors = parent.FindControlsOfType<ProfessionSelectorList>();
            foreach (Control selector in selectors)
            {
                var t = selector.FindControlsOfType<ListBox>().FirstOrDefault();
                if (t.IsEnabled)
                {
                    (t as ListBox)!.UnselectAll();
                }
            }
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
                    var brush = new SolidColorBrush(Color.FromArgb(255, 25, 188, 71));
                    ((ComboBox)sender).Background = brush;
                }
            }
        }
    }

    public static class Extensions
    {
        public static List<Control> FindControlsOfType<T>(this DependencyObject parent) where T : Control
        {
            var controls = new List<Control>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T control)
                {
                    controls.Add(control);
                }
                controls.AddRange(FindControlsOfType<T>(child));
            }

            return controls;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TheExpanseRPG.MVVM.View
{
    /// <summary>
    /// Interaction logic for AssignAttributeRollView.xaml
    /// </summary>
    public partial class AssignAttributeRollView : UserControl
    {
        private readonly List<Label> dropAcceptingLabels;
        public AssignAttributeRollView()
        {
            InitializeComponent();
            dropAcceptingLabels = new List<Label>();

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock attributeValue = (TextBlock)sender;
            foreach (Label label in dropAcceptingLabels)
            {
                if (label.Content == null)
                {
                    label.Background = new SolidColorBrush(Colors.LightGreen);
                }
            }
            DragDrop.DoDragDrop(attributeValue, ((TextBlock)sender).Text, DragDropEffects.Link);
            foreach (Label label in dropAcceptingLabels)
            {
                label.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void Label_Drop(object sender, DragEventArgs e)
        {
            Label target = (Label)sender;
            target.Content = e.Data.GetData(DataFormats.StringFormat);
        }
        public static T? FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Label label in FindVisualChildren<Label>((sender as DependencyObject)!))
            {
                if (label.Tag?.ToString() == "attributeLabel")
                {
                    dropAcceptingLabels.Add(label);
                }
            }
        }
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheExpanseRPG.Core.MVVM.View
{
    /// <summary>
    /// Interaction logic for AssignAttributeRollView.xaml
    /// </summary>
    public partial class AssignAttributeRollView : UserControl
    {
        public AssignAttributeRollView()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock attributeValue = (TextBlock)sender;
            ListBox parent = FindParent<ListBox>(attributeValue);
            DragDrop.DoDragDrop(attributeValue, ((TextBlock)sender).Text, DragDropEffects.Link);
        }

        private void Label_Drop(object sender, DragEventArgs e)
        {
            Label target = (Label)sender;
            target.Content = e.Data.GetData(DataFormats.StringFormat);
        }
        public static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
    }
}

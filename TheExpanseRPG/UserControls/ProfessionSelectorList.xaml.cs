using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.UserControls
{
    /// <summary>
    /// Interaction logic for ProfessionSelectorList.xaml
    /// </summary>
    public partial class ProfessionSelectorList : UserControl
    {
        public ProfessionSelectorList()
        {
            InitializeComponent();
        }


        public ObservableCollection<CharacterProfession> ItemsSource
        {
            get { return (ObservableCollection<CharacterProfession>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(ObservableCollection<CharacterProfession>), typeof(ProfessionSelectorList), new PropertyMetadata(null));



        public CharacterProfession SelectedItem
        {
            get { return (CharacterProfession)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(CharacterProfession), typeof(ProfessionSelectorList), new PropertyMetadata(null));



        public string ProfessionName
        {
            get { return (string)GetValue(ProfessionNameProperty); }
            set { SetValue(ProfessionNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProfessionNameProperty =
            DependencyProperty.Register(nameof(ProfessionName), typeof(string), typeof(ProfessionSelectorList), new PropertyMetadata(string.Empty));
    }
}

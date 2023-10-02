using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.CustomControls;

namespace TheExpanseRPG.UserControls
{
    public partial class ButtonWithCutBorder : UserControl
    {
        public ButtonWithCutBorder()
        {
            Button t = new Button();
            
            InitializeComponent();
        }
        static ButtonWithCutBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonWithCutBorder), new FrameworkPropertyMetadata(typeof(ButtonWithCutBorder)));
        }
        public static readonly DependencyProperty BorderCutsProperty = DependencyProperty.Register(
            nameof(BorderCuts),
            typeof(string),
            typeof(ButtonWithCutBorder),
            new PropertyMetadata(string.Empty));

        public string BorderCuts
        {
            get { return (string)GetValue(BorderCutsProperty); }
            set { SetValue(BorderCutsProperty, value); }
        }
    }
}

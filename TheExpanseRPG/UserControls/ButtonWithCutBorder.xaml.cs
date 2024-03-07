using System.Windows;
using System.Windows.Controls;

namespace TheExpanseRPG.UserControls
{
    public partial class ButtonWithCutBorder : UserControl
    {
        public ButtonWithCutBorder()
        {
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


        public string HasShadow
        {
            get { return (string)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasShadow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasShadowProperty =
            DependencyProperty.Register("HasShadow", typeof(string), typeof(ButtonWithCutBorder), new PropertyMetadata(string.Empty));


    }
}

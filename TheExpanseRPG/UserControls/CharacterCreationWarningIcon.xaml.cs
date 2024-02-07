using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TheExpanseRPG.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterCreationWarningIcon.xaml
    /// </summary>
    public partial class CharacterCreationWarningIcon : UserControl
    {
        public CharacterCreationWarningIcon()
        {
            InitializeComponent();
        }
        public string TooltipContent
        {
            get { return (string)GetValue(TooltipContentProperty); }
            set { SetValue(TooltipContentProperty, value); }
        }

        public static readonly DependencyProperty TooltipContentProperty =
            DependencyProperty.Register(nameof(TooltipContent), typeof(string), typeof(CharacterCreationWarningIcon), new PropertyMetadata(string.Empty));

        public Visibility WarningVisibility
        {
            get { return (Visibility)GetValue(WarningVisibilityProperty); }
            set { SetValue(WarningVisibilityProperty, value); }
        }

        public static readonly DependencyProperty WarningVisibilityProperty =
            DependencyProperty.Register(nameof(WarningVisibility), typeof(Visibility), typeof(CharacterCreationWarningIcon), new PropertyMetadata(Visibility.Visible));

        public int InitialShowDelay
        {
            get { return (int)GetValue(InitialShowDelayProperty); }
            set { SetValue(InitialShowDelayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialShowDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialShowDelayProperty =
            DependencyProperty.Register(nameof(InitialShowDelay), typeof(int), typeof(CharacterCreationWarningIcon), new PropertyMetadata(0));

        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(CharacterCreationWarningIcon), new PropertyMetadata(PlacementMode.Top));

        public int HorizontalOffset
        {
            get { return (int)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register(nameof(HorizontalOffset), typeof(int), typeof(CharacterCreationWarningIcon), new PropertyMetadata(0));

        public int VerticalOffset
        {
            get { return (int)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register(nameof(VerticalOffset), typeof(int), typeof(CharacterCreationWarningIcon), new PropertyMetadata(0));

        public new HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalAlignment.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(CharacterCreationWarningIcon), new PropertyMetadata(HorizontalAlignment.Left));

    }
}

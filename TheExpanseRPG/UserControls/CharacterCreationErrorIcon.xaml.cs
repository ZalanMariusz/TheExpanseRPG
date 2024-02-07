using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TheExpanseRPG.UserControls;

/// <summary>
/// Interaction logic for CharacterCreationErrorIcon.xaml
/// </summary>
public partial class CharacterCreationErrorIcon : UserControl
{
    public CharacterCreationErrorIcon()
    {
        InitializeComponent();
    }
    public string TooltipContent
    {
        get { return (string)GetValue(TooltipContentProperty); }
        set { SetValue(TooltipContentProperty, value); }
    }

    public static readonly DependencyProperty TooltipContentProperty =
        DependencyProperty.Register(nameof(TooltipContent), typeof(string), typeof(CharacterCreationErrorIcon), new PropertyMetadata(string.Empty));

    public Visibility ErrorVisibility
    {
        get { return (Visibility)GetValue(ErrorVisibilityProperty); }
        set { SetValue(ErrorVisibilityProperty, value); }
    }

    public static readonly DependencyProperty ErrorVisibilityProperty =
        DependencyProperty.Register(nameof(ErrorVisibility), typeof(Visibility), typeof(CharacterCreationErrorIcon), new PropertyMetadata(Visibility.Visible));

    public int InitialShowDelay
    {
        get { return (int)GetValue(InitialShowDelayProperty); }
        set { SetValue(InitialShowDelayProperty, value); }
    }

    // Using a DependencyProperty as the backing store for InitialShowDelay.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty InitialShowDelayProperty =
        DependencyProperty.Register(nameof(InitialShowDelay), typeof(int), typeof(CharacterCreationErrorIcon), new PropertyMetadata(0));

    public PlacementMode Placement
    {
        get { return (PlacementMode)GetValue(PlacementProperty); }
        set { SetValue(PlacementProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Placement.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(CharacterCreationErrorIcon), new PropertyMetadata(PlacementMode.Top));

    public int HorizontalOffset
    {
        get { return (int)GetValue(HorizontalOffsetProperty); }
        set { SetValue(HorizontalOffsetProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HorizontalOffsetProperty =
        DependencyProperty.Register(nameof(HorizontalOffset), typeof(int), typeof(CharacterCreationErrorIcon), new PropertyMetadata(0));

    public int VerticalOffset
    {
        get { return (int)GetValue(VerticalOffsetProperty); }
        set { SetValue(VerticalOffsetProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HorizontalOffset.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty VerticalOffsetProperty =
        DependencyProperty.Register(nameof(VerticalOffset), typeof(int), typeof(CharacterCreationErrorIcon), new PropertyMetadata(0));

    public new HorizontalAlignment HorizontalAlignment
    {
        get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
        set { SetValue(HorizontalAlignmentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HorizontalAlignment.  This enables animation, styling, binding, etc...
    public static new readonly DependencyProperty HorizontalAlignmentProperty =
        DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(CharacterCreationErrorIcon), new PropertyMetadata(HorizontalAlignment.Left));

}

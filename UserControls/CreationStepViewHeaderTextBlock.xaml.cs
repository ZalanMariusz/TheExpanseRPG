using System.Windows;
using System.Windows.Controls;

namespace TheExpanseRPG.UserControls;

public partial class CreationStepViewHeaderTextBlock : UserControl
{
    public CreationStepViewHeaderTextBlock()
    {
        InitializeComponent();
    }
    

    public string HeaderText
    {
        get { return (string)GetValue(HeaderTextProperty); }
        set { SetValue(HeaderTextProperty, value); }
    }

    public static readonly DependencyProperty HeaderTextProperty =
        DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(CreationStepViewHeaderTextBlock), new PropertyMetadata(string.Empty));


    public new HorizontalAlignment HorizontalAlignment
    {
        get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
        set { SetValue(HorizontalAlignmentProperty, value); }
    }
    public static new readonly DependencyProperty HorizontalAlignmentProperty =
        DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(CreationStepViewHeaderTextBlock), new PropertyMetadata(HorizontalAlignment.Stretch));

    public new HorizontalAlignment HorizontalContentAlignment
    {
        get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
        set { SetValue(HorizontalContentAlignmentProperty, value); }
    }

    public static new readonly DependencyProperty HorizontalContentAlignmentProperty =
        DependencyProperty.Register(nameof(HorizontalContentAlignment), typeof(HorizontalAlignment), typeof(CreationStepViewHeaderTextBlock), new PropertyMetadata(HorizontalAlignment.Left));


}

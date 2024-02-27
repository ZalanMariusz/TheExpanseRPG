using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Commands;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.UserControls;

/// <summary>
/// Interaction logic for CharacterAsListItem.xaml
/// </summary>
public partial class CharacterAsListItem : UserControl
{
    public CharacterAsListItem()
    {
        InitializeComponent();
    }


    public ExpanseCharacter Character
    {
        get { return (ExpanseCharacter)GetValue(CharacterProperty); }
        set { SetValue(CharacterProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CharacterProperty =
        DependencyProperty.Register(nameof(Character), typeof(ExpanseCharacter), typeof(CharacterAsListItem), new PropertyMetadata(new ExpanseCharacter() { Origin = Core.Enums.CharacterOrigin.Earth }));



    public RelayCommand DeleteCharacterCommand
    {
        get { return (RelayCommand)GetValue(DeleteCharacterCommandProperty); }
        set { SetValue(DeleteCharacterCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DeleteCharacterCommandProperty =
        DependencyProperty.Register(nameof(DeleteCharacterCommand), typeof(RelayCommand), typeof(CharacterAsListItem), new PropertyMetadata(null));

    public RelayCommand OpenCharacterSheetCommand
    {
        get { return (RelayCommand)GetValue(OpenCharacterSheetCommandProperty); }
        set { SetValue(OpenCharacterSheetCommandProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OpenCharacterSheetCommandProperty =
        DependencyProperty.Register(nameof(OpenCharacterSheetCommand), typeof(RelayCommand), typeof(CharacterAsListItem), new PropertyMetadata(null));

    //private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    //{
    //    backgroundBorder.Background.Opacity = 0.8;
    //}

    //private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    //{
    //    backgroundBorder.Background.Opacity = 0.5;
    //}


    public double BackgroundOpacity
    {
        get { return (double)GetValue(BackgroundOpacityProperty); }
        set { SetValue(BackgroundOpacityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BackgroundOpacity.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BackgroundOpacityProperty =
        DependencyProperty.Register(nameof(BackgroundOpacity), typeof(double), typeof(CharacterAsListItem), new PropertyMetadata(0.5));


}

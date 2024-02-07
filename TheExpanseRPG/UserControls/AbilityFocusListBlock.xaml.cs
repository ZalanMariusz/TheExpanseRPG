using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.UserControls;

/// <summary>
/// Interaction logic for AbilityFocusListBlock.xaml
/// </summary>
public partial class AbilityFocusListBlock : UserControl
{
    public AbilityFocusListBlock()
    {
        InitializeComponent();
    }




    public string FocusListName
    {
        get { return (string)GetValue(FocusListNameProperty); }
        set { SetValue(FocusListNameProperty, value); }
    }

    // Using a DependencyProperty as the backing store for FocusListName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FocusListNameProperty =
        DependencyProperty.Register(nameof(FocusListName), typeof(string), typeof(AbilityFocusListBlock), new PropertyMetadata(string.Empty));


    public ObservableCollection<AbilityFocus> FocusList
    {
        get { return (ObservableCollection<AbilityFocus>)GetValue(FocusListProperty); }
        set { SetValue(FocusListProperty, value); }
    }
    
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FocusListProperty =
        DependencyProperty.Register(nameof(FocusList), typeof(ObservableCollection<AbilityFocus>), typeof(AbilityFocusListBlock), new PropertyMetadata(new ObservableCollection<AbilityFocus>()));



}

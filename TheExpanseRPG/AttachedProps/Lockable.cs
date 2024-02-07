using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using TheExpanseRPG.Commands;
/*idea was there, but it just does not work - may revisit later*/
namespace TheExpanseRPG.AttachedProps;
public class Lockable : DependencyObject
{
    private static Dictionary<string, HashSet<UIElement>> _lockableCollection = new();

    public static bool GetIsLockable(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsLockableProperty);
    }
    public static void SetIsLockable(DependencyObject obj, bool value)
    {
        obj.SetValue(IsLockableProperty, value);
    }
    public static readonly DependencyProperty IsLockableProperty =
        DependencyProperty.RegisterAttached("IsLockable", typeof(bool), typeof(Lockable), new PropertyMetadata(false, SyncLockableCollection));

    private static void SyncLockableCollection(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d != null && (bool)e.NewValue && d is UIElement uIElement)
        {
            if (!_lockableCollection.TryAdd(GetLockTag(d), new() { uIElement }))
            {
                _lockableCollection[GetLockTag(d)].Add(uIElement);
            }
        }
        else if (d != null && !(bool)e.NewValue && d is UIElement uiElem)
        {
            _lockableCollection.TryGetValue(GetLockTag(d), out HashSet<UIElement>? list);
            if (list is not null)
            {
                list.Remove(uiElem);
            }
        }
    }

    public static string GetLockTag(DependencyObject obj)
    {
        return (string)obj.GetValue(LockTagProperty);
    }

    public static void SetLockTag(DependencyObject obj, string value)
    {
        obj.SetValue(LockTagProperty, value);
    }

    public static readonly DependencyProperty LockTagProperty =
        DependencyProperty.RegisterAttached("LockTag", typeof(string), typeof(Lockable), new PropertyMetadata(string.Empty));

    public static bool GetIsLockerControl(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsLockerControlProperty);
    }

    public static void SetIsLockerControl(DependencyObject obj, bool value)
    {
        obj.SetValue(IsLockerControlProperty, value);
    }
    public static readonly DependencyProperty IsLockerControlProperty =
        DependencyProperty.RegisterAttached("IsLockerControl", typeof(bool), typeof(Lockable), new PropertyMetadata(false, OnLockerControlAssigned));

    private static void OnLockerControlAssigned(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ButtonBase button)
        {
            button.Command = new RelayCommand(o => true, ToggleLockOnTaggedElements);
        }
    }

    private static void ToggleLockOnTaggedElements(object param)
    {
        foreach (UIElement uiElement in _lockableCollection[param.ToString()!])
        {
            uiElement.IsEnabled = !uiElement.IsEnabled;
        }
    }
}

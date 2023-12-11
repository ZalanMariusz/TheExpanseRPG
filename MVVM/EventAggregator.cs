using System;
using System.Runtime.CompilerServices;

namespace TheExpanseRPG.MVVM;

public static class EventAggregator
{
    public static event EventHandler<string>? LinkedPropertyChanged;
    public static void PublishLinkedPropertyChanged([CallerMemberName] string propertyName = "", object? sender = null)
    {
        LinkedPropertyChanged?.Invoke(sender, propertyName);
    }
    public static void UnSubscribeAll()
    {
        LinkedPropertyChanged = null;
    }

}

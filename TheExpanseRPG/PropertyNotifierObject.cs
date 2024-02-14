using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheExpanseRPG.MVVM;

namespace TheExpanseRPG
{
    public class PropertyNotifierObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            var t = PropertyChanged?.GetInvocationList();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void UnsubscribeAll()
        {
            PropertyChanged = null;
        }
    }
}

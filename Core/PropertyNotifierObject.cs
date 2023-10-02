﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheExpanseRPG.Core
{
    public class PropertyNotifierObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //protected void OnPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if(!EqualityComparer<T>.Default.Equals(field, value))
        //    {
        //        field = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

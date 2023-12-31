﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileAnalizer.ViewModels;
public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void PropertyChange<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}


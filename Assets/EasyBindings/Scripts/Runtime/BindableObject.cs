﻿using System.Runtime.CompilerServices;

namespace AillieoUtils.EasyBindings
{
    public abstract class BindableObject
    {
        public readonly Event<string> onPropertyChanged = new Event<string>();

        protected void RaiseNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            onPropertyChanged.Invoke(propertyName);
        }
    }
}

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AillieoUtils.EasyBindings
{
    public abstract class BindableObject
    {
        internal readonly Event<string> onPropertyChanged = new Event<string>();

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            onPropertyChanged.Invoke(propertyName);
        }

        protected bool SetStructValue<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "") where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                return false;
            }

            currentValue = newValue;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        protected bool SetClassValue<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "") where T : class
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
            {
                return false;
            }

            currentValue = newValue;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
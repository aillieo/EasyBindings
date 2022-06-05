using System.Collections.Generic;

namespace AillieoUtils.EasyBindings
{
    public partial class BindableProperty<T>
    {
        public readonly Event<PropertyChangedEventArg<T>> onValueChanged = new Event<PropertyChangedEventArg<T>>();

        private T value;

        public BindableProperty(T initialValue)
        {
            value = initialValue;
        }

        public void Next(T nextValue)
        {
            if (EqualityComparer<T>.Default.Equals(value, nextValue))
            {
                return;
            }
            
            T oldValue = value;
            value = nextValue;
            onValueChanged.Invoke(new PropertyChangedEventArg<T>(oldValue, nextValue));
        }
    }
}

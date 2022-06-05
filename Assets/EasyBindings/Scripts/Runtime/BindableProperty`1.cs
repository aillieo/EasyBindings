using System.Collections.Generic;

namespace AillieoUtils.EasyBindings
{
    public class BindableProperty<T>
    {
        public readonly Event<PropertyChangedEventArg<T>> onValueChanged = new Event<PropertyChangedEventArg<T>>();
        private readonly EqualityComparer<T> equalityComparer;
        private T value;

        public BindableProperty(T initialValue)
        {
            value = initialValue;
            this.equalityComparer = EqualityComparer<T>.Default;
        }

        public BindableProperty(T initialValue, EqualityComparer<T> equalityComparer)
        {
            value = initialValue;
            this.equalityComparer = equalityComparer;
        }

        public void Next(T nextValue)
        {
            if (equalityComparer.Equals(value, nextValue))
            {
                return;
            }

            T oldValue = value;
            value = nextValue;
            onValueChanged.Invoke(new PropertyChangedEventArg<T>(oldValue, nextValue));
        }
    }
}

using System.Collections.Generic;

namespace AillieoUtils.EasyBindings
{
    public class BindableProperty<T>
    {
        internal readonly Event<PropertyChangedEventArg<T>> onValueChanged = new Event<PropertyChangedEventArg<T>>();
        private readonly IEqualityComparer<T> equalityComparer;
        private T value;

        public T CurrentValue => value;

        public BindableProperty(T initialValue)
        {
            value = initialValue;
            this.equalityComparer = EqualityComparer<T>.Default;
        }

        public BindableProperty(T initialValue, IEqualityComparer<T> equalityComparer)
        {
            value = initialValue;
            this.equalityComparer = equalityComparer;
        }

        public bool Next(T nextValue)
        {
            if (equalityComparer.Equals(value, nextValue))
            {
                return false;
            }

            T oldValue = value;
            value = nextValue;
            onValueChanged.Invoke(new PropertyChangedEventArg<T>(oldValue, nextValue));
            return true;
        }
    }
}

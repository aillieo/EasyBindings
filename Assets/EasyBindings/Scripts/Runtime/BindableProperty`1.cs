namespace AillieoUtils.EasyBindings
{
    using System.Collections.Generic;

    public class BindableProperty<T>
    {
        internal readonly Event<PropertyChangedEventArg<T>> onValueChanged = new Event<PropertyChangedEventArg<T>>();
        private readonly IEqualityComparer<T> equalityComparer;
        private T value;

        public T CurrentValue => this.value;

        public BindableProperty(T initialValue)
        {
            this.value = initialValue;
            this.equalityComparer = EqualityComparer<T>.Default;
        }

        public BindableProperty(T initialValue, IEqualityComparer<T> equalityComparer)
        {
            this.value = initialValue;
            this.equalityComparer = equalityComparer;
        }

        public bool Next(T nextValue)
        {
            if (this.equalityComparer.Equals(this.value, nextValue))
            {
                return false;
            }

            T oldValue = this.value;
            this.value = nextValue;
            this.onValueChanged.Invoke(new PropertyChangedEventArg<T>(oldValue, nextValue));
            return true;
        }
    }
}

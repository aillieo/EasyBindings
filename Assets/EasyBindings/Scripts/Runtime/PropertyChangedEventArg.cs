namespace AillieoUtils.EasyBindings
{
    public struct PropertyChangedEventArg<T>
    {
        public readonly T oldValue;
        public readonly T nextValue;

        public PropertyChangedEventArg(T oldValue, T nextValue)
        {
            this.oldValue = oldValue;
            this.nextValue = nextValue;
        }
    }
}
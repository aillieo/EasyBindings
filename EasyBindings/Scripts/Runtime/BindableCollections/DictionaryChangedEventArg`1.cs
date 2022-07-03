namespace AillieoUtils.EasyBindings.Collections
{
    public readonly struct DictionaryChangedEventArg<T>
    {
        public readonly T key;
        public readonly EventType type;
        public DictionaryChangedEventArg(T key, EventType type)
        {
            this.key = key;
            this.type = type;
        }
    }
}

namespace AillieoUtils.EasyBindings.Collections
{
    public readonly struct ListChangedEventArg
    {
        public readonly int index;
        public readonly EventType type;

        public ListChangedEventArg(int index, EventType type)
        {
            this.index = index;
            this.type = type;
        }
    }
}

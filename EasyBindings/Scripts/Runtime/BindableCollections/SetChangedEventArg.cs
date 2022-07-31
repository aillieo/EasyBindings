namespace AillieoUtils.EasyBindings.Collections
{
    public readonly struct SetChangedEventArg
    {
        public readonly EventType type;

        public SetChangedEventArg(EventType type)
        {
            this.type = type;
        }
    }
}

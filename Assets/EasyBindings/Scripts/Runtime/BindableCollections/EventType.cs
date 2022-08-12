namespace AillieoUtils.EasyBindings.Collections
{
    using System;

    [Flags]
    public enum EventType : byte
    {
        Add = 1,
        Remove = 1 << 1,
        Update = 1 << 2,
        Clear = 1 << 3,
    }
}

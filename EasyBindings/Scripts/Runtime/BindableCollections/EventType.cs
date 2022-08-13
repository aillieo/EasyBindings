// -----------------------------------------------------------------------
// <copyright file="EventType.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    using System;

    /// <summary>
    /// Type of a collection changed event.
    /// </summary>
    [Flags]
    public enum EventType : byte
    {
        /// <summary>
        /// Indicates that a new element is added to the collection.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Indicates that an element is removed from the collection.
        /// </summary>
        Remove = 1 << 1,

        /// <summary>
        /// Indicates that an element is updated in the collection.
        /// </summary>
        Update = 1 << 2,

        /// <summary>
        /// Indicates that a collection is cleared.
        /// </summary>
        Clear = 1 << 3,
    }
}

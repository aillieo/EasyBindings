// -----------------------------------------------------------------------
// <copyright file="ListChangedEventArg.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    /// <summary>
    /// List changed event argument.
    /// </summary>
    public readonly struct ListChangedEventArg
    {
        /// <summary>
        /// Corresponding index of the change event.
        /// </summary>
        public readonly int index;

        /// <summary>
        /// Type of the change event.
        /// </summary>
        public readonly EventType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListChangedEventArg"/> struct.
        /// </summary>
        /// <param name="index">Corresponding index of the change event.</param>
        /// <param name="type">Type of the change event.</param>
        public ListChangedEventArg(int index, EventType type)
        {
            this.index = index;
            this.type = type;
        }
    }
}

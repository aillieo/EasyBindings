// -----------------------------------------------------------------------
// <copyright file="SetChangedEventArg`1.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    /// <summary>
    /// Set changed event argument.
    /// </summary>
    /// <typeparam name="T">Element type of the set.</typeparam>
    public readonly struct SetChangedEventArg<T>
    {
        /// <summary>
        /// Corresponding element of the change event.
        /// </summary>
        public readonly T element;

        /// <summary>
        /// Type of the change event.
        /// </summary>
        public readonly EventType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetChangedEventArg{T}"/> struct.
        /// </summary>
        /// <param name="element">Corresponding element of the change event.</param>
        /// <param name="type">Type of the change event.</param>
        public SetChangedEventArg(T element, EventType type)
        {
            this.element = element;
            this.type = type;
        }
    }
}

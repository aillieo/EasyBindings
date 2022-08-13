// -----------------------------------------------------------------------
// <copyright file="DictionaryChangedEventArg`1.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    /// <summary>
    /// Dictionary changed event argument.
    /// </summary>
    /// <typeparam name="T">Key type of the dictionary.</typeparam>
    public readonly struct DictionaryChangedEventArg<T>
    {
        /// <summary>
        /// Corresponding key of the change event.
        /// </summary>
        public readonly T key;

        /// <summary>
        /// Type of the change event.
        /// </summary>
        public readonly EventType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryChangedEventArg{T}"/> struct.
        /// </summary>
        /// <param name="key">Corresponding key of the change event.</param>
        /// <param name="type">Type of the change event.</param>
        public DictionaryChangedEventArg(T key, EventType type)
        {
            this.key = key;
            this.type = type;
        }
    }
}

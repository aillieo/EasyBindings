// -----------------------------------------------------------------------
// <copyright file="PropertyChangedEventArg`1.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings
{
    /// <summary>
    /// Property value changed event argument.
    /// </summary>
    /// <typeparam name="T">Value type of a property.</typeparam>
    public readonly struct PropertyChangedEventArg<T>
    {
        /// <summary>
        /// Old value of the property before it changes.
        /// </summary>
        public readonly T oldValue;

        /// <summary>
        /// New value of the property after it changes.
        /// </summary>
        public readonly T nextValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArg{T}"/> struct.
        /// </summary>
        /// <param name="oldValue">Old value of the property before it changes.</param>
        /// <param name="nextValue">New value of the property after it changes.</param>
        public PropertyChangedEventArg(T oldValue, T nextValue)
        {
            this.oldValue = oldValue;
            this.nextValue = nextValue;
        }
    }
}

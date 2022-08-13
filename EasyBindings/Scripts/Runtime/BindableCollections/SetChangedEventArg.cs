// -----------------------------------------------------------------------
// <copyright file="SetChangedEventArg.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    /// <summary>
    /// Set changed event argument.
    /// </summary>
    public readonly struct SetChangedEventArg
    {
        /// <summary>
        /// Type of the change event.
        /// </summary>
        public readonly EventType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetChangedEventArg"/> struct.
        /// </summary>
        /// <param name="type">Type of the change event.</param>
        public SetChangedEventArg(EventType type)
        {
            this.type = type;
        }
    }
}

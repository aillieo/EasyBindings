// -----------------------------------------------------------------------
// <copyright file="BindableObject.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// An object that can be bound with callbacks which will be invoked when properties of the object change.
    /// </summary>
    public abstract class BindableObject
    {
        private readonly EasyDelegate<string> onPropertyChangedDel = new EasyDelegate<string>();

        /// <summary>
        /// Gets the event that the object property changed.
        /// </summary>
        public EasyEvent<string> onPropertyChanged => this.onPropertyChangedDel;

        /// <summary>
        /// A helper method used to change value of a property and fire event.
        /// </summary>
        /// <param name="currentValue">Current value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <returns>Whether the property value changed.</returns>
        protected bool SetValue<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                return false;
            }

            currentValue = newValue;
            this.onPropertyChangedDel.Invoke(propertyName);
            return true;
        }
    }
}

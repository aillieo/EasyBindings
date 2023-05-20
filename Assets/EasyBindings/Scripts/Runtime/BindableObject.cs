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
        internal readonly EasyDelegate<string> onPropertyChanged = new EasyDelegate<string>();

        /// <summary>
        /// Notify that the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.onPropertyChanged.Invoke(propertyName);
        }

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
            if (typeof(T).IsValueType)
            {
                if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                {
                    return false;
                }
            }
            else
            {
                if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
                {
                    return false;
                }
            }

            currentValue = newValue;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// A helper method used to change value of a struct property and fire event.
        /// </summary>
        /// <param name="currentValue">Current value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <returns>Whether the property value changed.</returns>
        protected bool SetStructValue<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
            where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                return false;
            }

            currentValue = newValue;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// A helper method used to change value of a class property and fire event.
        /// </summary>
        /// <param name="currentValue">Current value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <returns>Whether the property value changed.</returns>
        protected bool SetClassValue<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
            where T : class
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
            {
                return false;
            }

            currentValue = newValue;
            this.NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}

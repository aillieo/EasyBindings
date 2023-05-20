// -----------------------------------------------------------------------
// <copyright file="BindableProperty`1.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings
{
    using System.Collections.Generic;

    /// <summary>
    /// A property that can be bound with callbacks which will be invoked when the property value changes.
    /// </summary>
    /// <typeparam name="T">Value type of a property.</typeparam>
    public class BindableProperty<T>
    {
        internal readonly EasyDelegate<PropertyChangedEventArg<T>> onValueChanged = new EasyDelegate<PropertyChangedEventArg<T>>();
        private readonly IEqualityComparer<T> equalityComparer;
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableProperty{T}"/> class.
        /// </summary>
        /// <param name="initialValue">Initial value of the property.</param>
        public BindableProperty(T initialValue)
        {
            this.value = initialValue;
            this.equalityComparer = EqualityComparer<T>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableProperty{T}"/> class.
        /// </summary>
        /// <param name="initialValue">Initial value of the property.</param>
        /// <param name="equalityComparer">Custom <see cref="IEqualityComparer{T}"/> to judge whether the value changes when <see cref="Next"/> is called.</param>
        public BindableProperty(T initialValue, IEqualityComparer<T> equalityComparer)
        {
            this.value = initialValue;
            this.equalityComparer = equalityComparer;
        }

        /// <summary>
        /// Gets current value of the property.
        /// </summary>
        public T CurrentValue => this.value;

        /// <summary>
        /// Set new value of the property.
        /// </summary>
        /// <param name="nextValue">New value of the property.</param>
        /// <returns>Whether the value changes.</returns>
        public bool Next(T nextValue)
        {
            if (this.equalityComparer.Equals(this.value, nextValue))
            {
                return false;
            }

            T oldValue = this.value;
            this.value = nextValue;
            this.onValueChanged.Invoke(new PropertyChangedEventArg<T>(oldValue, nextValue));
            return true;
        }
    }
}

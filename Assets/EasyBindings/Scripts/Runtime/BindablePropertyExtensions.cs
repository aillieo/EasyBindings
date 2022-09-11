// -----------------------------------------------------------------------
// <copyright file="BindablePropertyExtensions.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Game
{
    using AillieoUtils.EasyBindings;

    /// <summary>
    /// Short cuts for numeric <see cref="BindableProperty{T}"/> types.
    /// </summary>
    public static class BindablePropertyExtensions
    {
        /// <summary>
        /// Addition.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Addend.</param>
        public static void Add(this BindableProperty<int> bindableProperty, int value)
        {
            var v = bindableProperty.CurrentValue;
            v += value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Subtraction.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Subtrahend.</param>
        public static void Subtract(this BindableProperty<int> bindableProperty, int value)
        {
            var v = bindableProperty.CurrentValue;
            v -= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Multiplication.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Multiplier.</param>
        public static void Multiply(this BindableProperty<int> bindableProperty, int value)
        {
            var v = bindableProperty.CurrentValue;
            v *= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Division.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Divisor.</param>
        public static void Divide(this BindableProperty<int> bindableProperty, int value)
        {
            var v = bindableProperty.CurrentValue;
            v /= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Addition.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Addend.</param>
        public static void Add(this BindableProperty<float> bindableProperty, float value)
        {
            var v = bindableProperty.CurrentValue;
            v += value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Subtraction.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Subtrahend.</param>
        public static void Subtract(this BindableProperty<float> bindableProperty, float value)
        {
            var v = bindableProperty.CurrentValue;
            v -= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Multiplication.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Multiplier.</param>
        public static void Multiply(this BindableProperty<float> bindableProperty, float value)
        {
            var v = bindableProperty.CurrentValue;
            v *= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Division.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Divisor.</param>
        public static void Divide(this BindableProperty<float> bindableProperty, float value)
        {
            var v = bindableProperty.CurrentValue;
            v /= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Addition.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Addend.</param>
        public static void Add(this BindableProperty<long> bindableProperty, long value)
        {
            var v = bindableProperty.CurrentValue;
            v += value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Subtraction.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Subtrahend.</param>
        public static void Subtract(this BindableProperty<long> bindableProperty, long value)
        {
            var v = bindableProperty.CurrentValue;
            v -= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Multiplication.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Multiplier.</param>
        public static void Multiply(this BindableProperty<long> bindableProperty, long value)
        {
            var v = bindableProperty.CurrentValue;
            v *= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Division.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Divisor.</param>
        public static void Divide(this BindableProperty<long> bindableProperty, long value)
        {
            var v = bindableProperty.CurrentValue;
            v /= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Addition.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Addend.</param>
        public static void Add(this BindableProperty<double> bindableProperty, double value)
        {
            var v = bindableProperty.CurrentValue;
            v += value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Subtraction.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Subtrahend.</param>
        public static void Subtract(this BindableProperty<double> bindableProperty, double value)
        {
            var v = bindableProperty.CurrentValue;
            v -= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Multiplication.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Multiplier.</param>
        public static void Multiply(this BindableProperty<double> bindableProperty, double value)
        {
            var v = bindableProperty.CurrentValue;
            v *= value;
            bindableProperty.Next(v);
        }

        /// <summary>
        /// Division.
        /// </summary>
        /// <param name="bindableProperty">Original value.</param>
        /// <param name="value">Divisor.</param>
        public static void Divide(this BindableProperty<double> bindableProperty, double value)
        {
            var v = bindableProperty.CurrentValue;
            v /= value;
            bindableProperty.Next(v);
        }
    }
}

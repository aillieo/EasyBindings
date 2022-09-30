// -----------------------------------------------------------------------
// <copyright file="BinderExtensions.UGUI.Connect.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.UGUI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Binder extension methods for Unity UGUI.
    /// </summary>
    public static partial class BinderUGUIExtensions
    {
        /// <summary>
        /// Create a two-way binding between a <see cref="BindableProperty{bool}"/> and a <see cref="Toggle"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="toggle">Toggle to bind.</param>
        public static void Connect_ToggleIsOn(this Binder binder, BindableProperty<bool> bindableProperty, Toggle toggle)
        {
            int depth = 0;
            binder.BindPropertyValue(bindableProperty, v => toggle.isOn = v);
            binder.BindUnityEvent(toggle.onValueChanged, v =>
            {
                if (depth++ > 100)
                {
                    throw new StackOverflowException();
                }

                try
                {
                    bindableProperty.Next(v);
                }
                finally
                {
                    depth--;
                }
            });
        }

        /// <summary>
        /// Create a two-way binding between a <see cref="BindableProperty{float}"/> and a <see cref="Slider"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="slider">Slider to bind.</param>
        public static void Connect_SliderValue(this Binder binder, BindableProperty<float> bindableProperty, Slider slider)
        {
            int depth = 0;
            binder.BindPropertyValue(bindableProperty, v => slider.value = v);
            binder.BindUnityEvent(slider.onValueChanged, v =>
            {
                if (depth++ > 100)
                {
                    throw new StackOverflowException();
                }

                try
                {
                    bindableProperty.Next(v);
                }
                finally
                {
                    depth--;
                }
            });
        }

        /// <summary>
        /// Create a two-way binding between a <see cref="BindableProperty{string}"/> and a <see cref="InputField"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="inputField">InputField to bind.</param>
        public static void Connect_InputFieldText(this Binder binder, BindableProperty<string> bindableProperty, InputField inputField)
        {
            int depth = 0;
            binder.BindPropertyValue(bindableProperty, v => inputField.text = v);
            binder.BindUnityEvent(inputField.onEndEdit, v =>
            {
                if (depth++ > 100)
                {
                    throw new StackOverflowException();
                }

                try
                {
                    bindableProperty.Next(v);
                }
                finally
                {
                    depth--;
                }
            });
        }

        /// <summary>
        /// Create a two-way binding between a <see cref="BindableProperty{Vector2}"/> and a <see cref="ScrollRect"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="scrollRect">ScrollRect to bind.</param>
        public static void Connect_ScrollRectValue(this Binder binder, BindableProperty<Vector2> bindableProperty, ScrollRect scrollRect)
        {
            int depth = 0;
            binder.BindPropertyValue(bindableProperty, v => scrollRect.normalizedPosition = v);
            binder.BindUnityEvent(scrollRect.onValueChanged, v =>
            {
                if (depth++ > 100)
                {
                    throw new StackOverflowException();
                }

                try
                {
                    bindableProperty.Next(v);
                }
                finally
                {
                    depth--;
                }
            });
        }

        /// <summary>
        /// Create a two-way binding between a <see cref="BindableProperty{int}"/> and a <see cref="Dropdown"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="dropdown">Dropdown to bind.</param>
        public static void Connect_DropdownValue(this Binder binder, BindableProperty<int> bindableProperty, Dropdown dropdown)
        {
            int depth = 0;
            binder.BindPropertyValue(bindableProperty, v => dropdown.value = v);
            binder.BindUnityEvent(dropdown.onValueChanged, v =>
            {
                if (depth++ > 100)
                {
                    throw new StackOverflowException();
                }

                try
                {
                    bindableProperty.Next(v);
                }
                finally
                {
                    depth--;
                }
            });
        }
    }
}

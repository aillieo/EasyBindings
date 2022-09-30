// -----------------------------------------------------------------------
// <copyright file="BinderExtensions.UGUI.V2M.cs" company="AillieoTech">
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
        /// Create a one-way binding from a <see cref="Toggle"/> to a <see cref="BindableProperty{bool}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="toggle">Toggle to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        public static void BindV2M_ToggleIsOn(this Binder binder, Toggle toggle, BindableProperty<bool> bindableProperty)
        {
            bindableProperty.Next(toggle.isOn);
            binder.BindUnityEvent(toggle.onValueChanged, v => bindableProperty.Next(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Toggle"/> to a <see cref="BindableProperty{T}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="toggle">Toggle to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindV2M_ToggleIsOn<T>(this Binder binder, Toggle toggle, BindableProperty<T> bindableProperty, Func<bool, T> mapper)
        {
            bindableProperty.Next(mapper(toggle.isOn));
            binder.BindUnityEvent(toggle.onValueChanged, v => bindableProperty.Next(mapper(v)));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Toggle"/> to a <see cref="BindableObject"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="toggle">Toggle to bind.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="handler">Handler function.</param>
        public static void BindV2M_ToggleIsOn(this Binder binder, Toggle toggle, BindableObject bindableObject, Action<bool, BindableObject> handler)
        {
            handler(toggle.isOn, bindableObject);
            binder.BindUnityEvent(toggle.onValueChanged, v => handler(v, bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Slider"/> to a <see cref="BindableProperty{float}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="slider">Slider to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        public static void BindV2M_SliderValue(this Binder binder, Slider slider, BindableProperty<float> bindableProperty)
        {
            bindableProperty.Next(slider.value);
            binder.BindUnityEvent(slider.onValueChanged, v => bindableProperty.Next(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Slider"/> to a <see cref="BindableProperty{T}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="slider">Slider to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindV2M_SliderValue<T>(this Binder binder, Slider slider, BindableProperty<T> bindableProperty, Func<float, T> mapper)
        {
            bindableProperty.Next(mapper(slider.value));
            binder.BindUnityEvent(slider.onValueChanged, v => bindableProperty.Next(mapper(v)));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Slider"/> to a <see cref="BindableObject"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="slider">Slider to bind.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="handler">Handler function.</param>
        public static void BindV2M_SliderValue(this Binder binder, Slider slider, BindableObject bindableObject, Action<float, BindableObject> handler)
        {
            handler(slider.value, bindableObject);
            binder.BindUnityEvent(slider.onValueChanged, v => handler(v, bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="InputField"/> to a <see cref="BindableProperty{string}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="inputField">InputField to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        public static void BindV2M_InputFieldText(this Binder binder, InputField inputField, BindableProperty<string> bindableProperty)
        {
            bindableProperty.Next(inputField.text);
            binder.BindUnityEvent(inputField.onEndEdit, v => bindableProperty.Next(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="InputField"/> to a <see cref="BindableProperty{T}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="inputField">InputField to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindV2M_InputFieldText<T>(this Binder binder, InputField inputField, BindableProperty<T> bindableProperty, Func<string, T> mapper)
        {
            bindableProperty.Next(mapper(inputField.text));
            binder.BindUnityEvent(inputField.onEndEdit, v => bindableProperty.Next(mapper(v)));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="InputField"/> to a <see cref="BindableObject"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="inputField">InputField to bind.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="handler">Handler function.</param>
        public static void BindV2M_InputFieldText(this Binder binder, InputField inputField, BindableObject bindableObject, Action<string, BindableObject> handler)
        {
            handler(inputField.text, bindableObject);
            binder.BindUnityEvent(inputField.onEndEdit, v => handler(v, bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="ScrollRect"/> to a <see cref="BindableProperty{Vector2}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="scrollRect">ScrollRect to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        public static void BindV2M_ScrollRectValue(this Binder binder, ScrollRect scrollRect, BindableProperty<Vector2> bindableProperty)
        {
            bindableProperty.Next(scrollRect.normalizedPosition);
            binder.BindUnityEvent(scrollRect.onValueChanged, v => bindableProperty.Next(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="ScrollRect"/> to a <see cref="BindableProperty{T}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="scrollRect">ScrollRect to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindV2M_ScrollRectValue<T>(this Binder binder, ScrollRect scrollRect, BindableProperty<T> bindableProperty, Func<Vector2, T> mapper)
        {
            bindableProperty.Next(mapper(scrollRect.normalizedPosition));
            binder.BindUnityEvent(scrollRect.onValueChanged, v => bindableProperty.Next(mapper(v)));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="ScrollRect"/> to a <see cref="BindableObject"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="scrollRect">ScrollRect to bind.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="handler">Handler function.</param>
        public static void BindV2M_ScrollRectValue(this Binder binder, ScrollRect scrollRect, BindableObject bindableObject, Action<Vector2, BindableObject> handler)
        {
            handler(scrollRect.normalizedPosition, bindableObject);
            binder.BindUnityEvent(scrollRect.onValueChanged, v => handler(v, bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Dropdown"/> to a <see cref="BindableProperty{int}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="dropdown">Dropdown to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        public static void BindV2M_DropdownValue(this Binder binder, Dropdown dropdown, BindableProperty<int> bindableProperty)
        {
            bindableProperty.Next(dropdown.value);
            binder.BindUnityEvent(dropdown.onValueChanged, v => bindableProperty.Next(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Dropdown"/> to a <see cref="BindableProperty{T}"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="dropdown">Dropdown to bind.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindV2M_DropdownValue<T>(this Binder binder, Dropdown dropdown, BindableProperty<T> bindableProperty, Func<int, T> mapper)
        {
            bindableProperty.Next(mapper(dropdown.value));
            binder.BindUnityEvent(dropdown.onValueChanged, v => bindableProperty.Next(mapper(v)));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="Dropdown"/> to a <see cref="BindableObject"/>.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="dropdown">Dropdown to bind.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="handler">Handler function.</param>
        public static void BindV2M_DropdownValue(this Binder binder, Dropdown dropdown, BindableObject bindableObject, Action<int, BindableObject> handler)
        {
            handler(dropdown.value, bindableObject);
            binder.BindUnityEvent(dropdown.onValueChanged, v => handler(v, bindableObject));
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="BinderExtensions.UGUI.M2V.cs" company="AillieoTech">
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
        /// Create a one-way binding from a <see cref="BindableProperty{string}"/> to a <see cref="Text"/>'s value.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="text">Text to bind.</param>
        public static void BindM2V_TextText(this Binder binder, BindableProperty<string> bindableProperty, Text text)
        {
            binder.BindPropertyValue(bindableProperty, v => text.text = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Text"/>'s value.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="text">Text to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_TextText<T>(this Binder binder, BindableProperty<T> bindableProperty, Text text, Func<T, string> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => text.text = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Text"/>'s value.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="text">Text to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_TextText(this Binder binder, BindableObject bindableObject, string propName, Text text, Func<BindableObject, string> resolver)
        {
            text.text = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => text.text = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{Sprite}"/> to a <see cref="Image"/>'s sprite.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="image">Image to bind.</param>
        public static void BindM2V_ImageSprite(this Binder binder, BindableProperty<Sprite> bindableProperty, Image image)
        {
            binder.BindPropertyValue(bindableProperty, v => image.sprite = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Image"/>'s sprite.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="image">Image to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_ImageSprite<T>(this Binder binder, BindableProperty<T> bindableProperty, Image image, Func<T, Sprite> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => image.sprite = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Image"/>'s sprite.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="image">Image to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_ImageSprite(this Binder binder, BindableObject bindableObject, string propName, Image image, Func<BindableObject, Sprite> resolver)
        {
            image.sprite = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => image.sprite = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{float}"/> to a <see cref="Image"/>'s fillAmount.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="image">Image to bind.</param>
        public static void BindM2V_ImageFillAmount(this Binder binder, BindableProperty<float> bindableProperty, Image image)
        {
            binder.BindPropertyValue(bindableProperty, v => image.fillAmount = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Image"/>'s fillAmount.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="image">Image to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_ImageFillAmount<T>(this Binder binder, BindableProperty<T> bindableProperty, Image image, Func<T, float> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => image.fillAmount = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Image"/>'s fillAmount.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="image">Image to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_ImageFillAmount(this Binder binder, BindableObject bindableObject, string propName, Image image, Func<BindableObject, float> resolver)
        {
            image.fillAmount = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => image.fillAmount = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{float}"/> to a <see cref="Slider"/>'s value.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="slider">Slider to bind.</param>
        public static void BindM2V_SliderValue(this Binder binder, BindableProperty<float> bindableProperty, Slider slider)
        {
            binder.BindPropertyValue(bindableProperty, v => slider.value = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Slider"/>'s value.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="slider">Slider to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_SliderValue<T>(this Binder binder, BindableProperty<T> bindableProperty, Slider slider, Func<T, float> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => slider.value = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Slider"/>'s value.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="slider">Slider to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_SliderValue(this Binder binder, BindableObject bindableObject, string propName, Slider slider, Func<BindableObject, float> resolver)
        {
            slider.value = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => slider.value = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{bool}"/> to a <see cref="Selectable"/>'s interactable property.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="selectable">Selectable to bind.</param>
        public static void BindM2V_SelectableInteractable(this Binder binder, BindableProperty<bool> bindableProperty, Selectable selectable)
        {
            binder.BindPropertyValue(bindableProperty, v => selectable.interactable = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Selectable"/>'s interactable property.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="selectable">Selectable to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_SelectableInteractable<T>(this Binder binder, BindableProperty<T> bindableProperty, Selectable selectable, Func<T, bool> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => selectable.interactable = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Selectable"/>'s interactable property.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="selectable">Selectable to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_SelectableInteractable(this Binder binder, BindableObject bindableObject, string propName, Selectable selectable, Func<BindableObject, bool> resolver)
        {
            selectable.interactable = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => selectable.interactable = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{Color}"/> to a <see cref="Graphic"/>'s color.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="graphic">Graphic to bind.</param>
        public static void BindM2V_GraphicColor(this Binder binder, BindableProperty<Color> bindableProperty, Graphic graphic)
        {
            binder.BindPropertyValue(bindableProperty, v => graphic.color = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Graphic"/>'s color.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="graphic">Graphic to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_GraphicColor<T>(this Binder binder, BindableProperty<T> bindableProperty, Graphic graphic, Func<T, Color> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => graphic.color = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Graphic"/>'s color.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="graphic">Graphic to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_GraphicColor(this Binder binder, BindableObject bindableObject, string propName, Graphic graphic, Func<BindableObject, Color> resolver)
        {
            graphic.color = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => graphic.color = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{bool}"/> to a <see cref="Behaviour"/>'s enabled state.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="behaviour">Behaviour to bind.</param>
        public static void BindM2V_BehaviourEnabled(this Binder binder, BindableProperty<bool> bindableProperty, Behaviour behaviour)
        {
            binder.BindPropertyValue(bindableProperty, v => behaviour.enabled = v);
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="Behaviour"/>'s enabled state.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="behaviour">Behaviour to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_BehaviourEnabled<T>(this Binder binder, BindableProperty<T> bindableProperty, Behaviour behaviour, Func<T, bool> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => behaviour.enabled = mapper(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="Behaviour"/>'s enabled state.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="behaviour">Behaviour to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_BehaviourEnabled(this Binder binder, BindableObject bindableObject, string propName, Behaviour behaviour, Func<BindableObject, bool> resolver)
        {
            behaviour.enabled = resolver(bindableObject);
            binder.BindObject(bindableObject, propName, () => behaviour.enabled = resolver(bindableObject));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{bool}"/> to a <see cref="GameObject"/>'s active state.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="gameObject">GameObject to bind.</param>
        public static void BindM2V_GameObjectActive(this Binder binder, BindableProperty<bool> bindableProperty, GameObject gameObject)
        {
            binder.BindPropertyValue(bindableProperty, v => gameObject.SetActive(v));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableProperty{T}"/> to a <see cref="GameObject"/>'s active state.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableProperty">Property to bind.</param>
        /// <param name="gameObject">GameObject to bind.</param>
        /// <param name="mapper">Convert function.</param>
        /// <typeparam name="T">Value type of BindableProperty.</typeparam>
        public static void BindM2V_GameObjectActive<T>(this Binder binder, BindableProperty<T> bindableProperty, GameObject gameObject, Func<T, bool> mapper)
        {
            binder.BindPropertyValue(bindableProperty, v => gameObject.SetActive(mapper(v)));
        }

        /// <summary>
        /// Create a one-way binding from a <see cref="BindableObject"/> to a <see cref="GameObject"/>'s active state.
        /// </summary>
        /// <param name="binder"><see cref="Binder"/> to record this binding.</param>
        /// <param name="bindableObject">BindableObject to bind.</param>
        /// <param name="propName">Name of property that concerned.</param>
        /// <param name="gameObject">GameObject to bind.</param>
        /// <param name="resolver">Resolver function.</param>
        public static void BindM2V_GameObjectActive(this Binder binder, BindableObject bindableObject, string propName, GameObject gameObject, Func<BindableObject, bool> resolver)
        {
            gameObject.SetActive(resolver(bindableObject));
            binder.BindObject(bindableObject, propName, () => gameObject.SetActive(resolver(bindableObject)));
        }
    }
}

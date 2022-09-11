// -----------------------------------------------------------------------
// <copyright file="BinderExtensions.Collections.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings
{
    using System;
    using AillieoUtils.EasyBindings.Collections;

    /// <summary>
    /// Binder extension methods for BindableCollections.
    /// </summary>
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Bind an event handler to a <see cref="BindableList{T}"/>.
        /// </summary>
        /// <param name="binder">Binder object to record this binding.</param>
        /// <param name="list"><see cref="BindableList{T}"/> to bind to.</param>
        /// <param name="eventHandler">Event handler to bind.</param>
        /// <typeparam name="T">List element type.</typeparam>
        public static void BindList<T>(this Binder binder, BindableList<T> list, Action<ListChangedEventArg> eventHandler)
        {
            IEventHandle handle = list.listChangedEvent.AddListener(eventHandler);
            binder.Record(handle);
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="binder">Binder object to record this binding.</param>
        /// <param name="dictionary"><see cref="BindDictionary{TKey, TValue}"/> to bind to.</param>
        /// <param name="eventHandler">Event handler to bind.</param>
        /// <typeparam name="TKey">Dictionary key type.</typeparam>
        /// <typeparam name="TValue">Dictionary value type.</typeparam>
        public static void BindDictionary<TKey, TValue>(this Binder binder, BindableDictionary<TKey, TValue> dictionary, Action<DictionaryChangedEventArg<TKey>> eventHandler)
        {
            IEventHandle handle = dictionary.dictionaryChangedEvent.AddListener(eventHandler);
            binder.Record(handle);
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindSet{T}"/>.
        /// </summary>
        /// <param name="binder">Binder object to record this binding.</param>
        /// <param name="set"><see cref="BindableSet{T}"/> to bind to.</param>
        /// <param name="eventHandler">Event handler to bind.</param>
        /// <typeparam name="T">Set element type.</typeparam>
        public static void BindSet<T>(this Binder binder, BindableSet<T> set, Action<SetChangedEventArg<T>> eventHandler)
        {
            IEventHandle handle = set.setChangedEvent.AddListener(eventHandler);
            binder.Record(handle);
        }
    }
}

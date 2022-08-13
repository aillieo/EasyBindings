// -----------------------------------------------------------------------
// <copyright file="BinderExtensions.UnityEvents.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings
{
    using UnityEngine.Events;

    /// <summary>
    /// Binder extension methods for UnityEvents.
    /// </summary>
    public static partial class BinderExtensions
    {
        /// <summary>
        /// Bind a UnityAction to a UnityEvent.
        /// </summary>
        /// <param name="binder">Binder object to record this binding.</param>
        /// <param name="evt"><see cref="UnityEvent"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="UnityAction"/> to bind.</param>
        public static void BindUnityEvent(this Binder binder, UnityEvent evt, UnityAction eventHandler)
        {
            evt.AddListener(eventHandler);
            binder.RegisterCustomCleanupAction(() => evt.RemoveListener(eventHandler));
        }

        /// <summary>
        /// Bind a UnityAction to a UnityEvent.
        /// </summary>
        /// <param name="binder">Binder object to record this binding.</param>
        /// <param name="evt"><see cref="UnityEvent{T}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="UnityAction{T}"/> to bind.</param>
        /// <typeparam name="T">Event arg for <see cref="UnityEvent{T}"/>.</typeparam>
        public static void BindUnityEvent<T>(this Binder binder, UnityEvent<T> evt, UnityAction<T> eventHandler)
        {
            evt.AddListener(eventHandler);
            binder.RegisterCustomCleanupAction(() => evt.RemoveListener(eventHandler));
        }

        /// <summary>
        /// Bind a UnityAction to a UnityEvent.
        /// </summary>
        /// <param name="binder">Binder object to record this binding.</param>
        /// <param name="evt"><see cref="UnityEvent{T0, T1}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="UnityAction{T0, T1}"/> to bind.</param>
        /// <typeparam name="T0">The first event arg for  <see cref="UnityEvent{T0, T1}"/>.</typeparam>
        /// <typeparam name="T1">The second event arg for  <see cref="UnityEvent{T0, T1}"/>.</typeparam>
        public static void BindUnityEvent<T0, T1>(this Binder binder, UnityEvent<T0, T1> evt, UnityAction<T0, T1> eventHandler)
        {
            evt.AddListener(eventHandler);
            binder.RegisterCustomCleanupAction(() => evt.RemoveListener(eventHandler));
        }
    }
}

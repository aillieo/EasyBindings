// -----------------------------------------------------------------------
// <copyright file="Binder.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A recorder that keeps bindings of:
    /// 1. An <see cref="Event"/>/<see cref="Event{T}"/> and its handlers;
    /// 2. A <see cref="UnityEngine.Events.UnityEvent"/> and its handlers;
    /// 3. A <see cref="BindableProperty{T}"/> and handlers to its value changes;
    /// 4. A <see cref="BindableObject"/> and handlers to its property changes;
    /// 5. Custom bindings with custom cleanup action registrations.
    /// and can remove all these bindings with simply <see cref="Dispose"/>.
    /// </summary>
    public sealed class Binder : IDisposable
    {
        private List<IEventHandle> handles;
        private Event disposeEvent;

        /// <summary>
        /// Record an <see cref="IEventHandle"/>.
        /// </summary>
        /// <param name="handle">The <see cref="IEventHandle"/> to record.</param>
        public void Record(IEventHandle handle)
        {
            if (handle == null)
            {
                return;
            }

            if (this.handles == null)
            {
                this.handles = new List<IEventHandle>()
                {
                    handle,
                };
            }
            else
            {
                this.handles.Add(handle);
            }
        }

        /// <summary>
        /// Register an custom cleanup action, which will be invoked when calling <see cref="Dispose"/>.
        /// </summary>
        /// <param name="action">The custom action to register.</param>
        public void RegisterCustomCleanupAction(Action action)
        {
            if (action == null)
            {
                return;
            }

            if (this.disposeEvent == null)
            {
                this.disposeEvent = new Event();
            }

            this.disposeEvent.AddListener(action);
        }

        /// <summary>
        /// Remove all event handlers recorded, and invoke all cleanup actions registered.
        /// </summary>
        public void Dispose()
        {
            if (this.handles != null)
            {
                foreach (var handle in this.handles)
                {
                    handle.Unlisten();
                }

                this.handles.Clear();
            }

            if (this.disposeEvent != null)
            {
                try
                {
                    this.disposeEvent.SafeInvoke();
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e);
                }

                this.disposeEvent.RemoveAllListeners();
            }
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindableProperty{T}"/>, and the handler will be invoked when property value changes.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="bindableProperty">The <see cref="BindableProperty{T}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action{T, T}"/> to bind, and both the old and new values will be passed as parameters.</param>
        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<T, T> eventHandler)
        {
            this.BindPropertyChangeInternal(bindableProperty, arg => eventHandler(arg.oldValue, arg.nextValue));
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindableProperty{T}"/>, and the handler will be invoked when property value changes.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="bindableProperty">The <see cref="BindableProperty{T}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action{T}"/> to bind, and the new value will be passed as parameter.</param>
        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<T> eventHandler)
        {
            this.BindPropertyChangeInternal(bindableProperty, arg => eventHandler(arg.nextValue));
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindableProperty{T}"/>, and the handler will be invoked when property value changes.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="bindableProperty">The <see cref="BindableProperty{T}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action"/> to bind, and nothing will be passed as parameter.</param>
        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action eventHandler)
        {
            this.BindPropertyChangeInternal(bindableProperty, arg => eventHandler());
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindableProperty{T}"/>, and the handler will be invoked immediately and later when property value changes.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="bindableProperty">The <see cref="BindableProperty{T}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action{T}"/> to bind, and the value of property will be passed as parameter.</param>
        public void BindPropertyValue<T>(BindableProperty<T> bindableProperty, Action<T> eventHandler)
        {
            this.BindPropertyChange(bindableProperty, eventHandler);
            eventHandler.Invoke(bindableProperty.CurrentValue);
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindableObject"/>, and the handler will be invoked when object property changes.
        /// </summary>
        /// <param name="bindableObject">The <see cref="BindableObject"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action{string}"/> to bind, and the property name will be passed as parameter.</param>
        public void BindObjectChange(BindableObject bindableObject, Action<string> eventHandler)
        {
            if (bindableObject == null)
            {
                throw new ArgumentNullException(nameof(bindableObject));
            }

            if (eventHandler == null)
            {
                return;
            }

            IEventHandle handle = bindableObject.onPropertyChanged.AddListener(eventHandler);
            this.Record(handle);
        }

        /// <summary>
        /// Bind an event handler to a <see cref="BindableObject"/>, and the handler will be invoked when object property changes.
        /// </summary>
        /// <param name="bindableObject">The <see cref="BindableObject"/> to bind to.</param>
        /// <param name="propertyName">The property that is to listen to.</param>
        /// <param name="eventHandler"><see cref="Action"/> to bind.</param>
        public void BindObjectChange(BindableObject bindableObject, string propertyName, Action eventHandler)
        {
            if (bindableObject == null)
            {
                throw new ArgumentNullException(nameof(bindableObject));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"Null or empty: {nameof(propertyName)}");
            }

            if (eventHandler == null)
            {
                return;
            }

            IEventHandle handle = bindableObject.onPropertyChanged.AddListener(property =>
            {
                if (property == propertyName)
                {
                    eventHandler.Invoke();
                }
            });

            this.Record(handle);
        }

        /// <summary>
        /// Bind an event handler to an Easy Event.
        /// </summary>
        /// <typeparam name="T">Event argument type.</typeparam>
        /// <param name="evt"><see cref="Event{T}"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action{T}"/> to bind.</param>
        public void BindEvent<T>(Event<T> evt, Action<T> eventHandler)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            if (eventHandler == null)
            {
                return;
            }

            IEventHandle handle = evt.AddListener(eventHandler);
            this.Record(handle);
        }

        /// <summary>
        /// Bind an event handler to an Easy Event.
        /// </summary>
        /// <param name="evt"><see cref="Event"/> to bind to.</param>
        /// <param name="eventHandler"><see cref="Action"/> to bind.</param>
        public void BindEvent(Event evt, Action eventHandler)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            if (eventHandler == null)
            {
                return;
            }

            IEventHandle handle = evt.AddListener(eventHandler);
            this.Record(handle);
        }

        private void BindPropertyChangeInternal<T>(BindableProperty<T> bindableProperty, Action<PropertyChangedEventArg<T>> eventHandler)
        {
            if (bindableProperty == null)
            {
                throw new ArgumentNullException(nameof(bindableProperty));
            }

            if (eventHandler == null)
            {
                return;
            }

            IEventHandle handle = bindableProperty.onValueChanged.AddListener(eventHandler);
            this.Record(handle);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace AillieoUtils.EasyBindings
{
    public class Binder : IDisposable
    {
        private List<IEventHandle> handles;
        private Event disposeEvent = new Event();

        public void Record(IEventHandle handle)
        {
            if (handles == null)
            {
                handles = new List<IEventHandle>()
                {
                    handle
                };
            }
            else
            {
                handles.Add(handle);
            }
        }

        public void Dispose()
        {
            if (handles != null)
            {
                foreach (var handle in handles)
                {
                    handle.Unlisten();
                }
            }

            handles.Clear();

            disposeEvent.SafeInvoke();
            disposeEvent.RemoveAllListeners();
        }

        public void Bind<T>(BindableProperty<T> bindableProperty, Action<PropertyChangedEventArg<T>> eventHandler)
        {
            IEventHandle handle = bindableProperty.onValueChanged.AddListener(eventHandler);
            Record(handle);
        }

        public void Bind(BindableObject bindableObject, Action<string> eventHandler)
        {
            IEventHandle handle = bindableObject.onPropertyChanged.AddListener(eventHandler);
            Record(handle);
        }

        public void Bind(BindableObject bindableObject, string propertyName, Action eventHandler)
        {
            IEventHandle handle = bindableObject.onPropertyChanged.AddListener(property =>
            {
                if (property == propertyName)
                {
                    eventHandler?.Invoke();
                }
            });

            Record(handle);
        }

        public void Bind<T>(Event<T> evt, Action<T> eventHandler)
        {
            IEventHandle handle = evt.AddListener(eventHandler);
            Record(handle);
        }

        public void Bind(Event evt, Action eventHandler)
        {
            IEventHandle handle = evt.AddListener(eventHandler);
            Record(handle);
        }

        // Unity Events
        public void Bind(UnityEvent evt, UnityAction eventHandler)
        {
            evt.AddListener(eventHandler);
            disposeEvent.ListenOnce(() => evt.RemoveListener(eventHandler));
        }

        public void Bind<T>(UnityEvent<T> evt, UnityAction<T> eventHandler)
        {
            evt.AddListener(eventHandler);
            disposeEvent.ListenOnce(() => evt.RemoveListener(eventHandler));
        }

        public void Bind<T, R>(UnityEvent<T, R> evt, UnityAction<T, R> eventHandler)
        {
            evt.AddListener(eventHandler);
            disposeEvent.ListenOnce(() => evt.RemoveListener(eventHandler));
        }
    }
}

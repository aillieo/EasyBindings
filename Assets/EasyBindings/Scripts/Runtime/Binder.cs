using System;
using System.Collections.Generic;

namespace AillieoUtils.EasyBindings
{
    public class Binder : IDisposable
    {
        private List<IEventHandle> handles;
        private Event disposeEvent;

        public void Record(IEventHandle handle)
        {
            if (handle == null)
            {
                return;
            }

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

        public void RegisterCustomCleanupAction(Action action)
        {
            if (action == null)
            {
                return;
            }

            if (disposeEvent == null)
            {
                disposeEvent = new Event();
            }

            disposeEvent.AddListener(action);
        }

        public void Dispose()
        {
            if (handles != null)
            {
                foreach (var handle in handles)
                {
                    handle.Unlisten();
                }

                handles.Clear();
            }

            if (disposeEvent != null)
            {
                disposeEvent.SafeInvoke();
                disposeEvent.RemoveAllListeners();
            }
        }

        public void Bind<T>(BindableProperty<T> bindableProperty, Action<PropertyChangedEventArg<T>> eventHandler)
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
            Record(handle);
        }

        public void Bind(BindableObject bindableObject, Action<string> eventHandler)
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
            Record(handle);
        }

        public void Bind(BindableObject bindableObject, string propertyName, Action eventHandler)
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
                    eventHandler?.Invoke();
                }
            });

            Record(handle);
        }

        public void Bind<T>(Event<T> evt, Action<T> eventHandler)
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
            Record(handle);
        }

        public void Bind(Event evt, Action eventHandler)
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
            Record(handle);
        }
    }
}

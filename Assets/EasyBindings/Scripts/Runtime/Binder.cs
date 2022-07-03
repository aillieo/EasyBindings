using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                try
                {
                    disposeEvent.SafeInvoke();
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e);
                }

                disposeEvent.RemoveAllListeners();
            }
        }

        private void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<PropertyChangedEventArg<T>> eventHandler)
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

        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<T, T> eventHandler)
        {
            BindPropertyChange(bindableProperty, arg => eventHandler(arg.oldValue, arg.nextValue));
        }

        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action eventHandler)
        {
            BindPropertyChange(bindableProperty, (Action<PropertyChangedEventArg<T>>)(arg => eventHandler()));
        }

        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<T> eventHandler)
        {
            BindPropertyChange(bindableProperty, arg => eventHandler(arg.nextValue));
        }

        public void BindPropertyValue<T>(BindableProperty<T> bindableProperty, Action<T> eventHandler)
        {
            BindPropertyChange(bindableProperty, eventHandler);
            eventHandler.Invoke(bindableProperty.CurrentValue);
        }

        public void BindObject(BindableObject bindableObject, Action<string> eventHandler)
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

        public void BindObject(BindableObject bindableObject, string propertyName, Action eventHandler)
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
            Record(handle);
        }

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
            Record(handle);
        }
    }
}

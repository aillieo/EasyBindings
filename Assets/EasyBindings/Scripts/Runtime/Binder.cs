namespace AillieoUtils.EasyBindings
{
    using System;
    using System.Collections.Generic;

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
            this.Record(handle);
        }

        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<T, T> eventHandler)
        {
            this.BindPropertyChange(bindableProperty, arg => eventHandler(arg.oldValue, arg.nextValue));
        }

        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action eventHandler)
        {
            this.BindPropertyChange(bindableProperty, (Action<PropertyChangedEventArg<T>>)(arg => eventHandler()));
        }

        public void BindPropertyChange<T>(BindableProperty<T> bindableProperty, Action<T> eventHandler)
        {
            this.BindPropertyChange(bindableProperty, arg => eventHandler(arg.nextValue));
        }

        public void BindPropertyValue<T>(BindableProperty<T> bindableProperty, Action<T> eventHandler)
        {
            this.BindPropertyChange(bindableProperty, eventHandler);
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
            this.Record(handle);
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

            this.Record(handle);
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
            this.Record(handle);
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
            this.Record(handle);
        }
    }
}

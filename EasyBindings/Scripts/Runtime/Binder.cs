using System;
using System.Collections.Generic;

namespace AillieoUtils.EasyBindings
{
    public class Binder : IDisposable
    {
        private List<IEventHandle> handles;

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
    }
}

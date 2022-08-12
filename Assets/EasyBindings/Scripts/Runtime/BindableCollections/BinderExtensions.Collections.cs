namespace AillieoUtils.EasyBindings
{
    using System;
    using AillieoUtils.EasyBindings.Collections;

    public static partial class BinderExtensions
    {
        public static void BindList<T>(this Binder binder, BindableList<T> list, Action<ListChangedEventArg> eventHandler)
        {
            IEventHandle handle = list.listChangedEvent.AddListener(eventHandler);
            binder.Record(handle);
        }

        public static void BindDictionary<TKey, TValue>(this Binder binder, BindableDictionary<TKey, TValue> dictionary, Action<DictionaryChangedEventArg<TKey>> eventHandler)
        {
            IEventHandle handle = dictionary.dictionaryChangedEvent.AddListener(eventHandler);
            binder.Record(handle);
        }

        public static void BindSet<T>(this Binder binder, BindableSet<T> set, Action<SetChangedEventArg> eventHandler)
        {
            IEventHandle handle = set.setChangedEvent.AddListener(eventHandler);
            binder.Record(handle);
        }
    }
}

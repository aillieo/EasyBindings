using System.Collections;
using System.Collections.Generic;

namespace AillieoUtils.EasyBindings.Collections
{
    public class BindableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> source;
        internal readonly Event<DictionaryChangedEventArg<TKey>> dictionaryChangedEvent = new Event<DictionaryChangedEventArg<TKey>>();

        public ICollection<TKey> Keys => source.Keys;

        public ICollection<TValue> Values => source.Values;

        public int Count => source.Count;

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => source[key];
            set
            {
                if (source.ContainsKey(key))
                {
                    source[key] = value;
                    NotifyPropertyChanged(key, EventType.Update);
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        public BindableDictionary(IDictionary<TKey, TValue> source)
        {
            this.source = new Dictionary<TKey, TValue>(source);
        }

        public BindableDictionary()
        {
            this.source = new Dictionary<TKey, TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            source.Add(key, value);
            NotifyPropertyChanged(key, EventType.Add);
        }

        public bool ContainsKey(TKey key)
        {
            return source.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (source.Remove(key))
            {
                NotifyPropertyChanged(key, EventType.Remove);
                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return source.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)source).Add(item);
            NotifyPropertyChanged(item.Key, EventType.Add);
        }

        public void Clear()
        {
            if (source.Count == 0)
            {
                return;
            }

            source.Clear();
            NotifyPropertyChanged(default, EventType.Clear);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)source).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)source).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)source).Remove(item);
            NotifyPropertyChanged(item.Key, EventType.Remove);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)source).GetEnumerator();
        }

        protected void NotifyPropertyChanged(TKey key, EventType eventType)
        {
            dictionaryChangedEvent.Invoke(new DictionaryChangedEventArg<TKey>(key, eventType));
        }
    }
}

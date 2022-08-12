namespace AillieoUtils.EasyBindings.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    public class BindableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        internal readonly Event<DictionaryChangedEventArg<TKey>> dictionaryChangedEvent = new Event<DictionaryChangedEventArg<TKey>>();

        private readonly Dictionary<TKey, TValue> source;

        public ICollection<TKey> Keys => this.source.Keys;

        public ICollection<TValue> Values => this.source.Values;

        public int Count => this.source.Count;

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => this.source[key];
            set
            {
                if (this.source.ContainsKey(key))
                {
                    this.source[key] = value;
                    this.NotifyPropertyChanged(key, EventType.Update);
                }
                else
                {
                    this.Add(key, value);
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
            this.source.Add(key, value);
            this.NotifyPropertyChanged(key, EventType.Add);
        }

        public bool ContainsKey(TKey key)
        {
            return this.source.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (this.source.Remove(key))
            {
                this.NotifyPropertyChanged(key, EventType.Remove);
                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.source.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.source).Add(item);
            this.NotifyPropertyChanged(item.Key, EventType.Add);
        }

        public void Clear()
        {
            if (this.source.Count == 0)
            {
                return;
            }

            this.source.Clear();
            this.NotifyPropertyChanged(default, EventType.Clear);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)this.source).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.source).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (((ICollection<KeyValuePair<TKey, TValue>>)this.source).Remove(item))
            {
                this.NotifyPropertyChanged(item.Key, EventType.Remove);
                return true;
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.source).GetEnumerator();
        }

        private void NotifyPropertyChanged(TKey key, EventType eventType)
        {
            this.dictionaryChangedEvent.Invoke(new DictionaryChangedEventArg<TKey>(key, eventType));
        }
    }
}

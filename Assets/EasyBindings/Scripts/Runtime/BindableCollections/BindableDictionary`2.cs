// -----------------------------------------------------------------------
// <copyright file="BindableDictionary`2.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A dictionary that can be bound with callbacks which will be invoked when the dictionary changes.
    /// </summary>
    /// <typeparam name="TKey">Type of key.</typeparam>
    /// <typeparam name="TValue">Type of value.</typeparam>
    public class BindableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        internal readonly EasyDelegate<DictionaryChangedEventArg<TKey>> dictionaryChangedEvent = new EasyDelegate<DictionaryChangedEventArg<TKey>>();

        private static readonly bool valueTypeIsClass = typeof(TValue).IsClass;

        private readonly Dictionary<TKey, TValue> source;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">Initial elements of the dictionary.</param>
        /// <param name="comparer"><see cref="IEqualityComparer{T}"/> for the dictionary.</param>
        public BindableDictionary(IDictionary<TKey, TValue> source, IEqualityComparer<TKey> comparer)
        {
            this.source = new Dictionary<TKey, TValue>(source, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">Initial elements of the dictionary.</param>
        public BindableDictionary(IDictionary<TKey, TValue> source)
        {
            this.source = new Dictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer"><see cref="IEqualityComparer{T}"/> for the dictionary.</param>
        public BindableDictionary(IEqualityComparer<TKey> comparer)
        {
            this.source = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableDictionary{TKey, TValue}"/> class.
        /// </summary>
        public BindableDictionary()
        {
            this.source = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Gets ths event that will be invoked when the dictionary changes.
        /// </summary>
        public EasyEvent<DictionaryChangedEventArg<TKey>> onDictionaryChanged => this.dictionaryChangedEvent;

        /// <inheritdoc/>
        public ICollection<TKey> Keys => this.source.Keys;

        /// <inheritdoc/>
        public ICollection<TValue> Values => this.source.Values;

        /// <inheritdoc/>
        public int Count => this.source.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public TValue this[TKey key]
        {
            get => this.source[key];
            set
            {
                if (this.source.TryGetValue(key, out TValue oldValue))
                {
                    if (valueTypeIsClass && object.ReferenceEquals(oldValue, value))
                    {
                        return;
                    }

                    this.source[key] = value;
                    this.NotifyPropertyChanged(key, EventType.Update);
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        /// <inheritdoc/>
        public void Add(TKey key, TValue value)
        {
            this.source.Add(key, value);
            this.NotifyPropertyChanged(key, EventType.Add);
        }

        /// <inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            return this.source.ContainsKey(key);
        }

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            if (this.source.Remove(key))
            {
                this.NotifyPropertyChanged(key, EventType.Remove);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.source.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.source).Add(item);
            this.NotifyPropertyChanged(item.Key, EventType.Add);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (this.source.Count == 0)
            {
                return;
            }

            this.source.Clear();
            this.NotifyPropertyChanged(default, EventType.Clear);
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)this.source).Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.source).CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (((ICollection<KeyValuePair<TKey, TValue>>)this.source).Remove(item))
            {
                this.NotifyPropertyChanged(item.Key, EventType.Remove);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        /// <inheritdoc/>
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

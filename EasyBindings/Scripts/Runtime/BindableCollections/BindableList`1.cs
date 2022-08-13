// -----------------------------------------------------------------------
// <copyright file="BindableList`1.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A list that can be bound with callbacks which will be invoked when the list changes.
    /// </summary>
    /// <typeparam name="T">Type of element.</typeparam>
    public class BindableList<T> : IList<T>
    {
        internal readonly Event<ListChangedEventArg> listChangedEvent = new Event<ListChangedEventArg>();

        private readonly List<T> source;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableList{T}"/> class.
        /// </summary>
        /// <param name="source">Initial elements of the list.</param>
        public BindableList(IEnumerable<T> source)
        {
            this.source = new List<T>(source);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableList{T}"/> class.
        /// </summary>
        public BindableList()
        {
            this.source = new List<T>();
        }

        /// <inheritdoc/>
        public int Count => this.source.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public T this[int index]
        {
            get => this.source[index];
            set
            {
                this.source[index] = value;
                this.NotifyPropertyChanged(index, EventType.Update);
            }
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            return this.source.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            this.source.Insert(index, item);
            this.NotifyPropertyChanged(index, EventType.Add);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            this.source.RemoveAt(index);
            this.NotifyPropertyChanged(index, EventType.Remove);
        }

        /// <inheritdoc/>
        public void Add(T item)
        {
            this.source.Add(item);
            var index = this.source.Count - 1;
            this.NotifyPropertyChanged(index, EventType.Add);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (this.source.Count == 0)
            {
                return;
            }

            this.source.Clear();
            this.NotifyPropertyChanged(-1, EventType.Clear);
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return this.source.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)this.source).CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            if (index >= 0)
            {
                this.RemoveAt(index);
                this.NotifyPropertyChanged(index, EventType.Remove);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.source).GetEnumerator();
        }

        private void NotifyPropertyChanged(int index, EventType eventType)
        {
            this.listChangedEvent.Invoke(new ListChangedEventArg(index, eventType));
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="BindableSet`1.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A set that can be bound with callbacks which will be invoked when the set changes.
    /// </summary>
    /// <typeparam name="T">Type of element.</typeparam>
    public class BindableSet<T> : ISet<T>
    {
        internal readonly Event<SetChangedEventArg<T>> setChangedEvent = new Event<SetChangedEventArg<T>>();

        private readonly HashSet<T> source;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableSet{T}"/> class.
        /// </summary>
        /// <param name="source">Initial elements of the set.</param>
        /// <param name="comparer"><see cref="IEqualityComparer{T}"/> for the set.</param>
        public BindableSet(IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            this.source = new HashSet<T>(source, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableSet{T}"/> class.
        /// </summary>
        /// <param name="source">Initial elements of the set.</param>
        public BindableSet(IEnumerable<T> source)
        {
            this.source = new HashSet<T>(source);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableSet{T}"/> class.
        /// </summary>
        /// <param name="comparer"><see cref="IEqualityComparer{T}"/> for the set.</param>
        public BindableSet(IEqualityComparer<T> comparer)
        {
            this.source = new HashSet<T>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableSet{T}"/> class.
        /// </summary>
        public BindableSet()
        {
            this.source = new HashSet<T>();
        }

        /// <inheritdoc/>
        public int Count => this.source.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

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
        public bool Add(T item)
        {
            if (this.source.Add(item))
            {
                this.NotifyPropertyChanged(item, EventType.Add);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("SymmetricExceptWith is not supported for a BindableSet.");
        }

        /// <inheritdoc/>
        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("UnionWith is not supported for a BindableSet.");
        }

        /// <inheritdoc/>
        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("ExceptWith is not supported for a BindableSet.");
        }

        /// <inheritdoc/>
        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("IntersectWith is not supported for a BindableSet.");
        }

        /// <inheritdoc/>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.source.IsProperSubsetOf(other);
        }

        /// <inheritdoc/>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.source.IsProperSupersetOf(other);
        }

        /// <inheritdoc/>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.source.IsSubsetOf(other);
        }

        /// <inheritdoc/>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.source.IsSupersetOf(other);
        }

        /// <inheritdoc/>
        public bool Overlaps(IEnumerable<T> other)
        {
            return this.source.Overlaps(other);
        }

        /// <inheritdoc/>
        public bool SetEquals(IEnumerable<T> other)
        {
            return this.source.SetEquals(other);
        }

        /// <inheritdoc/>
        void ICollection<T>.Add(T item)
        {
            this.Add(item);
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
            if (this.source.Remove(item))
            {
                this.NotifyPropertyChanged(item, EventType.Remove);
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

        private void NotifyPropertyChanged(T item, EventType eventType)
        {
            this.setChangedEvent.Invoke(new SetChangedEventArg<T>(item, eventType));
        }
    }
}

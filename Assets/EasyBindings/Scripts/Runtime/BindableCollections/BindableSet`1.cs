using System;
using System.Collections;
using System.Collections.Generic;

namespace AillieoUtils.EasyBindings.Collections
{
    public class BindableSet<T> : ISet<T>
    {
        private readonly HashSet<T> source;
        internal readonly Event<SetChangedEventArg> setChangedEvent = new Event<SetChangedEventArg>();

        public int Count => source.Count;

        public bool IsReadOnly => false;

        public BindableSet(IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            this.source = new HashSet<T>(source, comparer);
        }

        public BindableSet(IEnumerable<T> source)
        {
            this.source = new HashSet<T>(source);
        }

        public BindableSet(IEqualityComparer<T> comparer)
        {
            this.source = new HashSet<T>(comparer);
        }

        public BindableSet()
        {
            this.source = new HashSet<T>();
        }

        public void Clear()
        {
            if (source.Count == 0)
            {
                return;
            }

            source.Clear();
            NotifyPropertyChanged(EventType.Clear);
        }

        public bool Add(T item)
        {
            if (source.Add(item))
            {
                NotifyPropertyChanged(EventType.Add);
                return true;
            }

            return false;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("SymmetricExceptWith is not upported for a BindableSet.");
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("UnionWith is not upported for a BindableSet.");
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("ExceptWith is not upported for a BindableSet.");
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException("IntersectWith is not upported for a BindableSet.");
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return source.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return source.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return source.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return source.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return source.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return source.SetEquals(other);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Contains(T item)
        {
            return source.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)source).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (source.Remove(item))
            {
                NotifyPropertyChanged(EventType.Remove);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)source).GetEnumerator();
        }

        protected void NotifyPropertyChanged(EventType eventType)
        {
            setChangedEvent.Invoke(new SetChangedEventArg(eventType));
        }
    }
}

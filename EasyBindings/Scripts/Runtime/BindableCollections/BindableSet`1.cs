namespace AillieoUtils.EasyBindings.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BindableSet<T> : ISet<T>
    {
        internal readonly Event<SetChangedEventArg> setChangedEvent = new Event<SetChangedEventArg>();

        private readonly HashSet<T> source;

        public int Count => this.source.Count;

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
            if (this.source.Count == 0)
            {
                return;
            }

            this.source.Clear();
            this.NotifyPropertyChanged(EventType.Clear);
        }

        public bool Add(T item)
        {
            if (this.source.Add(item))
            {
                this.NotifyPropertyChanged(EventType.Add);
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
            return this.source.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.source.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.source.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.source.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.source.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return this.source.SetEquals(other);
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public bool Contains(T item)
        {
            return this.source.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)this.source).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (this.source.Remove(item))
            {
                this.NotifyPropertyChanged(EventType.Remove);
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.source).GetEnumerator();
        }

        private void NotifyPropertyChanged(EventType eventType)
        {
            this.setChangedEvent.Invoke(new SetChangedEventArg(eventType));
        }
    }
}

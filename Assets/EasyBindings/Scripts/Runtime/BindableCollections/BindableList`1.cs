using System;
using System.Collections;
using System.Collections.Generic;

namespace AillieoUtils.EasyBindings.Collections
{
    public class BindableList<T> : IList<T>
    {
        private readonly List<T> source;
        internal readonly Event<ListChangedEventArg> listChangedEvent = new Event<ListChangedEventArg>();

        public int Count => source.Count;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => source[index];
            set
            {
                source[index] = value;
                NotifyPropertyChanged(index, EventType.Update);
            }
        }

        public BindableList(IEnumerable<T> source)
        {
            this.source = new List<T>(source);
        }

        public BindableList()
        {
            this.source = new List<T>();
        }

        public int IndexOf(T item)
        {
            return source.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            source.Insert(index, item);
            NotifyPropertyChanged(index, EventType.Add);
        }

        public void RemoveAt(int index)
        {
            source.RemoveAt(index);
            NotifyPropertyChanged(index, EventType.Remove);
        }

        public void Add(T item)
        {
            source.Add(item);
            int index = source.Count - 1;
            NotifyPropertyChanged(index, EventType.Add);
        }

        public void Clear()
        {
            source.Clear();
            NotifyPropertyChanged(-1, EventType.Clear);
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
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                NotifyPropertyChanged(index, EventType.Remove);
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

        protected void NotifyPropertyChanged(int index, EventType eventType)
        {
            listChangedEvent.Invoke(new ListChangedEventArg(index, eventType));
        }
    }
}

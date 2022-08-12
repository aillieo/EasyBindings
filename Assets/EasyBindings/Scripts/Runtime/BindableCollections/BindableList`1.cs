namespace AillieoUtils.EasyBindings.Collections
{
    using System.Collections;
    using System.Collections.Generic;

    public class BindableList<T> : IList<T>
    {
        internal readonly Event<ListChangedEventArg> listChangedEvent = new Event<ListChangedEventArg>();

        private readonly List<T> source;

        public int Count => this.source.Count;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => this.source[index];
            set
            {
                this.source[index] = value;
                this.NotifyPropertyChanged(index, EventType.Update);
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
            return this.source.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.source.Insert(index, item);
            this.NotifyPropertyChanged(index, EventType.Add);
        }

        public void RemoveAt(int index)
        {
            this.source.RemoveAt(index);
            this.NotifyPropertyChanged(index, EventType.Remove);
        }

        public void Add(T item)
        {
            this.source.Add(item);
            var index = this.source.Count - 1;
            this.NotifyPropertyChanged(index, EventType.Add);
        }

        public void Clear()
        {
            if (this.source.Count == 0)
            {
                return;
            }

            this.source.Clear();
            this.NotifyPropertyChanged(-1, EventType.Clear);
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
            var index = this.IndexOf(item);
            if (index >= 0)
            {
                this.RemoveAt(index);
                this.NotifyPropertyChanged(index, EventType.Remove);
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

        private void NotifyPropertyChanged(int index, EventType eventType)
        {
            this.listChangedEvent.Invoke(new ListChangedEventArg(index, eventType));
        }
    }
}

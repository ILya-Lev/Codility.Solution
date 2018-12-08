using System;
using System.Collections;
using System.Collections.Generic;

namespace Codility.Solvers
{
    public class SortedCollection<T> : ICollection<T> where T : IComparable<T>
    {
        private readonly List<T> _storage = new List<T>();
        private readonly IComparer<T> _comparer;

        public SortedCollection(bool inAscendingOrder = true)
        {
            if (inAscendingOrder)
                _comparer = new AscendingComparer();
            else
                _comparer = new DescendingComparer();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        public void Add(T item)
        {
            var index = _storage.BinarySearch(item, _comparer);
            if (index < 0)
                _storage.Insert(~index, item);
            else
                _storage.Insert(index, item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _storage.AddRange(items);
            _storage.Sort(_comparer);
        }

        public void Clear()
        {
            _storage.Clear();
        }

        public bool Contains(T item)
        {
            return _storage.BinarySearch(item, _comparer) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            var removed = false;

            var targetIndex = _storage.BinarySearch(item, _comparer);
            while (targetIndex >= 0)
            {
                _storage.RemoveAt(targetIndex);
                targetIndex = _storage.BinarySearch(item, _comparer);
                removed = true;
            }

            return removed;
        }

        public int Count => _storage.Count;
        public bool IsReadOnly => false;

        public T this[int index] => _storage[index];

        private class AscendingComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (object.ReferenceEquals(x, default(T)))
                    return object.ReferenceEquals(y, default(T)) ? 0 : -1;
                return x.CompareTo(y);
            }
        }
        private class DescendingComparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (object.ReferenceEquals(y, default(T)))
                    return object.ReferenceEquals(x, default(T)) ? 0 : 1;       //?
                return y.CompareTo(x);
            }
        }
    }
}

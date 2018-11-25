using System.Collections.Generic;

namespace Codility.Solvers
{
    /// <summary>
    /// queue based on 2 stacks
    /// </summary>
    public class QueueOnStack<T>
    {
        private readonly Stack<T> _insertion = new Stack<T>();
        private readonly Stack<T> _buffer = new Stack<T>();

        public int Count => _insertion.Count + _buffer.Count;

        public void Enqueue(T item)
        {
            if (_buffer.Count == 0)
            {
                _insertion.Push(item);
                return;
            }

            while (_buffer.Count > 0)
            {
                _insertion.Push(_buffer.Pop());
            }
            _insertion.Push(item);
        }

        public T Dequeue()
        {
            if (_insertion.Count == 0)
            {
                return _buffer.Pop();
            }

            while (_insertion.Count > 1)
            {
                _buffer.Push(_insertion.Pop());
            }
            return _insertion.Pop();
        }
    }
}

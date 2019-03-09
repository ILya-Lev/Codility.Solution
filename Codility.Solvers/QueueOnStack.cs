using System;
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

    /// <summary>
    /// my implementation is 2 times faster. why?
    /// </summary>
    /// <typeparam name="T"></typeparam>

    //public class QueueOnStack<T>
    //{
    //    private readonly DoubleStackQueue<T> _doubleStackQueue;

    //    public QueueOnStack()
    //    {
    //        _doubleStackQueue = new DoubleStackQueue<T>();
    //    }
    //    public int Count => _doubleStackQueue.Count;

    //    public void Enqueue(T item)
    //    {
    //        _doubleStackQueue.Enqueue(item);
    //    }

    //    public T Dequeue()
    //    {
    //        return _doubleStackQueue.Decueue();
    //    }
    //}

    public class DoubleStackQueue<T>
    {
        public DoubleStackQueue()
        {
            PrimaryStorage = new Stack<T>();
            SecondaryStorage = new Stack<T>();
        }
        private Stack<T> PrimaryStorage { get; set; }
        private Stack<T> SecondaryStorage { get; set; }
        public int Count => PrimaryStorage.Count + SecondaryStorage.Count;

        public bool IsEmpty() => PrimaryStorage.IsEmpty() && SecondaryStorage.IsEmpty();

        private T Take(Func<T> storageAccessor)
        {
            if (SecondaryStorage.IsEmpty())
                PrimaryStorage.ReverseTo(SecondaryStorage);

            return storageAccessor.Invoke();
        }
             
        public void Enqueue(T item)
        {
            if (PrimaryStorage.IsEmpty())
                SecondaryStorage.ReverseTo(PrimaryStorage);

            PrimaryStorage.Push(item);
        }

        public T Decueue() => Take(() => SecondaryStorage.Pop());

        public T Peek() => Take(() => SecondaryStorage.Peek());
    }

    public static class StackExtensions
    {
        public static Stack<T> ReverseTo<T>(this Stack<T> source, Stack<T> destination)
        {
            while (!source.IsEmpty())
            {
                destination.Push(source.Pop());
            }

            return destination;
        }

        public static bool IsEmpty<T>(this Stack<T> source) => source.Count == 0;
    }
}

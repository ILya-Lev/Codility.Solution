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

    //public class QueueOnStack0<T>
    //{
    //    /// <summary>
    //    /// Stack for enqueue operations
    //    /// </summary>
    //    protected Stack<T> ForwardStack { get; } = new Stack<T>();

    //    /// <summary>
    //    /// Stack for dequeue operations
    //    /// </summary>
    //    protected Stack<T> BackwardStack { get; } = new Stack<T>();

    //    /// <summary>
    //    /// Fill target stack from source stack.
    //    /// </summary>
    //    /// <param name="from">source stack</param>
    //    /// <param name="to">target stack</param>
    //    protected static void Refill(Stack<T> from, Stack<T> to)
    //    {
    //        while (from.Count > 0)
    //            to.Push(from.Pop());
    //    }

    //    public virtual void Enqueue(T item)
    //    {
    //        if (BackwardStack.Count > 0)
    //            Refill(from: BackwardStack, to: ForwardStack);

    //        ForwardStack.Push(item);
    //    }

    //    public virtual T Dequeue()
    //    {
    //        if (ForwardStack.Count > 0)
    //            Refill(from: ForwardStack, to: BackwardStack);

    //        return BackwardStack.Pop();
    //    }

    //    public virtual T GetHead()
    //    {
    //        if (ForwardStack.Count > 0)
    //            Refill(from: ForwardStack, to: BackwardStack);

    //        return BackwardStack.Peek();
    //    }

    //    public int Count => ForwardStack.Count + BackwardStack.Count;

    //    public bool IsEmpty => Count == 0;
    //}

    //public class QueueOnStack<T> : QueueOnStack0<T>
    //{
    //    /// <summary>
    //    /// Holds head item.
    //    /// </summary>
    //    private T _head;

    //    public override void Enqueue(T item)
    //    {
    //        if (BackwardStack.Count > 0)
    //            Refill(from: BackwardStack, to: ForwardStack);

    //        // if first item set head
    //        if (IsEmpty)
    //            _head = item;

    //        ForwardStack.Push(item);
    //    }

    //    public override T Dequeue()
    //    {
    //        if (ForwardStack.Count > 0)
    //            Refill(from: ForwardStack, to: BackwardStack);

    //        var value = BackwardStack.Pop();

    //        // change head after taking first item
    //        if (!IsEmpty)
    //            _head = BackwardStack.Peek();

    //        return value;
    //    }

    //    /// <summary>
    //    /// Gets stored head.
    //    /// </summary>
    //    /// <exception cref="InvalidOperationException">Throwed when queueu is empty</exception>
    //    public override T GetHead()
    //    {
    //        if (IsEmpty)
    //            throw new InvalidOperationException("Queue is empty.");

    //        return _head;
    //    }
    //}

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

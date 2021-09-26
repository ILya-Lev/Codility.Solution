using System;
using System.Collections.Generic;

namespace Facebook.Problems
{
    public class ReverseOperations
    {
        public class Node
        {
            public int Data { get; }
            public Node? Next { get; set; }

            public Node(int d) => Data = d;
        }

        public static Node FromArray(int[] array)
        {
            Node head = null;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                head = new Node(array[i]) { Next = head };
            }
            return head;
        }

        public static IEnumerable<int> ToSequence(Node head)
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public static Node Reverse(Node head)
        {
            Node current = head, previous = null;

            while (current?.Next != null)
            {
                if (current.Data % 2 != 0)
                {
                    previous = current;
                    current = current.Next;
                    continue;
                }

                var start = previous;

                while (current.Next != null && current.Next.Data % 2 == 0)
                {
                    var tmp = current.Next;
                    current.Next = previous;

                    previous = current;
                    current = tmp;
                }

                var next = current.Next;
                current.Next = previous;

                if (start == null)
                {
                    previous = head;
                    head = current;
                }
                else
                {
                    previous = start.Next;
                    start.Next = current;
                }

                previous.Next = next;
                current = next;
            }

            return head;
        }

        public static Node ReverseNew(Node head)
        {
            Node current = head, previous = null;
            
            while (current != null)
            {
                //move on
                if (current.Data % 2 != 0)
                {
                    previous = current;
                    current = current.Next;
                    continue;
                }

                //collect even part and reverse its internals
                var start = previous;
                var subhead = current;
                while (current.Next != null && current.Next.Data % 2 == 0)
                {
                    var next = current.Next;
                    current.Next = previous;
                    previous = current;
                    current = next;
                }

                //complete reversal/rotation of even part
                var tmp = current.Next;
                current.Next = previous;
                subhead.Next = tmp;
                if (start != null)
                {
                    start.Next = current;
                }
                else
                {
                    head = current;
                }

                current = tmp;
            }

            return head;
        }
    }
}
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
    }
}
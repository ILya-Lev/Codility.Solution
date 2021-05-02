using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public class ListNode //: IEquatable<ListNode>
    {
        public int val;
        public ListNode next;
        public ListNode(int value = 0, ListNode nextNode = null)
        {
            val = value;
            next = nextNode;
        }

        public override bool Equals(object? obj)
        {
            return obj is ListNode node && Equals(node);
        }

        public bool Equals(ListNode other)
        {
            var thisIterator = this;
            var otherIterator = other;
            while (thisIterator != null && otherIterator != null)
            {
                if (thisIterator.val != otherIterator.val)
                    return false;
                thisIterator = thisIterator.next;
                otherIterator = otherIterator.next;
            }

            return thisIterator == null && otherIterator == null;
        }
    }

    public static class ListNodeExtensions
    {
        public static ListNode FromDirectOrder(this IReadOnlyList<int> source)
        {
            var number = new ListNode();
            for (int i = 0; i < source.Count; i++)
            {
                number.val = source[i];
                if (i + 1 == source.Count)
                    return number;

                var previous = new ListNode(nextNode: number);
                number = previous;
            }

            return number;//actually will never rich here
        }
    }

    public class P0002AddTwoNumbers
    {
        public ListNode AddTwoNumbers(ListNode lhs, ListNode rhs)
        {
            var tail = new ListNode();
            var head = tail;
            var nextDigitDelta = 0;
            while (lhs != null && rhs != null)
            {
                var currentSum = lhs.val + rhs.val + nextDigitDelta;

                tail.val = currentSum % 10;
                tail.next = new ListNode();
                tail = tail.next;

                nextDigitDelta = currentSum / 10;
                lhs = lhs.next;
                rhs = rhs.next;
            }

            tail = TraverseRemainder(lhs, ref nextDigitDelta, tail);
            tail = TraverseRemainder(rhs, ref nextDigitDelta, tail);

            if (nextDigitDelta != 0)
                tail.val = nextDigitDelta;
            else
                RemoveTrailingZero(head, tail);

            return head;
        }

        private static ListNode TraverseRemainder(ListNode remainder, ref int nextDigitDelta, ListNode tail)
        {
            if (remainder == null)
                return tail;

            while (remainder != null)
            {
                var currentSum = remainder.val + nextDigitDelta;

                tail.val = currentSum % 10;
                tail.next = new ListNode();
                tail = tail.next;

                nextDigitDelta = currentSum / 10;
                remainder = remainder.next;
            }

            if (nextDigitDelta != 0)
            {
                tail.val = nextDigitDelta;
                nextDigitDelta = 0;
            }

            return tail;
        }

        private void RemoveTrailingZero(ListNode head, ListNode tail)
        {
            if (!head.Equals(tail) && tail.val == 0)
            {
                var current = head;
                while (current.next.next != null)
                {
                    current = current.next;
                }

                current.next = null;
            }
        }
    }
}

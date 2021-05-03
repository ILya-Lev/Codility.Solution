using System.Collections.Generic;

namespace LeetCode.Tasks
{
    public static partial class ListNodeExtensions
    {
        public static ListNode FromSameOrder(this IEnumerable<int> source)
        {
            var head = new ListNode();
            var current = head;
            using var sourceIterator = source.GetEnumerator();
            var containsSomething = sourceIterator.MoveNext();
            while (containsSomething)//enter the loop only if source contains anything
            {
                current.val = sourceIterator.Current;
                //leave the loop as soon as iteration has been completed
                if (!sourceIterator.MoveNext())
                    break;

                current.next = new ListNode();
                current = current.next;
            }

            return head;
        }
    }

    public class P0445AddTwoNumbers
    {
        public ListNode AddTwoNumbers(ListNode lhs, ListNode rhs)
        {
            var first = AsSequence(lhs);
            var second = AsSequence(rhs);

            var reversedSum = new List<int>();
            var firstCounter = first.Count - 1;
            var secondCounter = second.Count - 1;
            var remainder = 0;
            while (firstCounter >= 0 && secondCounter >= 0)
            {
                var currentSum = first[firstCounter--] + second[secondCounter--] + remainder;
                reversedSum.Add(currentSum % 10);
                remainder = currentSum / 10;
            }

            while (firstCounter >= 0)
            {
                var currentSum = first[firstCounter--] + remainder;
                reversedSum.Add(currentSum % 10);
                remainder = currentSum / 10;
            }

            while (secondCounter >= 0)
            {
                var currentSum = second[secondCounter--] + remainder;
                reversedSum.Add(currentSum % 10);
                remainder = currentSum / 10;
            }

            if (remainder != 0)
                reversedSum.Add(remainder);

            var total = new ListNode();
            var current = total;
            for (int i = reversedSum.Count - 1; i >= 0; i--)
            {
                current.val = reversedSum[i];
                if (i == 0)
                    break;
                current.next = new ListNode();
                current = current.next;
            }

            return total;
        }

        private IReadOnlyList<int> AsSequence(ListNode head)
        {
            var digits = new List<int>();
            for (var current = head; current != null; current = current.next)
            {
                digits.Add(current.val);
            }

            return digits;
        }
    }
}

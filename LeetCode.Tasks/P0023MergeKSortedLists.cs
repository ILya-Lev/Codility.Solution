using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Tasks;

public static class P0023MergeKSortedLists
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        public override string ToString()
        {
            var isLast = next is null ? 'L' : ' ';
            return $"{val} {isLast}".Trim();
        }

        public static ListNode FromSequence(IEnumerable<int> sequence)
        {
            var result = new ListNode();
            var current = result;
            ListNode last = null;
            
            foreach (var n in sequence)
            {
                current.val = n;
                current.next = new ListNode();
                last = current;
                current = current.next;
            }

            if (last?.next == current)
                last!.next = null;
            
            return result;
        }

        public static IReadOnlyList<int> ToSequence(ListNode head)
        {
            var result = new List<int>();
            for (var current = head; current != null; current = current.next)
                result.Add(current.val);
            return result;
        }
    }

    public static ListNode MergeKLists(ListNode[] lists)
    {
        if (lists is null || lists.Length == 0)
            return null;

        var result = new ListNode();
        var current = result;
        ListNode last = null;
        var heads = lists.Where(n => n != null).OrderBy(n => n.val).ToArray();//deep copy

        while (heads.Length > 0)
        {
            var minValue = heads[0].val;
            var minAmount = heads.TakeWhile(n => n.val == minValue).Count();

            (current, last) = AdvanceMinHeads(minValue, minAmount, current, last, heads);
            
            //keep heads clean
            heads = heads.Where(n => n != null).ToArray();

            MaintainHeadsSorted(minAmount, heads);
        }

        if (last?.next == current)
            last!.next = null;

        return result;
    }

    private static (ListNode current, ListNode last) AdvanceMinHeads(int minValue, int minAmount
        , ListNode current, ListNode last
        , ListNode[] heads)
    {
        for (int i = 0; i < minAmount; i++)
        {
            current.val = minValue;
            current.next = new ListNode();
            last = current;
            current = current.next;

            heads[i] = heads[i].next;
        }

        return (current, last);
    }

    private static void MaintainHeadsSorted(int minAmount, ListNode[] heads)
    {
        for (int i = minAmount - 1; i >= 0; i--)
        {
            for (int j = i; j+1 < heads.Length && heads[j].val > heads[j+1].val; j++)
            {
                (heads[j], heads[j+1]) = (heads[j+1], heads[j]);
            }
        }
    }
}
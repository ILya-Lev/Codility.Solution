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

    public static ListNode MergeKListsPriority(ListNode[] lists)
    {
        if (lists is null || lists.Count(n => n != null) == 0)
            return null;
        
        var head = new ListNode();
        var current = head;
        ListNode last = null;
        
        var queue = new PriorityQueue<ListNode, int>(lists.Where(n => n != null).Select(n => (n, n.val)));
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            current.val = node.val;
            if (node.next != null)
                queue.Enqueue(node.next, node.next.val);

            last = current;
            current.next = new ListNode();
            current = current.next;
        }

        if (last?.next == current)
            last!.next = null;
            
        return head;
    }


    /// <summary>
    /// as all lists are already sorted, one has to consider only current heads of all lists
    /// sort heads
    /// take top  - it's a min by def, but as values are not unique - traverse until you find next value above min
    /// for each min item advance head by one
    /// remove nulls
    /// maintain heads in the ascending order - invariant of the algorithm
    /// to maintain heads sorted, handle only former top min items,
    /// i.e. there is no need to sort all heads, only ones which were advanced
    /// </summary>
    /// <param name="lists"></param>
    /// <returns></returns>
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
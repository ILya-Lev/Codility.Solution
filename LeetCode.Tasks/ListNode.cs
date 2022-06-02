using System.Collections.Generic;

namespace LeetCode.Tasks;

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

public static partial class ListNodeExtensions
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

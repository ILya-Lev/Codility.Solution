using System.Collections;
using System.Collections.Generic;

namespace LeetCode.Tasks;

public class P0024SwapPairs
{
    public ListNode SwapPairs(ListNode head)
    {
        if (head?.next is null)
            return head;

        var h = head.next;
        var e = head;

        var remainder = h.next;

        h.next = e;
        e.next = SwapPairs(remainder);

        return h;
    }
}
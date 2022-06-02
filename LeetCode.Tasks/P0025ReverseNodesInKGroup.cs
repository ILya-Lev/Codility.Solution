namespace LeetCode.Tasks;

/// <summary>
/// https://leetcode.com/problems/reverse-nodes-in-k-group/submissions/
/// fix end of the group - it's an initial head
/// advance head (h) by swapping one link
/// we break the link between (h) and (n) as we'll create one on the next step
///
/// in the end create a link between current end and next group
///
/// next group could be found out recursively
/// </summary>
public class P0025ReverseNodesInKGroup
{
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head?.next is null || IsShorter(head, k))
            return head;

        ListNode h = head, end = head;
        ListNode n = head.next;
        for (int i = 1; i < k && n != null; i++)
        {
            //swap one link
            var tmp = h;
            h = n;
            n = n.next;
            h.next = tmp;
        }

        end.next = ReverseKGroup(n, k);

        return h;
    }

    private bool IsShorter(ListNode head, int k)
    {
        var length = 0;
        for (var c = head; c != null; c = c.next)
        {
            if (++length >= k)
                return false;
        }
        return true;
    }
}
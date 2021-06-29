namespace LeetCode.Tasks
{

    public class P0234PalindromeLinkedList
    {
        //1. find length
        //2. find sum of lhs half and rhs half (if not equal - not a palindrome)
        //3. find sum of ai - ai_+1 up to the middle, handle even/odd length cases
        //if lhs sign fluctuating does not match rhs sign fluctuating - not a palindrome...

        public bool IsPalindrome(ListNode head)
        {
            if (head is null) return false;
            (int length, int total) = GoThroughFirstTime(head);

            int lhsMiddle = length / 2;
            int rhsMiddle = (length + 1) / 2;
            int lhsTotal = 0, rhsTotal = 0, lhsFluct = 0, rhsFluct = 0;

            var index = 0;
            var sign = 1;
            var current = head;
            for (; index < lhsMiddle; current = current.next, index++)
            {
                lhsTotal += current.val;

                sign *= -1;//sign should be mirrored => here changed before usage
                lhsFluct += sign * current.val;
            }

            if (lhsMiddle != rhsMiddle)//ship middle item in odd numbered list
            {
                current = current.next;
            }

            for (; current is not null; current = current.next)
            {
                rhsTotal += current.val;

                rhsFluct += sign * current.val;
                sign *= -1;//sign should be mirrored => here changed after usage
            }


            return lhsTotal == rhsTotal && lhsFluct == rhsFluct;
        }


        private (int length, int total) GoThroughFirstTime(ListNode head)
        {
            int length = 0, total = 0;
            for (var current = head; current is not null; current = current.next)
            {
                length++;
                total += current.val;
            }
            return (length, total);
        }
    }
}

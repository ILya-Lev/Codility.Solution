using FluentAssertions;
using LeetCode.Tasks;
using Xunit;

namespace LeetCode.Tests;

[Trait("Category", "Unit")]
public class P0023MergeKSortedListsTests
{
    [Fact]
    public void FromSequence_ThreeNumbers_CorrectOrder()
    {
        var sequence = new[] { 1, 2, 3 };
        var head = ListNode.FromSequence(sequence);
        var resurrected = ListNode.ToSequence(head);
        resurrected.Should().BeEquivalentTo(sequence);
    }

    [Fact]
    public void MergeKList_Sample1_Ascending()
    {
        var lists = new[]
        {
            ListNode.FromSequence(new[] { 1, 4, 5 }),
            ListNode.FromSequence(new[] { 1, 3, 4 }),
            ListNode.FromSequence(new[] { 2, 6 }),
        };

        var result = P0023MergeKSortedLists.MergeKListsPriority(lists);

        var sequence = ListNode.ToSequence(result);

        sequence.Should().BeEquivalentTo(new[]{1,1,2,3,4,4,5,6});
        sequence.Should().BeInAscendingOrder();
    }

    [Fact]
    public void MergeKList_Sample2_Ascending()
    {
        var lists = new[]
        {
            ListNode.FromSequence(new[] { -1, 1}),
            ListNode.FromSequence(new[] { -3, 1, 4 }),
            ListNode.FromSequence(new[] { -2, -1, 0, 2 }),
        };

        var result = P0023MergeKSortedLists.MergeKListsPriority(lists);

        var sequence = ListNode.ToSequence(result);

        sequence.Should().BeInAscendingOrder();
    }
}
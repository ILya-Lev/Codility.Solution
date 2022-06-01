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
        var head = P0023MergeKSortedLists.ListNode.FromSequence(sequence);
        var resurrected = P0023MergeKSortedLists.ListNode.ToSequence(head);
        resurrected.Should().BeEquivalentTo(sequence);
    }

    [Fact]
    public void MergeKList_Sample1_Ascending()
    {
        var lists = new[]
        {
            P0023MergeKSortedLists.ListNode.FromSequence(new[] { 1, 4, 5 }),
            P0023MergeKSortedLists.ListNode.FromSequence(new[] { 1, 3, 4 }),
            P0023MergeKSortedLists.ListNode.FromSequence(new[] { 2, 6 }),
        };

        var result = P0023MergeKSortedLists.MergeKListsPriority(lists);

        var sequence = P0023MergeKSortedLists.ListNode.ToSequence(result);

        sequence.Should().BeEquivalentTo(new[]{1,1,2,3,4,4,5,6});
        sequence.Should().BeInAscendingOrder();
    }

    [Fact]
    public void MergeKList_Sample2_Ascending()
    {
        var lists = new[]
        {
            P0023MergeKSortedLists.ListNode.FromSequence(new[] { -1, 1}),
            P0023MergeKSortedLists.ListNode.FromSequence(new[] { -3, 1, 4 }),
            P0023MergeKSortedLists.ListNode.FromSequence(new[] { -2, -1, 0, 2 }),
        };

        var result = P0023MergeKSortedLists.MergeKListsPriority(lists);

        var sequence = P0023MergeKSortedLists.ListNode.ToSequence(result);

        sequence.Should().BeInAscendingOrder();
    }
}
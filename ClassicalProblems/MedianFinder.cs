namespace ClassicalProblems;

public static class MedianFinder
{
    public static async IAsyncEnumerable<decimal> GetMedianPrices(IAsyncEnumerable<decimal> rawPrices)
    {
        PriorityQueue<decimal, decimal> headMax = new(new DescendingComparer<decimal>()),
                                        tailMin = new(new AscendingComparer<decimal>());
        await foreach (var price in rawPrices)
        {
            if (headMax.Count == 0 || headMax.Peek() >= price)
            {
                headMax.Enqueue(price, price);
                MaintainCountEquilibrium(headMax, tailMin);
                yield return CalculateMedian(headMax, tailMin);
            }
            else
            {
                tailMin.Enqueue(price, price);
                MaintainCountEquilibrium(headMax, tailMin);
                yield return CalculateMedian(tailMin, headMax);
            }
        }
    }

    private static decimal CalculateMedian(PriorityQueue<decimal, decimal> lead, PriorityQueue<decimal, decimal> follow)
        => lead.Count == follow.Count
         ? (lead.Peek() + follow.Peek()) / 2
         : lead.Peek();

    private static void MaintainCountEquilibrium(PriorityQueue<decimal, decimal> headMax, PriorityQueue<decimal, decimal> tailMin)
    {
        if (headMax.Count > tailMin.Count + 1)
        {
            var headTop = headMax.Dequeue();
            tailMin.Enqueue(headTop, headTop);
        }
        else if (headMax.Count + 1 < tailMin.Count)
        {
            var tailBottom = tailMin.Dequeue();
            headMax.Enqueue(tailBottom, tailBottom);
        }
    }
}

public class DescendingComparer<T> : IComparer<T> where T : notnull, IComparable<T>
{
    public int Compare(T lhs, T rhs) => rhs.CompareTo(lhs);
}
public class AscendingComparer<T> : IComparer<T> where T : notnull, IComparable<T>
{
    public int Compare(T lhs, T rhs) => lhs.CompareTo(rhs);
}
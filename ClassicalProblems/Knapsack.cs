namespace ClassicalProblems;

public class Knapsack
{
    public class Item
    {
        public decimal Cost { get; set; }
        public decimal Weight { get; set; }
    }

    public IReadOnlyList<Item> FillInNaive(IEnumerable<Item?> stuff, decimal capacity)
    {
        var valuable = stuff.Where(i => i?.Weight > 0)
            .OrderByDescending(i => i!.Cost / i.Weight)
            .ThenByDescending(i => i!.Cost)//if the ratio is the same put smaller item first
            .ToArray();

        var content = new List<Item>();
        var room = capacity;
        var currentCost = 0m;

        foreach (var item in valuable)
        {
            if (room >= item!.Weight)
            {
                content.Add(item);
             
                currentCost += item.Cost;
                room -= item.Weight;
            }
        }

        return content;
    }

    /// <summary>
    /// dynamic programming O(N*C), where N amount of items, C total capacity
    /// </summary>
    /// <remarks>
    /// http://rosettacode.org/wiki/Knapsack_problem/0-1#Dynamic_programming_solution
    /// </remarks>
    public IReadOnlyList<Item> FillIn(IEnumerable<Item> stuff, decimal capacity)
    {
        var items = stuff.ToArray();
        var bestSolutionSoFar = new decimal[items.Length + 1, (int)capacity + 1];
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            for (int currentCapacity = 1; currentCapacity <= capacity; currentCapacity++)
            {
                var previousItemCost = bestSolutionSoFar[i, currentCapacity];
                if (currentCapacity >= item.Weight) //the item fits into the knapsack
                {
                    //lookup another possible lest solution
                    var costOfRemovingWeightForItem = bestSolutionSoFar[i, currentCapacity - (int)item.Weight];
                    bestSolutionSoFar[i + 1, currentCapacity] = Math.Max(costOfRemovingWeightForItem + item.Cost, previousItemCost);
                }
                else//there is no room for the given item
                {
                    bestSolutionSoFar[i + 1, currentCapacity] = previousItemCost;
                }
            }
        }

        var content = new List<Item>();
        var room = (int)capacity;
        for (int i = items.Length; i > 0; i--)
        {
            if (bestSolutionSoFar[i - 1, room] != bestSolutionSoFar[i, room]) //does the item belong to the best possible solution
            {
                content.Add(items[i-1]);
                room -= (int)items[i - 1].Weight;
            }
        }
        return content;
    }
}
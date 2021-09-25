using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Problems
{
    public class QueueRemovals
    {
        public static int[] FindPositions(int[] arr, int x)
        {
            var initialPositions = new int[x];

            //time: O(x*x = n) and space as well
            var queue = new Queue<(int value, int index)>(arr.Select((n, idx) => (n, idx)));

            //time O(x*x)
            for (int i = 0; i < x; i++)
            {
                var itemsToPop = Math.Min(x, queue.Count);
                var buffer = new (int value, int index)[itemsToPop];
                var maxValue = -1;

                for (int j = 0; j < itemsToPop; j++)
                {
                    var currentItem = queue.Dequeue();
                    maxValue = Math.Max(maxValue, currentItem.value);
                    buffer[j] = currentItem;
                }

                for (int j = 0; j < itemsToPop; j++)
                {
                    if (buffer[j].value == maxValue && initialPositions[i] == 0)
                    {
                        initialPositions[i] = buffer[j].index + 1;//convert 0-based into 1-based index
                    }
                    else
                    {
                        var decreasedValue = Math.Max(0, buffer[j].value - 1);
                        queue.Enqueue((decreasedValue, buffer[j].index));
                    }
                }
            }

            return initialPositions;
        }
    }
}
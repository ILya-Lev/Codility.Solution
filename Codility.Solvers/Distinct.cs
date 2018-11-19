using System.Collections.Generic;

namespace Codility.Solvers
{
    public class Distinct
    {
        public int GetAmount(int[] numbers)             //n
        {
            var alreadySeen = new HashSet<int>();
            foreach (var number in numbers)             //O(n)
            {
                if (!alreadySeen.Contains(number))      //O(1) as there is no collisions by def
                    alreadySeen.Add(number);            //O(1) as there is no collisions by def
            }

            return alreadySeen.Count;                   //O(1)
        }
    }
}

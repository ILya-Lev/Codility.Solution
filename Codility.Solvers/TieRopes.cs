using System;
using System.Collections.Generic;
using System.Text;

namespace Codility.Solvers
{
    public class TieRopes
    {
        public int GetRopeNumber(int limit, int[] ropes)
        {
            var currentSize = 0;
            var counter = 0;
            foreach (var rope in ropes)
            {
                if (currentSize < limit)
                {
                    currentSize += rope;
                }
                else
                {
                    counter++;
                    currentSize = rope;
                }
            }

            if (currentSize >= limit) 
                counter++;

            return counter;
        }
    }
}

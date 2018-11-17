using System;

namespace Codility.Solvers
{
    public class MinAvgTwoSlice
    {
        public int MinAverageSlice(int[] input)
        {
            var total = (input[0]+input[1])/2.0;
            var sliceStart = 0;
            for (int i = 1; i < input.Length-1; i++)
            {
                for (int j = 2; j < input.Length; j++)
                {
                    
                }
            }

            return 0;
        }
    }
}

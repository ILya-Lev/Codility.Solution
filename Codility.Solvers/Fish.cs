using System.Collections.Generic;

namespace Codility.Solvers
{
    public class Fish
    {
        public int SurvivalAmount(int[] size, int[] direction)
        {
            var goingUpSurvived = 0;
            var goingDown = new Stack<int>();
            for (int fishIndex = 0; fishIndex < direction.Length; fishIndex++)
            {
                if (direction[fishIndex] == 1)
                {
                    goingDown.Push(size[fishIndex]);
                    continue;
                }

                if (WillGoingUpSurvive(goingDown, size[fishIndex]))
                {
                    goingUpSurvived++;
                }
            }

            return goingUpSurvived + goingDown.Count;
        }

        private bool WillGoingUpSurvive(Stack<int> goingDown, int goingUpSize)
        {
            while (goingDown.Count > 0 && goingDown.Peek() < goingUpSize)
            {
                goingDown.Pop();
            }

            return goingDown.Count == 0;
        }
    }
}

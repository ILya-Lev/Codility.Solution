using System;

namespace Codility.Solvers
{
    public class MinPerimeterRectangle
    {
        public int GetMinPerimeter(int area)
        {
            var maxDivider = (int) Math.Ceiling(Math.Sqrt(area));
            for (int divider = maxDivider; divider >= 1; divider--)
            {
                if (area % divider == 0)
                    return 2 * (divider + area / divider);
            }

            return -1;
        }
    }
}

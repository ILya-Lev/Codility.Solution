using System;
using System.Collections.Generic;

namespace Codility.Solvers
{
    public class AbsDistinct
    {
        /// <summary>
        /// input should be non-decreasingly sorted
        /// </summary>
        public int Amount(int[] input)
        {
            if (input.Length == 0) return 0;
            var set = new HashSet<long>();
            foreach (var value in input)
            {
                var absValue = Math.Abs((long)value);
                set.Add(absValue);
            }

            return set.Count;
        }
        //public int Amount(int[] input)
        //{
        //    if (input.Length == 0) return 0;

        //    var allPositiveDescending = MakePositiveOnly(input).ToArray();
        //    var uniqueCount = GetNumberOfUniqueElements(allPositiveDescending);
        //    return uniqueCount;
        //}

        //private IEnumerable<long> MakePositiveOnly(int[] input)
        //{
        //    int ending = input.Length - 1;
        //    int starting = 0;
        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        var head = input[i];
        //        if (head < 0)
        //        {
        //            var absHead = -(long)head;
        //            for (; ending > i && input[ending] > absHead; ending--)
        //            {
        //                yield return input[ending];
        //            }

        //            yield return absHead;
        //        }
        //        else
        //        {
        //            starting = i;
        //            break;
        //        }
        //    }

        //    for (int i = ending; i >= starting; i--)
        //    {
        //        yield return Math.Abs((long)input[i]);
        //    }
        //}

        //private int GetNumberOfUniqueElements(long[] allPositiveDescending)
        //{
        //    var previous = allPositiveDescending[0];
        //    var counter = 1;
        //    foreach (var value in allPositiveDescending)
        //    {
        //        if (value != previous)
        //        {
        //            previous = value;
        //            counter++;
        //        }
        //    }

        //    return counter;
        //}
    }
}

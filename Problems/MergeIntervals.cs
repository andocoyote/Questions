using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Problems
{
    internal class Problem9
    {
        /*
         * Given a collection of intervals, merge all overlapping intervals.
         * Input: [[1,3],[2,6],[8,10],[15,18]]
         * Output: [[1,6],[8,10],[15,18]]
         * Explanation: Since intervals [1,3] and [2,6] overlap, they should be merged into [1,6].
         * Assume the intervals are sorted in ascending order
        */
        public static void MergeIntervalsTest()
        {
            int[,] intervals = { { 1, 3 }, { 2, 6 }, { 8, 10 }, { 15, 18 } };

            int[][] mergedIntervals = MergeIntervals(intervals);

            Console.WriteLine($"mergedIntervals size: {mergedIntervals.Length}");

            for (int i = 0; i < mergedIntervals.Length; i++)
            {
                Console.WriteLine($"[{mergedIntervals[i][0]}, {mergedIntervals[i][1]}]");
            }
        }

        public static int[][] MergeIntervals(int[,] intervals)
        {
            List<int[]> list = new List<int[]>();

            for (int i = 0; i < intervals.GetLength(0)-1; i++)
            {
                // Two intervals overlap if: i[0,1] >= i[1,0]
                if (intervals[i, 1] >= intervals[i+1, 0])
                {
                    Console.WriteLine($"[{intervals[i, 0]},{intervals[i, 1]}] and [{intervals[i+1, 0]},{intervals[i+1, 1]}] overlap");
                    list.Add(new int[2] { intervals[i, 0], intervals[i+1, 1] });

                }
                else
                {
                    Console.WriteLine($"[{intervals[i, 0]},{intervals[i, 1]}] and [{intervals[i+1, 0]},{intervals[i+1, 1]}] don't overlap");
                    list.Add(new int[2] { intervals[i, 0], intervals[i, 1] });
                }
            }

            return list.ToArray();
        }

        /*public static int[][] EvaluateIntervals(int[,] interval1, int[,] interval2)
        {
            // Two intervals overlap if: i[0,1] >= i[1,0]
            if (intervals[i, 1] >= intervals[i + 1, 0])
            {
                Console.WriteLine($"[{intervals[i, 0]},{intervals[i, 1]}] and [{intervals[i + 1, 0]},{intervals[i + 1, 1]}] overlap");
                list.Add(new int[2] { intervals[i, 0], intervals[i + 1, 1] });

            }
            else
            {
                Console.WriteLine($"[{intervals[i, 0]},{intervals[i, 1]}] and [{intervals[i + 1, 0]},{intervals[i + 1, 1]}] don't overlap");
                list.Add(new int[2] { intervals[i, 0], intervals[i, 1] });
            }
        }*/
    }
}

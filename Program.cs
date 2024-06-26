﻿using Practice.Problems;

namespace Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Action> functions = new List<Action>
            {
                Problem1.PalindromeDescendantTest,
                Problem2.TranslateEnglishToPigLatinTest,
                Problem3.FindPrimeNumberTest,
                Problem4.AllergiesTest,
                Problem5.ThreeSumProblemTest,
                Problem6.PalindromeTest,
                Problem7.FileAnalyzerTest,
                Problem8.DatabaseConnectionPoolingTest,
                Problem9.MergeIntervalsTest,
                Problem10.CoinChangeTest,
                Problem11.FibonacciTest,
                Problem12.FactorialTest,
                Problem13.BinarySearchTreeTest
            };

            int selection = -1;

            while (true)
            {
                try
                {
                    // Run the test specified by the user on the command line
                    Console.Write("Enter the index of the practice problem to run: ");

                    int.TryParse(Console.ReadLine(), out selection);

                    if (selection == 0)
                        break;

                    if (selection <= functions.Count)
                    {
                        functions.ElementAt(selection - 1)();
                    }

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
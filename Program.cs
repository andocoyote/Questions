using Practice.Problems;
using System.Runtime.CompilerServices;

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
                Problem3.FindPrimeNumberTest
            };

            int selection = -1;

            while (true)
            {
                try
                {
                    Console.Write("Enter the index of the practice problem to run: ");

                    int.TryParse(Console.ReadLine(), out selection);

                    if (selection == 0)
                        break;

                    functions.ElementAt(selection-1)();

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
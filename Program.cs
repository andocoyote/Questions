using Practice.Problems;

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
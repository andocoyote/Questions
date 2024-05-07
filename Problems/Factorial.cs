namespace Practice.Problems
{
    internal class Problem12
    {
        public static void FactorialTest()
        {
            int degree = 5;

            int factor = Factorial(degree);

            Console.WriteLine($"Factorial to degree {degree} is {factor} (recursive)");
        }

        public static int Factorial(int degree)
        {
            if (degree == 0 || degree == 1)
            {
                return 1;
            }

            // !n = 1 * 2 * 3 ... * n
            // !5 = 1 * 2 * 3 * 4 * 5 = 120
            return degree * Factorial(degree - 1);
        }
    }
}

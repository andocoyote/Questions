namespace Practice.Problems
{
    internal class Problem11
    {
        // 0, 1, 1, 2, 3, 5, 8, 13, 21 ...
        public static void FibonacciTest()
        {
            int degree = 8;

            int fib1 = FibonacciRecursive(degree);

            Console.WriteLine($"Fibonacci to degree {degree} is {fib1} (recursive)");

            int fib2 = FibonacciRegular(degree);

            Console.WriteLine($"Fibonacci to degree {degree} is {fib2} (non-recursive)");
        }

        public static int FibonacciRecursive(int degree)
        {
            if (degree == 0 || degree == 1)
            {
                return degree;
            }

            return FibonacciRecursive(degree - 1) + FibonacciRecursive(degree - 2);
        }

        public static int FibonacciRegular(int degree)
        {
            int a = 0;
            int b = 1;
            int fib = 0;

            if (degree == 0)
            {
                return a;
            }
            else if (degree == 1)
            {
                return b;
            }

            for (int i = 2; i <= degree; i++)
            {
                fib = a + b;
                a = b;
                b = fib;
            }

            return fib;
        }
    }
}

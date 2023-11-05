namespace Practice.Problems
{
    internal class Problem3
    {
        public static void FindPrimeNumberTest()
        {
            int primeNum1 = FindPrimeNumber(1);             // Should be 2
            Console.WriteLine($"The 1st prime number is {primeNum1}");

            int primeNum5 = FindPrimeNumber(5);             // Should be 11
            Console.WriteLine($"The 5th prime number is {primeNum5}");

            int primeNum9 = FindPrimeNumber(9);             // Should be 23
            Console.WriteLine($"The 9th prime number is {primeNum9}");

            int primeNum1000 = FindPrimeNumber(1000);       // Should be 7919
            Console.WriteLine($"The 1000th prime number is {primeNum1000}");

            int primeNum100000 = FindPrimeNumber(100000); // Should be 1299709
            Console.WriteLine($"The 100,000th prime number is {primeNum100000}");
        }

        // Given a number n, find the nth prime number
        public static int FindPrimeNumber(int nthprime)
        {
            bool isPrime = false;
            int primeCount = 0;
            int curNum = 2;
            
            // Find the nth prime number
            while(true)
            {
                // Determine if a number is prime
                isPrime = IsNumberPrime(curNum);

                if(isPrime)
                {
                    Console.WriteLine($"Found prime number #{primeCount} and it's {curNum}");
                    primeCount++;
                }

                if(primeCount == nthprime)
                {
                    break;
                }

                curNum++;
            }

            // Return the nth prime number
            return curNum;
        }

        private static bool IsNumberPrime(int num)
        {
            bool isPrime = true;
            int curModulus = 2;

            // Only need to check the modulo if it's half the prime candidate
            while (curModulus <= Math.Floor((float)num/2))
            {
                if (num % curModulus == 0)
                {
                    isPrime = false;
                    break;
                }

                curModulus++;
            }

            return isPrime;
        }
    }
}

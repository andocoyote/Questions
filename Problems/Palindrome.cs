namespace Practice.Problems
{
    internal static class Problem6
    {
        private static List<int> ints = new List<int>()
        {
            11211211, 13001120, 23336014, 11
        };

        public static void PalindromeTest()
        {
            foreach (int i in ints)
            {
                CheckPalindrome(i);
            }
        }

        public static bool CheckPalindrome(int num)
        {
            bool isPalindrome = true;
            string numString = num.ToString();
            int startIndex = 0;
            int endIndex = numString.Length - 1;

            // Determine if this is a palindrome by comparing corresponding numbers
            while (startIndex < endIndex)
            {
                Console.WriteLine($"\tComparing {numString[startIndex]} and {numString[endIndex]}:");

                if (numString[startIndex] != numString[endIndex])
                {
                    isPalindrome = false;
                    break;
                }

                startIndex++;
                endIndex--;
            }

            Console.WriteLine($"Number is{(isPalindrome ? "" : " not")} a palindrome.");

            return isPalindrome;
        }
    }
}

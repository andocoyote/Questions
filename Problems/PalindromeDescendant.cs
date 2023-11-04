using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Problems
{
    internal static class Problem1
    {
        public static void PalindromeDescendantTest()
        {
            List<int> ints = new List<int>()
            {
                11211230, 13001120, 23336014, 11
            };

            foreach (int i in ints)
            {
                PalindromeDescendant(i);
            }
        }

        /*
        Palindrome Descendant:
        A number may not be a palindrome, but its descendant can be.
        A number's direct child is created by summing each pair of adjacent digits to create the digits of the next number.
        For instance, 123312 is not a palindrome, but its next child 363 is, where: 3 = 1 + 2; 6 = 3 + 3; 3 = 1 + 2.
        Create a function that returns true if the number itself is a palindrome or any of its descendants
        down to the first 2 digit number (a 1-digit number is trivially a palindrome).

        Examples
        PalindromeDescendant(11211230) ➞ false
        // 11211230 ➞ 2333 ➞ 56

        palindromeDescendant(13001120) ➞ true
        // 13001120 ➞ 4022 ➞ 44

        PalindromeDescendant(23336014) ➞ true
        // 23336014 ➞ 5665

        PalindromeDescendant(11) ➞ true
        // Number itself is a palindrome.
        Notes
        Numbers will always have an even number of digits.
        */
        public static bool PalindromeDescendant(int num)
        {
            bool isPalindrome = false;

            // Get the length of the number
            string numString = num.ToString();
            int len = numString.Length;

            // Convert num to an array of ints
            int[] numArray = new int[len];

            Console.Write("Original number: ");
            for (int i = 0; i < len; i++)
            {
                numArray[i] = int.Parse(numString[i].ToString());
                Console.Write(numArray[i]);
            }
            Console.WriteLine();

            // Search for palindromes
            isPalindrome = EvaluatePalindrome(numArray);
            Console.WriteLine($"Overall result: A palindrome was{(isPalindrome ? "" : " not")} found.");

            return isPalindrome;
        }

        private static bool EvaluatePalindrome(int[] numArray)
        {
            bool isPalindrome = true;
            int startIndex = 0;
            int addedIndex = 0;
            int[] newNumArray = numArray;

            // If we have more than 2 digits in the number, add them in groups of two to make the new number
            if (numArray.Length / 2 >= 2)
            {
                newNumArray = new int[numArray.Length / 2];

                // Add numbers in groups of two to create the descendant palindrome
                while (startIndex + 1 < numArray.Length)
                {
                    newNumArray[addedIndex] = numArray[startIndex] + numArray[startIndex + 1];
                    addedIndex++;
                    startIndex += 2;
                }

                Console.Write("Descendant number: ");
                foreach (int i in newNumArray)
                {
                    Console.Write(i);
                }
                Console.WriteLine();
            }

            // Determine if this is a palindrome by comparing corresponding numbers
            startIndex = 0;
            int endIndex = newNumArray.Length - 1;

            while (startIndex < endIndex)
            {
                Console.WriteLine($"\tComparing {newNumArray[startIndex]} and {newNumArray[endIndex]}:");

                if (newNumArray[startIndex] != newNumArray[endIndex])
                {
                    isPalindrome = false;
                    break;
                }

                startIndex++;
                endIndex--;
            }

            Console.WriteLine($"Descendant is{(isPalindrome ? "" : " not")} a palindrome.");

            // If we've found a palindrome, return true.  Else, keep looking
            if (newNumArray.Length / 2 > 1)
            {
                return isPalindrome || EvaluatePalindrome(newNumArray);
            }
            // Stopping case: the number we're going to create is less than 2 digits
            else
            {
                return isPalindrome;
            }
        }
    }
}

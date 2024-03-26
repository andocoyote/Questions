namespace Practice.Problems
{
    internal class Problem7
    {
        private static string _filename = @"Data\WordFile.txt";

        /*
        Count and display the total number of words in the file.
        Count and display the total number of lines in the file.
        Identify and display the longest word in the file.
        Identify and display the frequency of each word in the file.
        Output: Present the analysis results in a clear and well-formatted manner.
        */
        public static void FileAnalyzerTest()
        {
            string? line = string.Empty;
            string? longestWord = string.Empty;
            List<string>? tokens = null;
            SortedDictionary<string, int> wordFrequencyDict = new();
            int wordCount = 0;
            int lineCount = 0;
            char[] punctuation = {'.', ',', '?', '!', '-', ':', ' '};

            using (StreamReader? reader = new StreamReader(_filename))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    tokens = line.Split(' ').ToList();

                    // Trim all punctuation chars from the start and end of each word
                    tokens = tokens.Select(word => new string(word.Trim(punctuation))).ToList();

                    // Remove empty strings
                    tokens.Remove("");

                    // Make all words lowercase so "The" and "the" are treated identically
                    tokens = tokens.Select(word => new string(word.ToLower())).ToList();

                    // Count the total number of words
                    wordCount += tokens?.Count ?? 0;

                    // Count the total number of lines
                    lineCount++;

                    foreach(string word in tokens ?? Enumerable.Empty<string>())
                    {
                        // Set the longest word so far
                        if (longestWord.Length < word.Length)
                        {
                            longestWord = word;
                        }

                        // Set the frequency of each word
                        if (wordFrequencyDict.ContainsKey(word))
                        {
                            wordFrequencyDict[word]++;
                        }
                        else
                        {
                            wordFrequencyDict.Add(word, 1);
                        }
                    }
                } // End of while(...)
            } // End of using(...)

            List<string>? longestWords = tokens?.Where(word => word.Length == longestWord.Length).ToList();

            Console.WriteLine($"Word count: {wordCount}");
            Console.WriteLine($"Line count: {lineCount}");

            Console.WriteLine($"Longest word(s): {longestWord}");
            longestWords?.ForEach(word => Console.WriteLine($"\t{word}"));

            Console.WriteLine("Frequency per word:");
            foreach (string word in wordFrequencyDict.Keys)
            {
                Console.WriteLine($"\t{word}:{wordFrequencyDict[word]}");
            }
        }
    }
}

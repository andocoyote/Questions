using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Problems
{
    internal static class Problem2
    {
        private readonly static char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        private readonly static char[] punctuation = { ' ', ';', ',', '.', '?' };

        public static void TranslateEnglishToPigLatinTest()
        {
            List<string> sentences = new List<string>()
            {
                "trebuchet", "apple", "I like to eat honey waffles.", "Do you think it is going to rain today?"
            };

            foreach (string s in sentences)
            {
                TranslateEnglishToPigLatin(s);
            }
        }

        /*
        English to Pig Latin Translator
        Pig latin has two very simple rules:

        1. If a word starts with a consonant move the first letter(s) of the word, till you reach a vowel, to the end of the word and add "ay" to the end.
        have ➞ avehay
        cram ➞ amcray
        take ➞ aketay
        cat ➞ atcay
        shrimp ➞ impshray
        trebuchet ➞ ebuchettray

        2. If a word starts with a vowel add "yay" to the end of the word.
        ate ➞ ateyay
        apple ➞ appleyay
        oaken ➞ oakenyay
        eagle ➞ eagleyay

        Write two functions to make an English to Pig Latin translator.
        The first function TranslateWord(word) takes a single word and returns that word translated into pig latin.
        The second function TranslateSentence(sentence) takes an English sentence and returns that sentence translated into pig latin.

        Examples
        TranslateWord("flag") ➞ "agflay"
        TranslateWord("Apple") ➞ "Appleyay"
        TranslateWord("button") ➞ "uttonbay"
        TranslateWord("") ➞ ""
        TranslateSentence("I like to eat honey waffles.") ➞ "Iyay ikelay otay eatyay oneyhay afflesway."
        TranslateSentence("Do you think it is going to rain today?") ➞ "Oday ouyay inkthay ityay isyay oinggay otay ainray odaytay?"

        Notes
        Regular expressions will help you not mess up the punctuation in the sentence.
        If the original word or sentence starts with a capital letter, the translation should preserve its case (see examples #2, #5 and #6).
        */
        public static void TranslateEnglishToPigLatin(string sentence)
        {
            string translatedSentence = TranslateSentence(sentence);

            Console.WriteLine($"Original sentence: {sentence}");
            Console.WriteLine($"Translated sentence: {translatedSentence}");
        }

        private static string TranslateSentence(string sentence)
        {
            string translatedSentence = string.Empty;

            string[] words = sentence.Split(punctuation);

            foreach (string word in words)
            {
                translatedSentence += " " + TranslateWord(word);
            }

            return translatedSentence;
        }

        private static string TranslateWord(string word)
        {
            string translatedWord = string.Empty;

            // Obtain the index of the first vowel
            int firstVowel = word.ToLower().IndexOfAny(vowels);

            if (firstVowel > 0)
            {
                // Remove up to and including the first vowel
                string coreString = word.Remove(0, firstVowel);

                translatedWord = coreString + word.Substring(0, firstVowel) + "ay";
            }
            else if (firstVowel == 0)
            {
                translatedWord = word;
                translatedWord += "yay";
            }

            return translatedWord;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Used for generic string manipulations
    /// </summary>
    public static class StringManipulations
    {
        #region Parts
        /// <summary>
        /// Used to replace word in string while keeping original case structure
        /// </summary>
        private static KeepCaseReplacer keepCaseReplacer = new KeepCaseReplacer();

        /// <summary>
        /// Manages replace of word sequences
        /// </summary>
        private static WordSequenceReplacer wordSequenceReplacer;

        /// <summary>
        /// Used to remove words from strings
        /// </summary>
        private static WordRemover wordRemover = new WordRemover();

        /// <summary>
        /// List of possible word delimiters
        /// </summary>
        private static readonly char[] wordDelimiterList = { ' ', '\n', '\t', '\r', '.', ',', ')', '(', '"', '{', '}', '[', ']', ':', ';', '?', '%', '!', '\\', '/' };

        /// <summary>
        /// HashSet of possible word delimiters
        /// </summary>
        private static readonly HashSet<char> wordDelimiterHash;

        /// <summary>
        /// Manages insertions of words
        /// </summary>
        private static WordInsertionManager wordInsertionManager = new WordInsertionManager();

        /// <summary>
        /// Manages inversions of words
        /// </summary>
        private static WordInversionManager wordInversionManager = new WordInversionManager();
        #endregion

        #region Constructors
        /// <summary>
        /// Initialization of default data
        /// </summary>
        static StringManipulations()
        {
            wordDelimiterHash = new HashSet<char>(wordDelimiterList);
            wordSequenceReplacer = new WordSequenceReplacer();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Replace content in string but keep original case
        /// </summary>
        /// <param name="original">original</param>
        /// <param name="from">to replace</param>
        /// <param name="to">to replace to</param>
        /// <returns>String with replaced content with case kept</returns>
        public static string ReplaceWordKeepCase(this string original, string from, string to)
        {
            return keepCaseReplacer.ReplaceWord(original, from, to);
        }

        /// <summary>
        /// Invert two word occurence in a string but keep original case
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="word1">word 1</param>
        /// <param name="word2">word 2</param>
        /// <returns>String with occurences inverted with case kept</returns>
        public static string InvertWordKeepCase(this string original, string word1, string word2)
        {
            string newString = keepCaseReplacer.ReplaceWord(original, word1, "aeiouaeiouaeiouaeiouaeiou");
            newString = keepCaseReplacer.ReplaceWord(newString, word2, word1);
            newString = keepCaseReplacer.ReplaceWord(newString, "aeiouaeiouaeiouaeiouaeiou", word2);
            return newString;
        }

        /// <summary>
        /// Whether a char is upperCase
        /// </summary>
        /// <param name="letter">char</param>
        /// <returns>Whether a char is upperCase</returns>
        public static bool IsUpperCase(this char letter)
        {
            return letter.ToString().ToUpper() == letter.ToString();
        }

        /// <summary>
        /// Convert a char to upperCase
        /// </summary>
        /// <param name="letter">char</param>
        /// <returns>UpperCase version of char</returns>
        public static char ToUpper(this char letter)
        {
            return letter.ToString().ToUpper()[0];
        }

        /// <summary>
        /// Convert a char to lowerCase
        /// </summary>
        /// <param name="letter">char</param>
        /// <returns>lowerCase version</returns>
        public static char ToLower(this char letter)
        {
            return letter.ToString().ToLowerInvariant()[0];
        }

        /// <summary>
        /// Whether string contains specified word
        /// </summary>
        /// <param name="text">text to analyze</param>
        /// <param name="word">word to find in text</param>
        /// <returns>Whether string contains specified word</returns>
        public static bool ContainsWord(this string text, string word)
        {
            word = word.ToLowerInvariant();
            WordStringStream wordStringStream = new WordStringStream(text);
            return wordStringStream.ContainsWord(word);
        }

        /// <summary>
        /// Remove word from string
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="wordToRemove">word to remove</param>
        /// <returns>String with specified word removed</returns>
        public static string RemoveWord(this string original, string wordToRemove)
        {
            return RemoveWord(original, wordToRemove, 0);
        }

        /// <summary>
        /// Remove word from string by word name
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="wordToRemove">word to remove</param>
        /// <param name="occurenceCount">how many times we remove it (default: infinite: 0)</param>
        /// <returns>String with specified word removed</returns>
        public static string RemoveWord(this string original, string wordToRemove, int occurenceCount)
        {
            return wordRemover.RemoveWord(original, wordToRemove, occurenceCount);
        }

        /// <summary>
        /// Remove word from string by word position
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="wordPosition">position of word to remove (starting at 0)</param>
        /// <param name="isKeepDelimiterAfterNotBefore">true: keep delimiter after removed word, false: keep delimiter before word to remove</param>
        /// <returns>String with removed word at specified position with specified delimiter kept</returns>
        public static string RemoveWord(this string original, int wordPosition, bool isKeepDelimiterAfterNotBefore)
        {
            return wordRemover.RemoveWord(original, wordPosition, isKeepDelimiterAfterNotBefore);
        }

        /// <summary>
        /// Insert a word or a groupe of word at provided index (0: before everything)
        /// </summary>
        /// <param name="originalString">original string</param>
        /// <param name="wordsToInsert">words to insert</param>
        /// <param name="positionIndex">position index</param>
        /// <returns>new string with inserted words</returns>
        public static string InsertWords(this string originalString, string wordsToInsert, int positionIndex)
        {
            return wordInsertionManager.InsertWords(originalString, wordsToInsert, positionIndex);
        }

        /// <summary>
        /// Invert word positions in string
        /// </summary>
        /// <param name="originalString">original string</param>
        /// <param name="wordPosition1">word1's position</param>
        /// <param name="wordPosition2">word2's position</param>
        /// <returns>String with position of word 1 and word 2 inverted</returns>
        public static string InvertWordPosition(this string originalString, int wordPosition1, int wordPosition2)
        {
            return wordInversionManager.InvertWordPosition(originalString, wordPosition1, wordPosition2);
        }

        /// <summary>
        /// Replace a word sequence in proposition
        /// </summary>
        /// <param name="originalProposition">original proposition (case insensitive)</param>
        /// <param name="fromSequence">from word sequence (case insensitive, delimiter insensitive)</param>
        /// <param name="toSequence">from word sequence (case insensitive, delimiter insensitive)</param>
        /// <returns>proposition with replaced word sequences</returns>
        public static string ReplaceWordSequence(this string originalProposition, string fromSequence, string toSequence)
        {
            return wordSequenceReplacer.ReplaceWordSequence(originalProposition, fromSequence, toSequence);
        }

        /// <summary>
        /// Replace word sequence
        /// </summary>
        /// <param name="original">original proposition</param>
        /// <param name="startWordSequence">begining of the word sequence</param>
        /// <param name="midWordCount">how many unknown words in middle of sequence to replace</param>
        /// <param name="endWordSequence">ending of word sequence</param>
        /// <param name="startReplace">begining of replaced word sequence</param>
        /// <param name="endReplace">ending of replaced word sequence</param>
        /// <returns>Text with replaced word sequences</returns>
        public static string ReplaceWordSequence(this string original, string startWordSequence, int midWordCount, string endWordSequence, string startReplace, string endReplace)
        {
            return wordSequenceReplacer.ReplaceWordSequence(original, startWordSequence, midWordCount, endWordSequence, startReplace, endReplace);
        }

        /// <summary>
        /// Receives string and returns the string with its letters reversed.
        /// </summary>
        public static string ReverseString(this string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Convert string to all lowercase and replace word from to
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="from">from word</param>
        /// <param name="to">to word</param>
        /// <returns>lowcase string with replaced words</returns>
        internal static string ReplaceWordInsensitiveLower(this string original, string from, string to)
        {
            original = original.ToLowerInvariant();
            from = from.ToLowerInvariant();
            to = to.ToLowerInvariant();

            string newString = " " + original + " ";

            foreach (char wordDelimiter1 in wordDelimiterList)
                foreach (char wordDelimiter2 in wordDelimiterList)
                    newString = newString.Replace(wordDelimiter1 + from + wordDelimiter2, wordDelimiter1 + to + wordDelimiter2);

            newString = newString.Substring(1, newString.Length - 2);

            return newString;
        }

        /// <summary>
        /// Whether character is a word delimiter
        /// </summary>
        /// <param name="letter">character</param>
        /// <returns>Whether character is a word delimiter</returns>
        internal static bool IsWordDelimiter(char letter)
        {
            return wordDelimiterHash.Contains(letter);
        }
        #endregion
    }
}
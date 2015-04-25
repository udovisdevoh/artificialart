using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Replace words in string while keeping original case structure
    /// </summary>
    internal class KeepCaseReplacer
    {
        #region Internal Methods
        /// <summary>
        /// replace words in string while keeping original case structure
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="from">string to replace</param>
        /// <param name="to">replace to</param>
        /// <returns>String with replaced words with case structure kept</returns>
        internal string ReplaceWord(string original, string from, string to)
        {
            string newString = original.ReplaceWordInsensitiveLower(from, to);
            newString = ApplyWordCaseStructureToText(newString, original);
            return newString;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Take a string and apply word case structure from another
        /// </summary>
        /// <param name="targetText">string to modify</param>
        /// <param name="sourceText">string to take case from</param>
        /// <returns>new string with modified case</returns>
        private string ApplyWordCaseStructureToText(string targetText, string sourceText)
        {
            WordStringStream targetWordStream = new WordStringStream(targetText);
            WordStringStream sourceWordStream = new WordStringStream(sourceText);

            string newString = targetWordStream.FirstDelimiter;

            string targetWord, targetDelimiter, sourceWord, sourceDelimiter;
            while (targetWordStream.TryGetNextWord(out targetWord, out targetDelimiter))
            {
                if (sourceWordStream.TryGetNextWord(out sourceWord, out sourceDelimiter))
                {
                    newString += ApplyWordCaseStructureToWord(targetWord, sourceWord);

                    if (targetDelimiter != null)
                        newString += targetDelimiter;
                }
            }

            return newString;
        }

        /// <summary>
        /// Take a word and apply case structure of another word
        /// </summary>
        /// <param name="targetWord">word to modify</param>
        /// <param name="sourceWord">word to take case structure</param>
        /// <returns>modified word with the other word's case structure</returns>
        private string ApplyWordCaseStructureToWord(string targetWord, string sourceWord)
        {
            char[] targetCharList = targetWord.ToCharArray();
            char[] sourceCharList = sourceWord.ToCharArray();

            string newWord = string.Empty;

            int charCounter = 0;
            bool isUpperCase = false;
            foreach (char targetLetter in targetCharList)
            {
                if (sourceCharList.Length > charCounter)
                    isUpperCase = sourceCharList[charCounter].IsUpperCase();

                if (isUpperCase)
                    newWord += targetLetter.ToUpper();
                else
                    newWord += targetLetter.ToLower();

                charCounter++;
            }

            return newWord;
        }
        #endregion
    }
}

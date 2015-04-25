using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Manages insertions of words
    /// </summary>
    internal class WordInsertionManager
    {
        #region Internal Methods
        /// <summary>
        /// Insert a word or a groupe of word at provided index (0: before everything)
        /// </summary>
        /// <param name="originalString">original string</param>
        /// <param name="wordsToInsert">words to insert</param>
        /// <param name="desiredIndex">desired position index</param>
        /// <returns>new string with inserted words</returns>
        internal string InsertWords(string originalString, string wordsToInsert, int desiredIndex)
        {
            WordStream wordStream = new WordStream(originalString);
            string newString = wordStream.FirstDelimiter;

            Word word;
            int index = 0;
            while (wordStream.TryGetNextWord(out word))
            {
                if (index == desiredIndex)
                {
                    newString += wordsToInsert.Trim();
                    newString += " ";
                }

                newString += word.ToString();
                if (word.RightDelimiter != null)
                    newString += word.RightDelimiter;

                index++;
            }

            return newString;
        }
        #endregion
    }
}

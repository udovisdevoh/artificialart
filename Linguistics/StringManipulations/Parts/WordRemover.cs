using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Used to remove words from strings
    /// </summary>
    internal class WordRemover
    {
        #region Internal Methods
        /// <summary>
        /// Remove word from string
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="wordToRemove">word to remove</param>
        /// <param name="desiredOccurenceCount">how many times we remove it (default: infinite: 0)</param>
        /// <returns>String with specified word removed</returns>
        internal string RemoveWord(string original, string wordToRemove, int desiredOccurenceCount)
        {
            int counter = 0;

            WordStream wordStream = new WordStream(original);

            string newString = wordStream.FirstDelimiter;

            Word currentWord;
            Word latestWord = null;
            bool isFirstWord = true;
            bool isPreviousWordRemoved = false;

            while (wordStream.TryGetNextWord(out currentWord))
            {
                latestWord = currentWord;
                if (desiredOccurenceCount == 0 || counter < desiredOccurenceCount)
                {
                    if (currentWord.ToString().ToLowerInvariant() == wordToRemove.ToLowerInvariant())
                    {
                        if (!isFirstWord)
                        {
                            newString += GetLongestDelimiter(currentWord.LeftDelimiter, currentWord.RightDelimiter);
                        }
                        isPreviousWordRemoved = true;
                        counter++;
                    }
                    else
                    {
                        if (!isFirstWord && !isPreviousWordRemoved && currentWord.LeftDelimiter != null)
                            newString += currentWord.LeftDelimiter;

                        newString += currentWord.ToString();
                        isPreviousWordRemoved = false;
                    }

                    isFirstWord = false;
                }
                else
                {
                    if (!isFirstWord && !isPreviousWordRemoved && currentWord.LeftDelimiter != null)
                        newString += currentWord.LeftDelimiter;
                    newString += currentWord.ToString();
                    isPreviousWordRemoved = false;
                }
            }

            if (latestWord != null && latestWord.RightDelimiter != null)
                newString += latestWord.RightDelimiter.ToString();

            return newString;
        }

        /// <summary>
        /// Remove word from string by word position
        /// </summary>
        /// <param name="original">original string</param>
        /// <param name="wordPosition">position of word to remove (starting at 0)</param>
        /// <param name="isKeepDelimiterAfterNotBefore">true: keep delimiter after removed word, false: keep delimiter before word to remove</param>
        /// <returns>String with removed word at specified position with specified delimiter kept</returns>
        internal string RemoveWord(string original, int wordPosition, bool isKeepDelimiterAfterNotBefore)
        {
            WordStream wordStream = new WordStream(original);

            string newString = wordStream.FirstDelimiter;

            int wordCounter = 0;
            foreach (Word word in wordStream)
            {
                if (wordCounter == wordPosition && wordCounter != 0)
                {
                    if (isKeepDelimiterAfterNotBefore && word.RightDelimiter != null)
                        newString += word.RightDelimiter;
                    else if (word.LeftDelimiter != null)
                        newString += word.LeftDelimiter;
                }
                else if (wordCounter != wordPosition)
                {
                    newString += word.ToString();
                    if (word.RightDelimiter != null && wordCounter != wordPosition - 1)
                        newString += word.RightDelimiter;
                }

                wordCounter++;
            }

            newString = newString.Trim();

            return newString;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// From two delimiters, return the longest one
        /// </summary>
        /// <param name="delimiter1">delimiter 1</param>
        /// <param name="delimiter2">delemiter 2</param>
        /// <returns>longest of the two</returns>
        private string GetLongestDelimiter(string delimiter1, string delimiter2)
        {
            if (delimiter1 == null && delimiter2 == null)
                return string.Empty;
            else if (delimiter1 == null)
                return delimiter2;
            else if (delimiter2 == null)
                return delimiter1;
            else if (delimiter1.Length > delimiter2.Length)
                return delimiter1;
            else
                return delimiter2;
        }
        #endregion
    }
}

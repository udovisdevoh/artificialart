using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages words like "don't" and "didn't" and "doesn't"
    /// </summary>
    internal class NtManager
    {
        #region Fields
        /// <summary>
        /// List of words for which we can add "n't"
        /// </summary>
        private HashSet<string> ntAbleWordList;
        #endregion

        #region Constructor
        /// <summary>
        /// Create an n't manager
        /// </summary>
        public NtManager()
        {
            ntAbleWordList = new HashSet<string>();
            ntAbleWordList.Add("did");
            ntAbleWordList.Add("should");
            ntAbleWordList.Add("could");
            ntAbleWordList.Add("would");
            ntAbleWordList.Add("will");//Must be altered to "won't"
            ntAbleWordList.Add("can");
            ntAbleWordList.Add("do");
            ntAbleWordList.Add("must");
            ntAbleWordList.Add("does");
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Remove words like "don't" and "didn't" and "doesn't"
        /// </summary>
        /// <param name="originalText">original text</param>
        /// <returns>original text with removed occurence of don't, didn't or doesn't</returns>
        internal string RemoveNt(string originalText)
        {
            return RemoveNt(originalText, 0);
        }

        /// <summary>
        /// Remove n't from words like "don't" and "didn't" and "doesn't"
        /// </summary>
        /// <param name="originalText">original text</param>
        /// <param name="desiredRemoveCount">how many times we remove it (default: 0 as infinite)</param>
        /// <returns>original text with removed occurence of don't, didn't or doesn't</returns>
        internal string RemoveNt(string originalText, int desiredRemoveCount)
        {
            int index = originalText.ToLowerInvariant().IndexOf("n't");

            bool wasContainWordWo = originalText.ToLowerInvariant().ContainsWord("wo");

            string newString = originalText.Remove(index, 3);

            if (!wasContainWordWo && newString.ToLowerInvariant().ContainsWord("wo"))
                newString = newString.ReplaceWordKeepCase("wo", "will");

            return newString;
        }

        /// <summary>
        /// Whether string contains words for which "n't" can be added
        /// </summary>
        /// <param name="text">string to analyze</param>
        /// <returns>Whether string contains words for which "n't" can be added</returns>
        internal bool ContainsNtAbleWord(string text)
        {
            WordStringStream wordStringStream = new WordStringStream(text);

            string currentWord, currentDelimiter;
            while (wordStringStream.TryGetNextWord(out currentWord, out currentDelimiter))
                if (ntAbleWordList.Contains(currentWord.ToLowerInvariant()))
                    return true;

            return false;
        }

        /// <summary>
        /// Add "n't" to word from original text
        /// </summary>
        /// <param name="text">original text</param>
        /// <param name="desiredChangeCount">how many times to do it (0: infinite)</param>
        /// <returns></returns>
        internal string AddNt(string text, int desiredChangeCount)
        {
            WordStringStream wordStringStream = new WordStringStream(text);

            string currentWord, currentDelimiter;

            string newString = wordStringStream.FirstDelimiter;
            int changeCount = 0;
            while (wordStringStream.TryGetNextWord(out currentWord, out currentDelimiter))
            {
                if ((desiredChangeCount == 0 || changeCount < desiredChangeCount) && ntAbleWordList.Contains(currentWord.ToLowerInvariant()))
                {
                    currentWord += "n't";

                    if (currentWord.ToLowerInvariant() == "willn't")
                        currentWord = "won't";

                    changeCount++;
                }

                newString += currentWord;
                if (currentDelimiter != null)
                    newString += currentDelimiter;
            }

            return newString;
        }

        /// <summary>
        /// Add don't before first verb
        /// </summary>
        /// <param name="text">text to modify</param>
        /// <returns>modified text</returns>
        internal string AddDontBeforeFirstVerb(string text)
        {
            WordStringStream wordStringStream = new WordStringStream(text);

            string newString = wordStringStream.FirstDelimiter;

            string currentWord, currentDelimiter;
            bool isDidIt = false;
            while (wordStringStream.TryGetNextWord(out currentWord, out currentDelimiter))
            {
                if (!isDidIt)
                {
                    if (currentWord.IsVerb()) //doesn't need to add "s"
                    {
                        currentWord = "don't " + currentWord;
                        isDidIt = true;
                    }
                }

                newString += currentWord;
                if (currentDelimiter != null)
                    newString += currentDelimiter;
            }

            return newString;
        }
        #endregion
    }
}
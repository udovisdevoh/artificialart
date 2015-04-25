using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages questions that are questions just because they contain ? for they wouldn't be questions otherwise
    /// </summary>
    internal class QuestionManagerByMarkOnly
    {
        #region Internal Methods
        /// <summary>
        /// Whether question was detected by ?
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <returns>Whether question was detected by ?</returns>
        internal bool IsQuestion(string originalProposition)
        {
            originalProposition = originalProposition.ToLowerInvariant();
            return originalProposition.Contains('?');
        }

        /// <summary>
        /// Remove question mark
        /// </summary>
        /// <param name="originalProposition">originalProposition</param>
        /// <returns>proposition with question mark removed</returns>
        internal string RemoveQuestion(string originalProposition)
        {
            WordStream wordStream = new WordStream(originalProposition);
            Word lastWord = wordStream.Last();
            if (lastWord.RightDelimiter != null && lastWord.RightDelimiter.Contains('?'))
                lastWord.RightDelimiter = "";
            return wordStream.ToString();
        }
        #endregion
    }
}

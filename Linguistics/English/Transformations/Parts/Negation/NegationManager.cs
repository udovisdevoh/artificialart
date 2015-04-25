using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Performs negation of proposions
    /// </summary>
    internal class NegationManager
    {
        #region Parts
        /// <summary>
        /// Manages words like "don't" and "didn't" and "doesn't"
        /// </summary>
        private NtManager ntManager = new NtManager();

        /// <summary>
        /// Manages operations on the word "not"
        /// </summary>
        private NotManager notManager = new NotManager();

        /// <summary>
        /// Manages operations on the word "ain't"
        /// </summary>
        private AintManager aintManager = new AintManager();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Invert negation of proposition by removing or adding words like "not" or by replacing a word to an antonym
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <returns>Proposition with inverted negation or with antonyms</returns>
        internal string InvertNegation(string originalProposition)
        {
            if (originalProposition.Trim().ToLowerInvariant().StartsWith("don't"))
                return originalProposition.RemoveWord(0, true);
            else if (originalProposition.ContainsWord("not"))
                return originalProposition.RemoveWord("not", 1);
            else if (originalProposition.ContainsWord("ain't"))
                return aintManager.RemoveAintOnce(originalProposition);
            else if (originalProposition.ToLowerInvariant().Contains("n't"))
                return ntManager.RemoveNt(originalProposition, 1);
            else if (ntManager.ContainsNtAbleWord(originalProposition))
                return ntManager.AddNt(originalProposition, 1);
            else if (ContainsWordEndingWithIng(originalProposition))
                return notManager.AddNotBeforeFirstWordEndingWithIng(originalProposition);
            else if (notManager.ContaisnWordToPutNotAfter(originalProposition))
                return notManager.AddNotAfterFirstWordForIt(originalProposition);
            else if (Analysis.ContainsVerb(originalProposition))
                return ntManager.AddDontBeforeFirstVerb(originalProposition);
            else if (Transformations.ContainsAntonym(originalProposition))
                return Transformations.InvertAntonym(originalProposition, 1);

            return originalProposition.InsertWords("It's not like", 0);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Whether text contains a word ending with ing or in'
        /// </summary>
        /// <param name="text">text to analyze</param>
        /// <returns>Whether text contains a word ending with ing or in'</returns>
        private bool ContainsWordEndingWithIng(string text)
        {
            WordStringStream wordStringStream = new WordStringStream(text);
            foreach (string word in wordStringStream)
                if (word.ToLowerInvariant().EndsWith("ing") || word.ToLowerInvariant().EndsWith("in'"))
                    return true;
            return false;
        }
        #endregion
    }
}

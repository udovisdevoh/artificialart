using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages questions that are questions because they start with question starting words
    /// </summary>
    internal class QuestionManagerByStartingWithQuestionWord
    {
        #region Internal Methods
        /// <summary>
        /// Whether proposition is detected to be a question because it starts with a question word
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <returns>Whether proposition is detected to be a question because it starts with a question word</returns>
        internal bool IsQuestion(string originalProposition)
        {
            originalProposition = originalProposition.ToLowerInvariant();
            WordStringStream wordStringStream = new WordStringStream(originalProposition);
            return wordStringStream.First().IsQuestionBeginWord();
        }

        /// <summary>
        /// Remove question from proposition
        /// </summary>
        /// <param name="proposition">original proposition</param>
        /// <returns>proposition with question removed</returns>
        internal string RemoveQuestion(string proposition)
        {
            while (proposition.CountWords() > 1 && IsFirstAndSecondWordQuestionBeginWord(proposition))
                proposition = proposition.RemoveWord(0, false);

            if (proposition.CountWords() > 1 && IsQuestion(proposition))
            {
                WordStream wordStream = new WordStream(proposition);
                wordStream[0].StringValue = wordStream[0].StringValue.ToLowerInvariant();
                proposition = wordStream.ToString();
                proposition = proposition.InvertWordPosition(0, 1);
            }

            return proposition;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Whether the two first words are question begining words
        /// </summary>
        /// <param name="proposition">proposition to analyze</param>
        /// <returns>Whether the two first words are question begining words</returns>
        private bool IsFirstAndSecondWordQuestionBeginWord(string proposition)
        {
            return IsQuestion(proposition) && IsQuestion(proposition.RemoveWord(0, false));
        }
        #endregion
    }
}

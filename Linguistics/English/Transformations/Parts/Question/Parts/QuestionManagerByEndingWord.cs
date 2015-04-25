using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages questions that are questions because they end with a special word for questions
    /// </summary>
    internal class QuestionManagerByEndingWord
    {
        /// <summary>
        /// Whether question was detected because of the ending word
        /// </summary>
        /// <param name="originalProposition">proposition</param>
        /// <returns>Whether question was detected because of the ending word</returns>
        internal bool IsQuestion(string originalProposition)
        {
            originalProposition = originalProposition.ToLowerInvariant();
            WordStringStream wordStringStream = new WordStringStream(originalProposition);
            return wordStringStream.Last().IsQuestionEndingWord();
        }

        /// <summary>
        /// Remove question from proposition
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>proposition with removed question</returns>
        internal string RemoveQuestion(string proposition)
        {
            return proposition.RemoveWord(proposition.CountWords() - 1, true);
        }
    }
}

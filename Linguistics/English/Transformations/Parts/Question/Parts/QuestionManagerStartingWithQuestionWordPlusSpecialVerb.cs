using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages questions that are questions because they begin with question word plus special verb
    /// </summary>
    internal class QuestionManagerStartingWithQuestionWordPlusSpecialVerb
    {
        #region Fields
        /// <summary>
        /// Special verb list
        /// </summary>
        private HashSet<string> specialVerbList;
        #endregion

        #region Constructor
        public QuestionManagerStartingWithQuestionWordPlusSpecialVerb()
        {
            specialVerbList = new HashSet<string>();
            specialVerbList.Add("is");
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Whether question is question because it starts with a question word followed by a verb
        /// </summary>
        /// <param name="originalProposition"></param>
        /// <returns></returns>
        internal bool IsQuestion(string originalProposition)
        {
            originalProposition = originalProposition.ToLowerInvariant();
            WordStringStream wordStringStream = new WordStringStream(originalProposition);
            return wordStringStream.First().IsQuestionBeginWord() && wordStringStream[1] != null && specialVerbList.Contains(wordStringStream[1].ToLowerInvariant());
        }

        /// <summary>
        /// Remove the two first words
        /// </summary>
        /// <param name="proposition"></param>
        /// <returns></returns>
        internal string RemoveQuestion(string proposition)
        {
            proposition = proposition.RemoveWord(0, true);
            proposition = proposition.RemoveWord(0, true);
            return proposition;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages the difference between subjects and objects
    /// </summary>
    internal class SubjectObjectDetector
    {
        #region Constants
        /// <summary>
        /// When it is a subject
        /// </summary>
        private const bool isSubject = true;

        /// <summary>
        /// When it is an object
        /// </summary>
        private const bool isObject = false;
        #endregion

        #region Public Methods
        /// <summary>
        /// Whether word is a subject and not an object according to provided context
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is a subject and not an object according to provided context</returns>
        internal bool IsSubjectNotObject(Word word)
        {
            string previousWord = null;
            string nextWord = null;
            
            if (word.PreviousWord != null)
                previousWord = word.PreviousWord.ToString().ToLowerInvariant();

            if (word.NextWord != null)
                nextWord = word.NextWord.ToString().ToLowerInvariant();

            #region For stuff like "who are you?"
            if (previousWord != null)
            {
                if (previousWord == "are")
                    return isSubject;

                if (previousWord == "were")
                    return isSubject;

                if (previousWord == "am")
                    return isSubject;

                if (previousWord == "was")
                    return isSubject;
            }
            #endregion

            if (word.IsSentenceBegin)
                return isSubject;

            if (word.PreviousWord == null)
                return isSubject;

            if (word.NextWord == null)
                return isObject;

            if (word.LeftDelimiter == null)
                return isSubject;

            if (word.RightDelimiter == null)
                return isObject;

            if (word.LeftDelimiter.Contains(','))
                return isSubject;

            if (word.RightDelimiter.Contains(','))
                return isObject;

            if (previousWord == "do" && nextWord == "a")
                return isObject;

            if (previousWord == "do" && nextWord == "an")
                return isObject;

            if (previousWord == "do")
                return isSubject;

            if (previousWord == "to")
                return isObject;

            if (previousWord == "at")
                return isObject;

            if (previousWord == "on")
                return isObject;

            if (previousWord.IsModalVerb())
                return isSubject;

            bool isNextWordVerb = Analysis.IsVerb(nextWord);
            bool isPreviousWordVerb = Analysis.IsVerb(previousWord);


            if (isPreviousWordVerb && Analysis.IsPreposition(nextWord))
                return isObject;

            if (Analysis.IsPostposition(nextWord))
                return isObject;

            if (Analysis.IsQuestionBeginWord(previousWord) || previousWord.StartsWith("some") && Analysis.IsQuestionBeginWord(previousWord.Substring(4)))
                return isSubject;

            if (Analysis.IsSubordinatingConjunctionBeforeSubject(previousWord))
                return isSubject;

            if (isNextWordVerb)
                return isSubject;

            if (isPreviousWordVerb)
                return isObject;

            return isObject;
        }
        #endregion
    }
}

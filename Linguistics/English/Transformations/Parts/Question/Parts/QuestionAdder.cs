using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Converts affirmations to questions
    /// </summary>
    internal class QuestionAdder
    {
        #region Internal Methods
        /// <summary>
        /// Convert proposition to question
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>proposition as question</returns>
        internal string AddQuestion(string proposition)
        {
            if (IsSecondWordModalVerbOrQuestionBeginWord(proposition))
            {
                proposition = InvertFirstAndSecondWord(proposition);
            }
            else if (IsFirstWordYouOrI(proposition))
            {
                proposition = AddDoBefore(proposition);
            }
            else if (proposition.IsImperative())
            {
                proposition = AddWhyShouldIBefore(proposition);
            }
            else
            {
                proposition = AddDoYouThinkBefore(proposition);
            }

            proposition = AddQuestionMark(proposition);

            return proposition;

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add word "do" before proposition
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>proposition with "do" before</returns>
        private string AddDoBefore(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);

            if (wordStream.CountWords() > 0 && wordStream[0].StringValue.IsPronoun() && wordStream[0].StringValue.ToLowerInvariant() != "i")
            {
                wordStream[0].StringValue = wordStream[0].StringValue.ToLowerInvariant();
            }

            proposition = wordStream.ToString();
            proposition = proposition.InsertWords("Do", 0);
            return proposition;
        }

        /// <summary>
        /// Add "do you think" before proposition
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>proposition with "do you think" before</returns>
        private string AddDoYouThinkBefore(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);

            if (wordStream.CountWords() > 0 && wordStream[0].StringValue.IsPronoun() && wordStream[0].StringValue.ToLowerInvariant() != "i")
            {
                wordStream[0].StringValue = wordStream[0].StringValue.ToLowerInvariant();
            }

            proposition = wordStream.ToString();
            proposition = proposition.InsertWords("Do you think", 0);
            return proposition;
        }

        private string AddWhyShouldIBefore(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);

            if (wordStream.CountWords() > 0 && wordStream[0].StringValue.IsPronoun() && wordStream[0].StringValue.ToLowerInvariant() != "i")
            {
                wordStream[0].StringValue = wordStream[0].StringValue.ToLowerInvariant();
            }

            proposition = wordStream.ToString();
            proposition = proposition.InsertWords("Why should I", 0);
            return proposition;
        }

        /// <summary>
        /// Invert first and second word (which is a modal verb or a question word)
        /// </summary>
        /// <param name="proposition">source proposition</param>
        /// <returns>proposition with inverted 1st and 2nd word</returns>
        private string InvertFirstAndSecondWord(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);
            Word firstWord = wordStream[0];
            if (firstWord.StringValue.IsPronoun() && firstWord.StringValue.ToLowerInvariant() != "i")
            {
                firstWord.StringValue = firstWord.StringValue.ToLowerInvariant();
                proposition = wordStream.ToString();
            }
            proposition = proposition.InvertWordPosition(0, 1);
            return proposition;
        }

        /// <summary>
        /// true: second word is modal verb or question begin word, else: false
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>true: second word is modal verb or question begin word, else: false</returns>
        private bool IsSecondWordModalVerbOrQuestionBeginWord(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);
            return wordStream.CountWords() > 1 && wordStream.CountWords() > 0 && (wordStream[1].StringValue.ToLowerInvariant().IsQuestionBeginWord() || wordStream[1].StringValue.ToLowerInvariant().IsModalVerb());
        }

        /// <summary>
        /// Whether first word in proposition is "you" or "I"
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>Whether first word in proposition is "you" or "I"</returns>
        private bool IsFirstWordYouOrI(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);
            return wordStream.CountWords() > 0 && wordStream[0].StringValue.ToLowerInvariant() == "i" || wordStream[0].StringValue.ToLowerInvariant() == "you";
        }

        /// <summary>
        /// Add question mark to proposition
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>proposition with question mark</returns>
        private string AddQuestionMark(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);
            Word lastWord = wordStream.Last();
            lastWord.RightDelimiter = "?";
            return wordStream.ToString();
        }
        #endregion
    }
}

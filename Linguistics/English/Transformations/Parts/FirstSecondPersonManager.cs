using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages words such as you, me, I, yours mine, my, mines etc...
    /// </summary>
    internal class FirstSecondPersonManager
    {
        #region Internal Methods
        /// <summary>
        /// Invert "YOU" and "I" from string (your and my etc...)
        /// </summary>
        /// <param name="originalText">original text</param>
        /// <returns>Text with YOU and I inverted (your and my etc...)</returns>
        internal string InvertFirstSecondPerson(string originalText)
        {
            originalText = originalText.InvertWordKeepCase("my", "your");
            originalText = originalText.InvertWordKeepCase("i'm", "you're");
            originalText = originalText.InvertWordKeepCase("you'd", "i'd");
            originalText = originalText.InvertWordKeepCase("i'll", "you'll");
            originalText = originalText.InvertWordKeepCase("mine", "yours");
            originalText = originalText.InvertWordKeepCase("myself", "yourself");


            WordStream wordStream = new WordStream(originalText);

            string newString = wordStream.FirstDelimiter;
            Word originalWord;
            string newWord;
            string previousWord = null;
            string previousDelimiter = null;

            if (wordStream.FirstDelimiter != string.Empty)
                previousDelimiter = wordStream.FirstDelimiter;

            while (wordStream.TryGetNextWord(out originalWord))
            {
                newWord = originalWord.ToString().ReplaceWordKeepCase("i", "me");

                if (newWord == "ME")
                    newWord = "me";

                newWord = newWord.InvertWordKeepCase("you", "me");

                //Verb inversion
                if (previousWord != null && (previousWord.ToLowerInvariant() == "i" || previousWord.ToLowerInvariant() == "you" || previousWord.ToLowerInvariant() == "me"))
                {
                    newWord = newWord.InvertWordKeepCase("am", "are");
                    newWord = newWord.InvertWordKeepCase("was", "were");
                    newWord = newWord.InvertWordKeepCase("aren't", "amn't");
                }
                else if (originalWord.NextWord != null && (originalWord.NextWord.ToString().ToLowerInvariant() == "i" || originalWord.NextWord.ToString().ToLowerInvariant() == "you" || originalWord.NextWord.ToString().ToLowerInvariant() == "me"))
                {
                    newWord = newWord.InvertWordKeepCase("am", "are");
                    newWord = newWord.InvertWordKeepCase("was", "were");
                    newWord = newWord.InvertWordKeepCase("aren't", "amn't");
                }


                if (newWord.ToLowerInvariant() == "me" || newWord.ToLowerInvariant() == "i")
                    newWord = Analysis.IsSubjectNotObject(originalWord) ? "i" : "me";

                newWord = FixCase(newWord, originalWord.IsSentenceBegin);

                newString += newWord;

                if (originalWord.RightDelimiter != null)
                    newString += originalWord.RightDelimiter;

                originalWord.StringValue = newWord;

                previousWord = newWord;
                previousDelimiter = originalWord.RightDelimiter;
            }

            return newString;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Fix the case for words: You, me and I
        /// </summary>
        /// <param name="youMeI">You, me or I</param>
        /// <param name="isSentenceBegin">whether the word is at the beginin of a sentence</param>
        /// <returns>word with fixed case</returns>
        private string FixCase(string youMeI, bool isSentenceBegin)
        {
            if (youMeI == "i")
                youMeI = "I";

            if (youMeI == "i'm")
                youMeI = "I'm";

            if (youMeI == "YOu" && !isSentenceBegin)
                youMeI = "you";

            if (youMeI == "YOu're")
                youMeI = "You're";

            if (youMeI == "YOu'll")
                youMeI = "You'll";

            if (youMeI == "You" && !isSentenceBegin)
                youMeI = "you";

            if (youMeI == "You're" && !isSentenceBegin)
                youMeI = "you're";

            if (youMeI == "You're" && !isSentenceBegin)
                youMeI = "you";

            if (youMeI == "you" && isSentenceBegin)
                youMeI = "You";

            if (youMeI == "YOu")
                youMeI = "You";

            if (youMeI == "YOu'd")
                youMeI = "You'd";

            if (youMeI == "I'lL")
                youMeI = "I'll";

            return youMeI;
        }
        #endregion
    }
}

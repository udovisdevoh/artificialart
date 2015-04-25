using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// For linguistic and semantical analysis
    /// </summary>
    public static class Analysis
    {
        #region Parts
        /// <summary>
        /// Manages I and YOU
        /// </summary>
        private static SubjectObjectDetector subjectObjectDetector = new SubjectObjectDetector();

        /// <summary>
        /// Manages analysis of verbs
        /// </summary>
        private static VerbManager verbManager = new VerbManager();

        /// <summary>
        /// List of pronouns
        /// </summary>
        private static WordList pronounList;

        /// <summary>
        /// List of prepositions
        /// </summary>
        private static WordList prepositionList;

        /// <summary>
        /// List of postpositions
        /// </summary>
        private static WordList postpositionList;

        /// <summary>
        /// List of words like "when, why, how, where"
        /// </summary>
        private static WordList questionBeginList;

        /// <summary>
        /// List of words like "when, why, how, where" for question endings
        /// </summary>
        private static WordList questionEndingList;

        /// <summary>
        /// List of subordinating conjunctions that can be put before subject. For instance: while, if
        /// </summary>
        private static WordList subordinatingConjunctionBeforeSubjectList;
        #endregion

        #region Constructor
        /// <summary>
        /// Static constructor
        /// </summary>
        static Analysis()
        {
            pronounList = new WordListFromString(ArtificialArt.Properties.Resources.pronounList);
            prepositionList = new WordListFromString(ArtificialArt.Properties.Resources.prepositionList);
            postpositionList = new WordListFromString(ArtificialArt.Properties.Resources.postpositionList);
            questionBeginList = new WordListFromString(ArtificialArt.Properties.Resources.questionBeginWordList);
            questionEndingList = new WordListFromString(ArtificialArt.Properties.Resources.questionEndingWordList);
            subordinatingConjunctionBeforeSubjectList = new WordListFromString(ArtificialArt.Properties.Resources.subordinatingConjunctionBeforeSubjectList);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Count how many words in string
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static int CountWords(this string originalString)
        {
            WordStringStream wordStringStream = new WordStringStream(originalString);
            return wordStringStream.CountWords();
        }

        /// <summary>
        /// Whether the word is a verb
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is a verb</returns>
        public static bool IsVerb(this string word)
        {
            word = word.ToLowerInvariant();
            return verbManager.IsVerb(word);
        }

        /// <summary>
        /// Whether the word is a modal verb
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is a modal verb</returns>
        public static bool IsModalVerb(this string word)
        {
            word = word.ToLowerInvariant();
            return verbManager.IsModalVerb(word);
        }

        /// <summary>
        /// Whether the word is a pronoun
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is a pronoun</returns>
        public static bool IsPronoun(this string word)
        {
            word = word.ToLowerInvariant();
            return pronounList.ContainsExact(word);
        }

        /// <summary>
        /// Whether word is a preposition
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is a preposition</returns>
        public static bool IsPreposition(this string word)
        {
            word = word.ToLowerInvariant();
            return prepositionList.ContainsExact(word);
        }

        /// <summary>
        /// Whether word is a postposition
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is a preposition</returns>
        public static bool IsPostposition(this string word)
        {
            word = word.ToLowerInvariant();
            return postpositionList.ContainsExact(word);
        }

        /// <summary>
        /// Whether word is a subject rather than an object (from the context)
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is a subject rather than an object (from the context)</returns>
        public static bool IsSubjectNotObject(this Word word)
        {
            return subjectObjectDetector.IsSubjectNotObject(word);
        }

        /// <summary>
        /// Whether word is a word like: "how, when, where, why" etc
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is a word like: "how, when, where, why" etc</returns>
        public static bool IsQuestionBeginWord(this string word)
        {
            word = word.ToLowerInvariant();
            return questionBeginList.ContainsExact(word);
        }

        /// <summary>
        /// Whether word is a word like: "how, when, where, why" for question ending
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is a word like: "how, when, where, why" etc</returns>
        public static bool IsQuestionEndingWord(this string word)
        {
            word = word.ToLowerInvariant();
            return questionEndingList.ContainsExact(word);
        }

        /// <summary>
        /// Whether word is a subordinating conjunction that can be put before a verb
        /// </summary>
        /// <param name="word">word to analyze</param>
        /// <returns>Whether word is a subordinating conjunction that can be put before a verb</returns>
        public static bool IsSubordinatingConjunctionBeforeSubject(this string word)
        {
            word = word.ToLowerInvariant();
            return subordinatingConjunctionBeforeSubjectList.ContainsExact(word);
        }

        /// <summary>
        /// Whether text contains verb
        /// </summary>
        /// <param name="text">text to analyze</param>
        /// <returns>Whether text contains verb</returns>
        public static bool ContainsVerb(this string text)
        {
            text = text.ToLowerInvariant();
            WordStringStream wordStringStream = new WordStringStream(text);
            foreach (string word in wordStringStream)
                if (Analysis.IsVerb(word))
                    return true;
            return false;
        }
        #endregion
    }
}
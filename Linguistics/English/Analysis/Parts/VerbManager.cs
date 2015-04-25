using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages analysis of verbs
    /// </summary>
    class VerbManager
    {
        #region Parts
        /// <summary>
        /// List of modal verb
        /// </summary>
        private WordList modalVerbList;

        /// <summary>
        /// List of irregular verb
        /// </summary>
        private WordList irregularVerbList;

        /// <summary>
        /// List of regular verb
        /// </summary>
        private WordList regularVerbList;

        /// <summary>
        /// List of undefined verb
        /// </summary>
        private WordList undefinedVerbList;
        #endregion

        #region Constructor
        /// <summary>
        /// Create verb manager
        /// </summary>
        public VerbManager()
        {
            modalVerbList = new WordListFromString(ArtificialArt.Properties.Resources.modalVerbList);
            irregularVerbList = new WordListFromString(ArtificialArt.Properties.Resources.irregularVerbList);
            regularVerbList = new WordListFromString(ArtificialArt.Properties.Resources.regularVerbList);
            undefinedVerbList = new WordListFromString(ArtificialArt.Properties.Resources.undefinedVerbList);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Whether the word is a verb
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is a verb</returns>
        internal bool IsVerb(string word)
        {
            return IsModalVerb(word) || IsIrregularVerb(word) || IsRegularVerb(word) || IsUndefinedVerb(word);
        }

        /// <summary>
        /// Whether the word is in modal verb list
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is in modal verb list</returns>
        internal bool IsModalVerb(string word)
        {
            return modalVerbList.ContainsAsNegativeOrPositiveForm(word);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Whether the word is in irregular verb list
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is in irregular verb list</returns>
        private bool IsIrregularVerb(string word)
        {
            return irregularVerbList.ContainsAsNegativeOrPositiveForm(word);
        }

        /// <summary>
        /// Whether the word is in regular verb list
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is in regular verb list</returns>
        private bool IsRegularVerb(string word)
        {
            return regularVerbList.ContainsAsNegativeOrPositiveForm(word);
        }

        /// <summary>
        /// Whether the word is in undefined verb list
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether the word is in undefined verb list</returns>
        private bool IsUndefinedVerb(string word)
        {
            return undefinedVerbList.ContainsAsNegativeOrPositiveForm(word);
        }
        #endregion
    }
}
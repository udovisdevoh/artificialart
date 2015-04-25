using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Represents a word as a wrapper to a string but with more functionalities
    /// </summary>
    public class Word : IEquatable<Word>
    {
        #region Fields
        /// <summary>
        /// String value
        /// </summary>
        private string stringValue;

        /// <summary>
        /// Delimiter before word
        /// </summary>
        private string leftDelimiter = null;

        /// <summary>
        /// Delimiter after word
        /// </summary>
        private string rightDelimiter = null;

        /// <summary>
        /// Next word
        /// </summary>
        private Word nextWord = null;

        /// <summary>
        /// Previous word
        /// </summary>
        private Word previousWord = null;

        /// <summary>
        /// Whether the word is at the begining of a sentence
        /// </summary>
        private bool isSentenceBegin = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a word from a string
        /// </summary>
        /// <param name="stringValue">string value</param>
        public Word(string stringValue)
        {
            this.stringValue = stringValue;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get word's string value
        /// </summary>
        /// <returns>word's string value</returns>
        public override string ToString()
        {
            return stringValue;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Delimiter before word
        /// </summary>
        public string LeftDelimiter
        {
            get { return leftDelimiter; }
            set { leftDelimiter = value; }
        }

        /// <summary>
        /// Delimiter after word
        /// </summary>
        public string RightDelimiter
        {
            get { return rightDelimiter; }
            set { rightDelimiter = value; }
        }

        /// <summary>
        /// Next word
        /// </summary>
        public Word NextWord
        {
            get { return nextWord; }
            set { nextWord = value; }
        }

        /// <summary>
        /// Previous word
        /// </summary>
        public Word PreviousWord
        {
            get { return previousWord; }
            set { previousWord = value; }
        }

        /// <summary>
        /// Whether the word is at the begining of a sentence
        /// </summary>
        public bool IsSentenceBegin
        {
            get { return isSentenceBegin; }
            set { isSentenceBegin = value; }
        }

        /// <summary>
        /// String value
        /// </summary>
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        #endregion

        #region IEquatable<Word> Members
        /// <summary>
        /// Whether word is the same as the other (case insensitive)
        /// </summary>
        /// <param name="other">other word</param>
        /// <returns>Whether word is the same as the other (case insensitive)</returns>
        public bool Equals(Word other)
        {
            return this.ToString().ToLowerInvariant() == other.ToString().ToLowerInvariant();
        }
        #endregion
    }
}
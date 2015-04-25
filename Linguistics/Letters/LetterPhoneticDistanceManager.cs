using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Manages phonetic distance between letters
    /// </summary>
    internal class LetterPhoneticDistanceManager
    {
        #region Constants
        /// <summary>
        /// When letters are identical
        /// </summary>
        private const float sameLetter = 0.0f;

        /// <summary>
        /// When letters are from same group (for instance: v and f, p and b, é and è, j and y)
        /// </summary>
        private const float sameLetterGroup = 0.125f;

        /// <summary>
        /// When letters are both consonant or vowels
        /// </summary>
        private const float bothVowelOrConsonant = 0.25f;

        /// <summary>
        /// Longest possible distance between letters
        /// </summary>
        private const float longestDistanceLetter = 1.0f;

        /// <summary>
        /// Longest possible distance between undefined characters
        /// </summary>
        private const float longestDistanceUndefined = 2.0f;  
        #endregion

        #region Public Methods
        /// <summary>
        /// Get phonetic distance between two letters
        /// </summary>
        /// <param name="letter1">letter 1</param>
        /// <param name="letter2">letter 2</param>
        /// <returns>phonetic distance</returns>
        public float GetPhoneticDistance(char letter1, char letter2)
        {
            if (letter1 == letter2)
                return sameLetter;
            else if (letter1.IsSameLetterGroup(letter2))
                return sameLetterGroup;
            else if (letter1.IsVowel() == letter2.IsVowel() || letter1.IsConsonant() == letter2.IsConsonant())
                return bothVowelOrConsonant;
            else if (letter1.IsLetter() == letter2.IsLetter())
                return longestDistanceLetter;
            else
                return longestDistanceUndefined;
        }
        #endregion
    }
}

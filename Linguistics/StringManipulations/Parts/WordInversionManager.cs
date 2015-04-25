using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Manages inversions of words
    /// </summary>
    internal class WordInversionManager
    {
        /// <summary>
        /// Invert word positions in string
        /// </summary>
        /// <param name="originalString">original string</param>
        /// <param name="wordPosition1">word1's position</param>
        /// <param name="wordPosition2">word2's position</param>
        /// <returns>String with position of word 1 and word 2 inverted</returns>
        internal string InvertWordPosition(string originalString, int wordPosition1, int wordPosition2)
        {
            WordStream wordStream = new WordStream(originalString);

            string word1Value = wordStream[wordPosition1].ToString();
            string word2Value = wordStream[wordPosition2].ToString();

            wordStream[wordPosition2].StringValue = word1Value;
            wordStream[wordPosition1].StringValue = word2Value;

            return wordStream.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Markov;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages operations and analysys on synonyms and antonyms
    /// </summary>
    internal class SynonymManager
    {
        #region Fields and parts
        /// <summary>
        /// Manages saving and loading matrixes
        /// </summary>
        private XmlMatrixSaverLoader xmlMatrixSaverLoader = new XmlMatrixSaverLoader();

        /// <summary>
        /// Matrix that will contain synonyms
        /// Don't use directly, use synonymMatrix instead
        /// </summary>
        private Matrix _synonymMatrix = null;

        /// <summary>
        /// Matrix that will contain antonyms
        /// Don't use directly, use antonymMatrix instead
        /// </summary>
        private Matrix _antonymMatrix = null;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Invert words to their antonym in proposition
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <param name="desiredOccurenceReplacement">desired occurence replacement</param>
        /// <returns>Proposition with inverted antonym</returns>
        internal string InvertAntonym(string originalProposition, int desiredOccurenceReplacement)
        {
            WordStream wordStream = new WordStream(originalProposition);

            int replacementCount = 0;
            foreach (Word word in wordStream)
            {
                if (replacementCount < desiredOccurenceReplacement)
                {
                    string foundAntoym = TryFindBestAntonymOrSynonym(word.StringValue, antonymMatrix);
                    if (foundAntoym != null)
                    {
                        word.StringValue = foundAntoym;
                        replacementCount++;
                    }
                }
            }

            return wordStream.ToString();
        }

        /// <summary>
        /// Whether there is a know antonym to replace a word in original proposition
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <returns>Whether there is a know antonym to replace a word in original proposition</returns>
        internal bool ContainsAntonym(string originalProposition)
        {
            WordStringStream wordStringStream = new WordStringStream(originalProposition);

            foreach (string word in wordStringStream)
                if (antonymMatrix.NormalData.ContainsKey(word.ToLowerInvariant()))
                    return true;

            foreach (string word in wordStringStream)
                if (antonymMatrix.ReversedData.ContainsKey(word.ToLowerInvariant()))
                    return true;

            return false;
        }

        /// <summary>
        /// Try find best synonym for provided word or return null if none found
        /// </summary>
        /// <param name="originalWord">original word</param>
        /// <returns>Try find an synonym for provided word or return null if none found</returns>
        internal string TryFindBestSynonym(string originalWord)
        {
            return this.TryFindBestAntonymOrSynonym(originalWord, synonymMatrix);
        }

        /// <summary>
        /// Try find best antonym for provided word or return null if none found
        /// </summary>
        /// <param name="originalWord">original word</param>
        /// <returns>Try find an antonym for provided word or return null if none found</returns>
        internal string TryFindBestAntonym(string originalWord)
        {
            return this.TryFindBestAntonymOrSynonym(originalWord, antonymMatrix);
        }

        /// <summary>
        /// Try replace each word of text to a valid antonym
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>text with replaced each word of text to a valid antonym</returns>
        internal string TryConvertTextToAntonym(string text)
        {
            return this.TryConvertTextToAntonymOrAntonym(text, antonymMatrix);
        }

        /// <summary>
        /// Try replace each word of text to a valid synonym
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>text with replaced each word of text to a valid synonym</returns>
        internal string TryConvertTextToSynonym(string text)
        {
            return this.TryConvertTextToAntonymOrAntonym(text, synonymMatrix);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Try find best antonym or synonym for provided word or return null if none found
        /// </summary>
        /// <param name="originalWord">original word</param>
        /// <param name="matrix">matrix to use</param>
        /// <returns>Try find an synonym or antonym for provided word or return null if none found</returns>
        private string TryFindBestAntonymOrSynonym(string originalWord, Matrix matrix)
        {
            originalWord = originalWord.ToLowerInvariant().Trim();

            Dictionary<string, float> matrixData;
            string bestYet = null;
            double bestValue = 0f;

            if (matrix.NormalData.TryGetValue(originalWord, out matrixData))
            {
                foreach (KeyValuePair<string, float> wordAndValue in matrixData)
                {
                    string word = wordAndValue.Key;
                    float value = wordAndValue.Value;

                    if (bestYet == null || value > bestValue)
                    {
                        bestYet = word;
                        bestValue = value;
                    }
                }
            }

            if (matrix.ReversedData.TryGetValue(originalWord, out matrixData))
            {
                foreach (KeyValuePair<string, float> wordAndValue in matrixData)
                {
                    string word = wordAndValue.Key;
                    float value = wordAndValue.Value;

                    if (bestYet == null || value > bestValue)
                    {
                        bestYet = word;
                        bestValue = value;
                    }
                }
            }


            return bestYet;
        }

        /// <summary>
        /// Try replace each word in text with a synonym or antonym depending on provided matrix
        /// </summary>
        /// <param name="text">original text</param>
        /// <param name="synonymOrAntonymMatrix">provided matrix</param>
        /// <returns>text for which each word is replaced with a synonym or antonym depending on provided matrix</returns>
        private string TryConvertTextToAntonymOrAntonym(string text, Matrix synonymOrAntonymMatrix)
        {
            WordStream wordStream = new WordStream(text);

            string newText = string.Empty;

            foreach (Word word in wordStream)
            {
                string synonymOrAntonym = TryFindBestAntonymOrSynonym(word.ToString().ToLowerInvariant(), synonymOrAntonymMatrix);
                if (synonymOrAntonym != null)
                    word.StringValue = synonymOrAntonym;

                newText += word.StringValue;

                if (word.RightDelimiter != null)
                    newText += word.RightDelimiter;
            }

            return newText;
        }
        #endregion

        #region Lazy initializations
        /// <summary>
        /// Matrix that will contain synonyms
        /// </summary>
        private Matrix synonymMatrix
        {
            get
            {
                if (_synonymMatrix == null)
                    _synonymMatrix = xmlMatrixSaverLoader.LoadString(ArtificialArt.Properties.Resources.synonymList);

                return _synonymMatrix;
            }
        }

        /// <summary>
        /// Matrix that will contain antonyms
        /// </summary>
        private Matrix antonymMatrix
        {
            get
            {
                if (_antonymMatrix == null)
                    _antonymMatrix = xmlMatrixSaverLoader.LoadString(ArtificialArt.Properties.Resources.antonymList);

                return _antonymMatrix;
            }
        }
        #endregion
    }
}
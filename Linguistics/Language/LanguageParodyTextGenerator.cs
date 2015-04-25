using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Markov;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Generate text in various languages
    /// </summary>
    internal class LanguageParodyTextGenerator
    {
        #region Fields
        /// <summary>
        /// Language matrix collection
        /// </summary>
        private LanguageMatrixCollection languageMatrixCollection;
        #endregion

        #region Constructor
        /// <summary>
        /// Build text generator from language matrix collection
        /// </summary>
        /// <param name="languageMatrixCollection">language matrix collection</param>
        public LanguageParodyTextGenerator(LanguageMatrixCollection languageMatrixCollection)
        {
            this.languageMatrixCollection = languageMatrixCollection;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Generate text in specified language
        /// </summary>
        /// <param name="languageName">specified language</param>
        /// <param name="letterCount">how many letter</param>
        /// <param name="random">random number generator</param>
        /// <returns>generated text</returns>
        internal string GenerateText(LanguageNames languageName, int letterCount, Random random)
        {
            string languageNameString = LanguageManager.GetLanguageNameString(languageName);
            LanguageMatrix languageMatrix = languageMatrixCollection[languageNameString];

            string previousCharPair = languageMatrix.GetRandomStartingPair(random);

            string text = previousCharPair;

            for (int charCounter = 0; charCounter < letterCount; charCounter++)
            {
                Dictionary<string, float> row;
                while (!languageMatrix.NormalData.TryGetValue(previousCharPair, out row))
                {
                    previousCharPair = languageMatrix.GetRandomStartingPair(random);
                }

                string letter = Probabilities.GetPonderatedRandom(row, random);

                text += letter;

                previousCharPair = previousCharPair.Substring(1) + letter;
            }

            return text.Trim().ToLowerInvariant();
        }
        #endregion
    }
}

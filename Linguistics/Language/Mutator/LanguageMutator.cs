using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Linguistics;
using ArtificialArt.Markov;

namespace ArtificialArt.Linguistics.Mutator
{
    /// <summary>
    /// Convert a text to something that looks like another language
    /// </summary>
    internal class LanguageMutator
    {
        #region Constants
        /// <summary>
        /// Probability to force same letter group
        /// </summary>
        private const double forceSameLetterGroupProbability = 0.95;
        #endregion

        #region Fields
        /// <summary>
        /// Language Detector
        /// </summary>
        private LanguageDetector languageDetector;

        /// <summary>
        /// Language matrix collection
        /// </summary>
        private LanguageMatrixCollection languageMatrixCollection;
        #endregion

        #region Constructor
        /// <summary>
        /// Create language mutator
        /// </summary>
        /// <param name="languageDetector">language detector</param>
        /// <param name="languageMatrixCollection">language matrix collection</param>
        public LanguageMutator(LanguageDetector languageDetector, LanguageMatrixCollection languageMatrixCollection)
        {
            this.languageDetector = languageDetector;
            this.languageMatrixCollection = languageMatrixCollection;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Convert a text to something that looks like another language
        /// </summary>
        /// <param name="textSource">text source</param>
        /// <param name="desiredLanguageName">name of the desired language</param>
        /// <param name="random">random number generator</param>
        /// <returns>something that looks like another language</returns>
        internal string Translate(string textSource, string desiredLanguageName, Random random)
        {
            LanguageMatrix desiredLanguage = languageMatrixCollection[desiredLanguageName];

            textSource = textSource.ToUpperInvariant();
            string previousPair = string.Empty;

            string originalText = textSource;

            for (int passCount = 0; passCount < 100; passCount++)
            {
                string newText = string.Empty;

                for (int index = 0; index < textSource.Length; index++)
                {
                    char currentLetter = textSource[index];
                    char newLetter = currentLetter;

                    if (previousPair.Length == 2)
                    {
                        if (currentLetter.IsLetter())
                        {
                            Dictionary<string, float> row;
                            if (desiredLanguage.NormalData.TryGetValue(previousPair, out row))
                            {
                                newLetter = row.GetPonderatedRandom(random)[0];

                                if (!newLetter.IsLetter() || (random.NextDouble() <= forceSameLetterGroupProbability && !newLetter.IsSameLetterGroup(originalText[index])))
                                {
                                    newLetter = currentLetter;
                                }
                                else
                                {
                                    if (index > 0 && index < textSource.Length - 1 && newLetter != currentLetter)
                                    {
                                        string otherPreviousPair = textSource[index - 1].ToString() + newLetter.ToString();

                                        if (desiredLanguage.NormalData.TryGetValue(otherPreviousPair, out row))
                                        {
                                            char otherNewLetter = row.GetPonderatedRandom(random)[0];
                                            if (otherNewLetter != textSource[index + 1])
                                            {
                                                newLetter = currentLetter;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    newText += newLetter;

                    previousPair += currentLetter;
                    if (previousPair.Length == 3)
                        previousPair = previousPair.Substring(1, 2);
                }
                textSource = newText;
            }

            return textSource.ToLowerInvariant();
        }
        #endregion
    }
}

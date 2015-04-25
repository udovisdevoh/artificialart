using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Detects languages
    /// </summary>
    internal class LanguageDetector
    {
        #region Fields
        /// <summary>
        /// Collection of language matrixes
        /// </summary>
        private LanguageMatrixCollection languageMatrixCollection;
        #endregion

        #region Constructor
        /// <summary>
        /// Build text language detector from language matrix collection
        /// </summary>
        /// <param name="languageMatrixCollection">language matrix collection</param>
        public LanguageDetector(LanguageMatrixCollection languageMatrixCollection)
        {
            this.languageMatrixCollection = languageMatrixCollection;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Detect which language is present in text or null if nothing found
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>language's name</returns>
        public string DetectLanguage(string text)
        {
            string detectedLanguageName = null;

            double bestDifference = 0.0;
            
            LanguageMatrix languageMatrixFromText = new LanguageMatrix(text);

            foreach (string currentLanguageMatrixName in languageMatrixCollection.Keys)
            {
                LanguageMatrix currentLanguageMatrix = languageMatrixCollection[currentLanguageMatrixName];

                double currentDifference = languageMatrixFromText.CompareTo(currentLanguageMatrix,true);
                if (detectedLanguageName == null || currentDifference < bestDifference)
                {
                    bestDifference = currentDifference;
                    detectedLanguageName = currentLanguageMatrixName;
                }
            }

            return detectedLanguageName;
        }
        #endregion
    }
}

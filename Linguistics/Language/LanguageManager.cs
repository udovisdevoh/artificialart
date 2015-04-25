using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Linguistics.Mutator;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Manages language detection and conversion
    /// </summary>
    public static class LanguageManager
    {
        #region Fields
        /// <summary>
        /// Language detector
        /// </summary>
        private static LanguageDetector languageDetector;

        /// <summary>
        /// Text generator
        /// </summary>
        private static LanguageParodyTextGenerator languageParodyTextGenerator;

        /// <summary>
        /// Convert a text to something that looks like another language
        /// </summary>
        private static LanguageMutator languageMutator;
        #endregion

        #region Constructor
        /// <summary>
        /// Static constructor
        /// </summary>
        static LanguageManager()
        {
            LanguageMatrixCollection languageMatrixCollection = new LanguageMatrixCollection();
            languageDetector = new LanguageDetector(languageMatrixCollection);
            languageParodyTextGenerator = new LanguageParodyTextGenerator(languageMatrixCollection);
            languageMutator = new LanguageMutator(languageDetector,languageMatrixCollection);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Detect language from text string
        /// </summary>
        /// <param name="text">text string</param>
        /// <returns>detected language</returns>
        public static string DetectLanguage(this string text)
        {
            return languageDetector.DetectLanguage(text);
        }

        /// <summary>
        /// Generate language parody text
        /// </summary>
        /// <param name="languageName">language</param>
        /// <param name="letterCount">how many letter</param>
        /// <param name="random">random number generator</param>
        /// <returns>random text</returns>
        public static string GenerateLanguageParodyText(LanguageNames languageName, int letterCount, Random random)
        {
            return languageParodyTextGenerator.GenerateText(languageName, letterCount, random);
        }

        /// <summary>
        /// Translate a text to something that looks like something from another language
        /// </summary>
        /// <param name="textSource">text source</param>
        /// <param name="desiredLanguageName">desired language name</param>
        /// <param name="random">random number generator</param>
        /// <returns>something that looks like something from another language</returns>
        public static string TranslateByParody(this string textSource, LanguageNames desiredLanguageName, Random random)
        {
            string languageNameString = GetLanguageNameString(desiredLanguageName);
            return languageMutator.Translate(textSource, languageNameString, random);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build language name as string
        /// </summary>
        /// <param name="languageName">language name from enum</param>
        /// <returns>language name as string</returns>
        internal static string GetLanguageNameString(LanguageNames languageName)
        {
            string languageNameString = null;
            if (languageName == LanguageNames.Dutch)
                languageNameString = "dutch";
            else if (languageName == LanguageNames.English)
                languageNameString = "english";
            else if (languageName == LanguageNames.French)
                languageNameString = "french";
            else if (languageName == LanguageNames.German)
                languageNameString = "german";
            else if (languageName == LanguageNames.Italian)
                languageNameString = "italian";
            else if (languageName == LanguageNames.Polish)
                languageNameString = "polish";
            else if (languageName == LanguageNames.Portuguese)
                languageNameString = "portuguese";
            else if (languageName == LanguageNames.Spanish)
                languageNameString = "spanish";
            return languageNameString;
        }
        #endregion
    }
}

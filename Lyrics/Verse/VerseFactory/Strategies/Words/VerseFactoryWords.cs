using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    internal class VerseFactoryWords : AbstractVerseFactory
    {
        #region Constants
        /// <summary>
        /// Default language code
        /// </summary>
        private const string defaultLanguageCode = "en";
        #endregion

        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        protected VerseConstructionSettings verseConstructionSettings;

        /// <summary>
        /// Creation memory
        /// </summary>
        protected CreationMemory creationMemory;

        /// <summary>
        /// Language Code
        /// </summary>
        private string languageCode = defaultLanguageCode;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        /// <param name="creationMemory">creation memory (to remember which themes were used the most</param>
        public VerseFactoryWords(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
        {
            this.verseConstructionSettings = verseConstructionSettings;
            this.creationMemory = creationMemory;
        }
        #endregion

        #region Public Methods
        public override Verse Build(Verse previousVerse)
        {
            string verseContent = string.Empty;
            Theme currentTheme;
            string currentWord;

            while (verseContent.Length < verseConstructionSettings.DesiredLength - 4)
            {
                currentTheme = verseConstructionSettings.ThemeList.GetRandomTheme(verseConstructionSettings.Random);
                currentWord = currentTheme.GetRandomWord(verseConstructionSettings.Random);
                verseContent += " " + currentWord;
            }

            verseContent = verseContent.Trim();

            return new Verse(verseContent);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Language Code
        /// </summary>
        public override string LanguageCode
        {
            get
            {
                return languageCode;
            }
            set
            {
                languageCode = value;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Produces verses
    /// </summary>
    internal class VerseFactory : AbstractVerseFactory
    {
        #region Constants
        /// <summary>
        /// Default language code
        /// </summary>
        private const string defaultLanguageCode = "en";
        #endregion

        #region Fields and Parts
        /// <summary>
        /// Construction settings
        /// </summary>
        private VerseConstructionSettings verseConstructionSettings;

        /// <summary>
        /// To remember previous verse theme words not to repeat them too often
        /// </summary>
        private CreationMemory creationMemory;

        /// <summary>
        /// Straight verse factory
        /// </summary>
        private VerseFactoryStraight verseFactoryStraight;

        /// <summary>
        /// Word verse factory
        /// </summary>
        private VerseFactoryWords verseFactoryWords;

        /// <summary>
        /// Theme loader
        /// </summary>
        private ThemeLoader themeLoader = new ThemeLoader();

        /// <summary>
        /// Language code
        /// </summary>
        private string languageCode = defaultLanguageCode;

        /// <summary>
        /// Lyric source directory
        /// </summary>
        private string lyricSourcePath;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public VerseFactory(string lyricSourcePath, string languageCode)
        {
            this.lyricSourcePath = lyricSourcePath;
            string lyricSourceFileName = lyricSourcePath + "lyrics." + languageCode + ".txt";
            verseConstructionSettings = new VerseConstructionSettings(lyricSourceFileName);
            creationMemory = new CreationMemory();

            verseFactoryStraight = new VerseFactoryStraight(verseConstructionSettings, creationMemory);
            verseFactoryWords = new VerseFactoryWords(verseConstructionSettings, creationMemory);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            Verse verse;
            if (verseConstructionSettings.Algorithm == VerseConstructionSettings.AlgorithmStraight)
            {
                verse = verseFactoryStraight.Build(previousVerse);
            }
            else if (verseConstructionSettings.Algorithm == VerseConstructionSettings.AlgorithmWords)
            {
                verse = verseFactoryWords.Build(previousVerse);
            }
            else
            {
                throw new VerseFactoryException("Couldn't find proper algorithm to generate verse");
            }

            if (verse == null)
            {
                verse = verseFactoryStraight.Build(previousVerse);
            }

            creationMemory.Remember(verse, verseConstructionSettings);

            return verse;
        }

        /// <summary>
        /// Add theme to construction settings
        /// </summary>
        /// <param name="theme">theme to add</param>
        public void AddTheme(Theme theme)
        {
            verseConstructionSettings.AddTheme(theme);
        }

        /// <summary>
        /// Add theme to construction settings
        /// </summary>
        /// <param name="themeName">theme to add</param>
        public void AddTheme(string themeName)
        {
            AddTheme(themeLoader.Load(themeName));
        }

        /// <summary>
        /// Censor a theme in construction settings
        /// </summary>
        /// <param name="theme">theme to censor</param>
        public void CensorTheme(Theme theme)
        {
            verseConstructionSettings.CensorTheme(theme);
        }

        /// <summary>
        /// Censor a theme in construction settings
        /// </summary>
        /// <param name="themeName">theme to censor</param>
        public void CensorTheme(string themeName)
        {
            CensorTheme(themeLoader.Load(themeName));
        }

        /// <summary>
        /// Reset themes
        /// </summary>
        public void ResetThemes()
        {
            verseConstructionSettings.ResetThemes();
        }

        /// <summary>
        /// Clear creation memory
        /// </summary>
        public void ClearCreationMemory()
        {
            creationMemory.Clear();
        }

        /// <summary>
        /// Clear desired and undesired themes
        /// </summary>
        public void ClearThemes()
        {
            verseConstructionSettings.ClearThemes();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Desired length in char for verse
        /// </summary>
        public short DesiredLength
        {
            get { return verseConstructionSettings.DesiredLength; }
            set { verseConstructionSettings.DesiredLength = value; }
        }

        /// <summary>
        /// Set which algorithm to use
        /// </summary>
        public byte Algorithm
        {
            get { return verseConstructionSettings.Algorithm; }
            set { verseConstructionSettings.Algorithm = value; }
        }

        /// <summary>
        /// Theme name list
        /// </summary>
        public IEnumerable<string> SelectableThemeNameList
        {
            get { return themeLoader.ThemeNameList; }
        }

        /// <summary>
        /// Desired themes
        /// </summary>
        public ThemeList ThemeList
        {
            get { return verseConstructionSettings.ThemeList; }
        }

        /// <summary>
        /// Undesired themes
        /// </summary>
        public ThemeList ThemeBlackList
        {
            get { return verseConstructionSettings.ThemeBlackList; }
        }

        /// <summary>
        /// Current language code
        /// </summary>
        public override string LanguageCode
        {
            get
            {
                return languageCode;
            }

            set
            {
                if (languageCode != value)
                {
                    creationMemory.Clear();
                    themeLoader = new ThemeLoader(value);
                    Evaluator.ThemeLoader = themeLoader;
                }

                languageCode = value;
                verseConstructionSettings.SetLanguageCode(lyricSourcePath, value, themeLoader);
                verseFactoryStraight.LanguageCode = value;
                verseFactoryWords.LanguageCode = value;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    internal class VerseConstructionSettings
    {
        #region Constants
        /// <summary>
        /// Identifies straight algorithm
        /// </summary>
        public const byte AlgorithmStraight = 0;

        /// <summary>
        /// Identifies splicing based algorithm
        /// </summary>
        public const byte AlgorithmSplice = 1;

        /// <summary>
        /// Identifies cryptic char based markov chain algorithm
        /// </summary>
        public const byte AlgorithmCryptic = 2;

        /// <summary>
        /// Identifies algorithm working with analogies
        /// </summary>
        public const byte AlgorithmAnalogy = 3;

        /// <summary>
        /// Identifies algorithm working with interleaved analogies
        /// </summary>
        public const byte AlgorithmInterleavedAnalogy = 4;

        /// <summary>
        /// Identifies algorithm working with rhymes
        /// </summary>
        public const byte AlgorithmRhyme = 5;

        /// <summary>
        /// Identifies algorithm working with words
        /// </summary>
        public const byte AlgorithmWords = 6;

        /// <summary>
        /// Default desired length in char
        /// </summary>
        public const byte DefaultDesiredLength = 32;

        /// <summary>
        /// Reversed order lyrics file name
        /// </summary>
        public const string reversedLyricsFileName = "textSources/lyrics.en.reversed.txt";

        /// <summary>
        /// Default language code
        /// </summary>
        private const string defaultLanguageCode = "en";
        #endregion

        #region Fields
        /// <summary>
        /// Current algorithm
        /// </summary>
        private byte algorithm;

        /// <summary>
        /// Desired length for verse
        /// </summary>
        private short desiredLength;

        /// <summary>
        /// Internal theme list, do not use directly : lazy initialization
        /// </summary>
        private ThemeList themeList = new ThemeList();

        /// <summary>
        /// Internal theme black list, do not use directly : lazy initialization
        /// </summary>
        private ThemeList themeBlackList = new ThemeList();

        /// <summary>
        /// Straight ordered lyrics
        /// </summary>
        private LyricSource lyricSource;

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Language Code
        /// </summary>
        private string languageCode = defaultLanguageCode;
        #endregion

        #region Constructor
        public VerseConstructionSettings(string lyricSourceFileName)
        {
            lyricSource = new LyricSource(lyricSourceFileName);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Reset current selected themes
        /// </summary>
        public void ResetThemes()
        {
            themeList.Clear();
            themeBlackList.Clear();
        }

        /// <summary>
        /// Add a theme to current theme list
        /// </summary>
        /// <param name="theme">theme to add</param>
        public void AddTheme(Theme theme)
        {
            if (themeBlackList.Contains(theme))
                themeBlackList.Remove(theme);
            themeList.Add(theme);
        }

        /// <summary>
        /// Censor a theme 
        /// </summary>
        /// <param name="theme">theme to censor</param>
        public void CensorTheme(Theme theme)
        {
            if (themeList.Contains(theme))
                themeList.Remove(theme);
            themeBlackList.Add(theme);
        }

        public int GenerateRandomRhymeSpan()
        {
            return random.Next(1, 2);
        }

        /// <summary>
        /// Clear desired and undesired themes
        /// </summary>
        public void ClearThemes()
        {
            themeList.Clear();
            themeBlackList.Clear();
        }

        /// <summary>
        /// Set language code
        /// </summary>
        /// <param name="lyricSourcePath">lyric source directory</param>
        /// <param name="languageCode">language code</param>
        /// <param name="themeLoader">theme loader</param>
        public void SetLanguageCode(string lyricSourcePath, string languageCode, ThemeLoader themeLoader)
        {
            this.languageCode = languageCode;
            themeList.SetLanguageCode(languageCode, themeLoader);
            themeBlackList.SetLanguageCode(languageCode, themeLoader);
            lyricSource = new LyricSource(lyricSourcePath + "lyrics." + languageCode + ".txt");
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Set which algorithm to use
        /// </summary>
        public byte Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }

        /// <summary>
        /// Desired length in char for verse
        /// </summary>
        public short DesiredLength
        {
            get { return desiredLength; }
            set { desiredLength = value; }
        }

        /// <summary>
        /// Ordered lyric source
        /// </summary>
        public LyricSource LyricSource
        {
            get { return lyricSource; }
        }

        /// <summary>
        /// Desired themes
        /// </summary>
        public ThemeList ThemeList
        {
            get { return themeList; }
            set { themeList = value; }
        }

        /// <summary>
        /// Undesired themes
        /// </summary>
        public ThemeList ThemeBlackList
        {
            get { return themeBlackList; }
            set { themeBlackList = value; }
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        public Random Random
        {
            get { return random; }
        }
        #endregion
    }
}

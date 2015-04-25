using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Produces whole songs
    /// </summary>
    public class LyricSongFactory : ILyricSongFactory
    {
        #region Constants
        /// <summary>
        /// How many bars to generate by default
        /// </summary>
        private const int defaultBarCount = 16;

        /// <summary>
        /// How many letter per line by default
        /// </summary>
        private const int defaultCharCountPerLine = 32;

        /// <summary>
        /// How many bar per chorus by default
        /// </summary>
        private const int defaultBarCountPerChorus = 2;

        /// <summary>
        /// Default language code
        /// </summary>
        private const string defaultLanguageCode = "en";
        #endregion

        #region Fields and parts
        /// <summary>
        /// For each bar, the level of intensity (0 means nothing 0.5 means moderate verse, 0.75 means intense verse, 1 means chorus)
        /// </summary>
        private List<float> listBarIntensity = new List<float>();

        /// <summary>
        /// For each bar, how many letter to set
        /// </summary>
        private List<short> listBarLetterCount = new List<short>();

        /// <summary>
        /// How many bar to generate
        /// </summary>
        private int barCount = defaultBarCount;

        /// <summary>
        /// How many bar per chorus
        /// </summary>
        private int barCountPerChorus = defaultBarCountPerChorus;

        /// <summary>
        /// Builds verses
        /// </summary>
        private VerseFactory verseFactory;

        /// <summary>
        /// Lyric source file name
        /// </summary>
        private string lyricSourceFileName;

        /// <summary>
        /// How many line per bar
        /// </summary>
        private int lineCountPerBar = 1;

        /// <summary>
        /// Language code
        /// </summary>
        private string languageCode = defaultLanguageCode;

        /// <summary>
        /// Lyric source path
        /// </summary>
        private string lyricSourcePath;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a song factory from lyric source file name
        /// </summary>
        /// <param name="lyricSourcePath">lyric source path</param>
        /// <param name="languageCode">language code</param>
        public LyricSongFactory(string lyricSourcePath, string languageCode)
        {
            this.lyricSourcePath = lyricSourcePath;
            string lyricSourceFileName = lyricSourcePath + "lyrics." + languageCode + ".txt";
            verseFactory = new VerseFactory(lyricSourcePath, languageCode);
            verseFactory.DesiredLength = defaultCharCountPerLine;
            this.lyricSourceFileName = lyricSourceFileName;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add theme to construction settings
        /// </summary>
        /// <param name="themeName">theme to add</param>
        public void AddTheme(string themeName)
        {
            verseFactory.AddTheme(themeName);
        }

        /// <summary>
        /// Censor a theme in construction settings
        /// </summary>
        /// <param name="themeName">theme to censor</param>
        public void CensorTheme(string themeName)
        {
            verseFactory.CensorTheme(themeName);
        }

        /// <summary>
        /// Specify the bar settings at specified index
        /// </summary>
        /// <param name="barIndex">index</param>
        /// <param name="intensity">intensity</param>
        /// <param name="letterCount">how many letter in bar</param>
        public void SetBarSettings(int barIndex, float intensity, short letterCount)
        {
            SetBarIntensity(barIndex, intensity);
            SetBarLetterCount(barIndex, letterCount);
        }

        /// <summary>
        /// Specify the bar char count at specified index
        /// </summary>
        /// <param name="barIndex">index</param>
        /// <param name="letterCount">intensity</param>
        public void SetBarLetterCount(int barIndex, short letterCount)
        {
            if (barIndex < 0)
                throw new Exception("Invalid bar index " + barIndex);

            while (listBarLetterCount.Count < barIndex + 1)
                listBarLetterCount.Add(defaultCharCountPerLine);

            listBarLetterCount[barIndex] = letterCount;

            if (listBarIntensity.Count > barCount)
                barCount = listBarIntensity.Count;

            if (listBarLetterCount.Count > barCount)
                barCount = listBarLetterCount.Count;
        }

        /// <summary>
        /// Specify the bar intensity at specified index
        /// </summary>
        /// <param name="barIndex">index</param>
        /// <param name="intensity">intensity</param>
        public void SetBarIntensity(int barIndex, float intensity)
        {
            if (barIndex < 0)
                throw new Exception("Invalid bar index " + barIndex);

            while (listBarIntensity.Count < barIndex + 1)
                listBarIntensity.Add(0.0f);

            listBarIntensity[barIndex] = intensity;

            if (listBarIntensity.Count > barCount)
                barCount = listBarIntensity.Count;

            if (listBarLetterCount.Count > barCount)
                barCount = listBarLetterCount.Count;
        }

        /// <summary>
        /// Bar's letter count
        /// </summary>
        /// <param name="barIndex">bar's index</param>
        /// <returns>Bar's letter count</returns>
        public int GetBarLetterCount(int barIndex)
        {
            if (barIndex >= listBarLetterCount.Count)
                return defaultBarCount;

            return listBarLetterCount[barIndex];
        }

        /// <summary>
        /// Bar's intensity
        /// </summary>
        /// <param name="barIndex">bar's index</param>
        /// <returns>Bar's intensity</returns>
        public float GetBarIntensity(int barIndex)
        {
            if (barIndex >= listBarIntensity.Count)
                return 0f;

            return listBarIntensity[barIndex];
        }

        /// <summary>
        /// Build song from selected themes
        /// </summary>
        /// <returns>List of string for each line</returns>
        public List<string> Build()
        {
            Verse verse;

            List<string> song = new List<string>();
            List<string> chorus = new List<string>();
            int chorusPointer = 0;

            for (int i = 0; i < barCount ; i++)
            {
                for (int j = 0; j < lineCountPerBar; j++)
                {
                    bool isChorus = listBarIntensity[i] >= 0.95;
                    bool isSilence = listBarLetterCount[i] < 1 || listBarIntensity[i] <= 0.05;

                    if (isSilence)
                    {
                        song.Add("");
                    }
                    else if (isChorus)
                    {
                        if (chorus.Count < barCountPerChorus)
                        {
                            verseFactory.DesiredLength = listBarLetterCount[i];
                            verse = verseFactory.Build();
                            chorus.Add(verse.ToString());
                        }

                        song.Add(chorus[chorusPointer]);
                        chorusPointer++;
                        if (chorusPointer >= barCountPerChorus)
                            chorusPointer = 0;
                    }
                    else
                    {
                        verseFactory.DesiredLength = listBarLetterCount[i];
                        verse = verseFactory.Build();
                        song.Add(verse.ToString());
                    }
                }
            }

            return song;
        }

        /// <summary>
        /// Clear desired and undesired themes
        /// </summary>
        public void ClearThemes()
        {
            verseFactory.ClearThemes();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Theme name list
        /// </summary>
        public IEnumerable<string> SelectableThemeNameList
        {
            get { return verseFactory.SelectableThemeNameList; }
        }

        /// <summary>
        /// How many bars to generate
        /// </summary>
        public int BarCount
        {
            get { return barCount; }
            set { barCount = value; }
        }

        /// <summary>
        /// Desired length in char for verse
        /// </summary>
        public short CharCountPerLine
        {
            get { return verseFactory.DesiredLength; }
            set { verseFactory.DesiredLength = value; }
        }

        /// <summary>
        /// How many bar per chorus
        /// </summary>
        public int BarCountPerChorus
        {
            get
            {
                return barCountPerChorus;
            }
            set
            {
                barCountPerChorus = value;
            }
        }

        /// <summary>
        /// Desired themes
        /// </summary>
        public List<string> ThemeList
        {
            get { return verseFactory.ThemeList.Keys; }
        }

        /// <summary>
        /// Undesired themes
        /// </summary>
        public List<string> ThemeBlackList
        {
            get { return verseFactory.ThemeBlackList.Keys; }
        }

        /// <summary>
        /// Lyric source file name
        /// </summary>
        public string LyricSourceFileName
        {
            get { return lyricSourceFileName; }
        }

        /// <summary>
        /// Line count per bar
        /// </summary>
        public int LineCountPerBar
        {
            set
            {
                lineCountPerBar = value;
            }
        }

        /// <summary>
        /// Current language code
        /// </summary>
        public string LanguageCode
        {
            get
            {
                return languageCode;
            }

            set
            {
                languageCode = value;
                verseFactory.LanguageCode = value;
                lyricSourceFileName = "lyrics." + languageCode + ".txt";
            }
        }

        /// <summary>
        /// Lyric source folder
        /// </summary>
        public string LyricSourcePath
        {
            get { return lyricSourcePath; }
        }
        #endregion
    }
}

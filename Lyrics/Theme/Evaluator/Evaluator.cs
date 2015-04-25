using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Use this to match strings with themes
    /// </summary>
    internal static class Evaluator
    {
        #region Fields
        /// <summary>
        /// Anything but a letter
        /// </summary>
        private static Regex notALetter = new Regex(@"[^a-zA-Z]");

        /// <summary>
        /// Anything but a letter or space
        /// </summary>
        private static Regex notALetterNorSpace = new Regex(@"[^a-zA-Z ]");

        /// <summary>
        /// Theme Loader
        /// </summary>
        private static ThemeLoader themeLoader = new ThemeLoader();
        #endregion

        #region Public Methods
        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <param name="desiredLength">desired length</param>
        /// <param name="random">random number generator</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static int GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList, short desiredLength, Random random)
        {
            return GetScore(currentVerse, themeList, blackThemeList, desiredLength, null, random);
        }

        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <param name="desiredLength">desired length</param>
        /// <param name="creationMemory">creation memory (can be null)</param>
        /// <param name="random">random number generator</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static int GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList, short desiredLength, CreationMemory creationMemory, Random random)
        {
            return GetScore(currentVerse, themeList, blackThemeList, desiredLength, creationMemory, null, random);
        }

        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <param name="desiredLength">desired length</param>
        /// <param name="creationMemory">creation memory (can be null)</param>
        /// <param name="versesToRhymeWith">facultative (can be null) list of verse to rhyme with</param>
        /// <param name="random">random number generator</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static int GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList, short desiredLength, CreationMemory creationMemory, Queue<Verse> versesToRhymeWith, Random random)
        {
            int score = 0;
            score += Match(currentVerse.ToString(), themeList, creationMemory);
            score -= Match(currentVerse.ToString(), blackThemeList);

            score = score - Math.Abs(notALetterNorSpace.Replace(currentVerse.ToString(), "").Length - desiredLength);

            score += random.Next(-5, 5);

            return score;
        }

        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static int GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList)
        {
            return GetScore(currentVerse, themeList, blackThemeList, null);
        }

        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <param name="creationMemory">creation memory (can be null)</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static int GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList, CreationMemory creationMemory)
        {
            int score = 0;
            score += Match(currentVerse.ToString(), themeList, creationMemory);
            score -= Match(currentVerse.ToString(), blackThemeList);
            return score;
        }

        /// <summary>
        /// Get theme words from verse
        /// </summary>
        /// <param name="verse">verse to parse</param>
        /// <param name="themeList">list of themes</param>
        /// <returns>theme words from verse</returns>
        public static IEnumerable<string> GetThemeWords(Verse verse, ThemeList themeList)
        {
            List<string> themeWords = new List<string>();

            foreach (string word in verse.WordList)
                if (themeList.Contains(word))
                    themeWords.Add(word);

            return themeWords;
        }

        /// <summary>
        /// Count how many of each theme per theme in verse
        /// </summary>
        /// <param name="verse">verse</param>
        /// <param name="themeList">theme list</param>
        /// <returns>how many of each theme per theme in verse</returns>
        public static Dictionary<string, int> CountOccurencePerTheme(Verse verse, ThemeList themeList)
        {
            Dictionary<string, int> occurenceCountPerTheme = new Dictionary<string, int>();

            int count;

            foreach (Theme theme in themeList)
            {
                foreach (string word in verse.WordList)
                {
                    if (theme.Contains(word))
                    {
                        if (!occurenceCountPerTheme.TryGetValue(theme.Name, out count))
                            occurenceCountPerTheme.Add(theme.Name, 0);

                        occurenceCountPerTheme[theme.Name]++;
                    }
                }
            }

            return occurenceCountPerTheme;
        }

        /// <summary>
        /// List of theme for word
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>List of theme for word</returns>
        public static ThemeList GetThemeList(string word)
        {
            ThemeList selectedThemeList = new ThemeList();
            foreach (Theme theme in themeLoader.ThemeList)
                if (theme.Contains(word))
                    selectedThemeList.Add(theme);
            return selectedThemeList;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// How mutch the verse line matches provided theme list
        /// </summary>
        /// <param name="verseLine">verse line</param>
        /// <param name="themeList">provided theme list</param>
        /// <returns>how mutch the verse line matches provided theme list</returns>
        private static int Match(string verseLine, ThemeList themeList)
        {
            return Match(verseLine, themeList, null);
        }

        /// <summary>
        /// How mutch the verse line matches provided theme list
        /// </summary>
        /// <param name="verseLine">verse line</param>
        /// <param name="themeList">provided theme list</param>
        /// <param name="creationMemory">creation memory (can be null)</param>
        /// <returns>how mutch the verse line matches provided theme list</returns>
        private static int Match(string verseLine, ThemeList themeList, CreationMemory creationMemory)
        {
            int match = 0;
            int themeAddedValue;
            string[] words = verseLine.Split(' ');
            HashSet<string> wordIgnoreList = new HashSet<string>();

            foreach (Theme currentTheme in themeList)
            {
                foreach (string currentWord in words)
                {
                    string word = currentWord.Trim();
                    if (currentTheme.Contains(word) && word.Length > 0 && !wordIgnoreList.Contains(word))
                    {
                        if (creationMemory == null || !creationMemory.ContainsWord(word))
                        {
                            if (creationMemory == null)
                            {
                                themeAddedValue = 10;
                            }
                            else
                            {
                                themeAddedValue = creationMemory.GetThemeAddedValue(currentTheme.Name);
                            }

                            match += themeAddedValue;
                            wordIgnoreList.Add(word);
                            break;
                        }
                    }
                }
            }

            return match;
        }

        /// <summary>
        /// From verses, pick the one with length closest to desired length
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>from verses, pick the one with length closest to desired length</returns>
        public static Verse PickBestLength(IList<Verse> verseList, int desiredLength)
        {
            Verse bestVerse = null;
            int bestDifference = -1;
            int currentDifference = -1;
            string verseLine;

            foreach (Verse currentVerse in verseList)
            {
                verseLine = currentVerse.ToString().HardTrim();
                currentDifference = Math.Abs(verseLine.Length - desiredLength);

                if (currentDifference < bestDifference || bestVerse == null)
                {
                    bestVerse = currentVerse;
                    bestDifference = currentDifference;
                }
            }
            return bestVerse;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Theme loader
        /// </summary>
        public static ThemeLoader ThemeLoader
        {
            get { return themeLoader; }
            set { themeLoader = value; }
        }
        #endregion
    }
}

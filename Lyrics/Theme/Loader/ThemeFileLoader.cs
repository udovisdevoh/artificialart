using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// This class is used to read theme lists from file
    /// </summary>
    internal class ThemeFileLoader
    {
        #region Public Methods
        /// <summary>
        /// Read theme list from file
        /// </summary>
        /// <param name="themeFileName">theme file name</param>
        /// <returns>theme list</returns>
        public Dictionary<string, Theme> LoadThemeListFromFile(string themeFileName)
        {
            Dictionary<string, Theme> themeList = new Dictionary<string, Theme>();

            String currentThemeName = null;
            StreamReader streamReader = new StreamReader(themeFileName);

            string line = null;
            while (true)
            {
                line = streamReader.ReadLine();
                if (line == null)
                    break;

                currentThemeName = TrySwitchTheme(line, currentThemeName);

                if (IsWordList(line) && currentThemeName != null)
                    AddWordListToTheme(ExtractWordList(line), GetOrCreateTheme(currentThemeName, themeList));
            }

            return themeList;
        }

        /// <summary>
        /// Read theme list from string
        /// </summary>
        /// <param name="themeResource">theme resource as string</param>
        /// <returns>theme list</returns>
        public Dictionary<string, Theme> LoadThemeListFromString(string themeResource)
        {
            Dictionary<string, Theme> themeList = new Dictionary<string, Theme>();

            String currentThemeName = null;

            string[] lineList = themeResource.Split('\n');

            foreach (string lineFromList in lineList)
            {
                string line = lineFromList;
                line = line.Trim();

                if (line.Length == 0)
                    continue;

                currentThemeName = TrySwitchTheme(line, currentThemeName);

                if (IsWordList(line) && currentThemeName != null)
                    AddWordListToTheme(ExtractWordList(line), GetOrCreateTheme(currentThemeName, themeList));
            }

            return themeList;
        }

        /// <summary>
        /// Add word list to theme
        /// </summary>
        /// <param name="wordList">word list</param>
        /// <param name="theme">theme</param>
        private void AddWordListToTheme(IEnumerable<string> wordList, Theme theme)
        {
            foreach (String word in wordList)
                theme.Add(word);
        }

        /// <summary>
        /// Returns theme from name
        /// </summary>
        /// <param name="themeName">theme's name</param>
        /// <param name="themeList">list to look into</param>
        /// <returns>found theme or new theme</returns>
        private Theme GetOrCreateTheme(String themeName, Dictionary<string, Theme> themeList)
        {
            Theme theme;
            if (!themeList.TryGetValue(themeName, out theme))
            {
                theme = new Theme(themeName);
                themeList.Add(themeName, theme);
            }
            return theme;
        }

        /// <summary>
        /// Whether the line is a list of words for a theme
        /// </summary>
        /// <param name="line">text line</param>
        /// <returns>whether the line is a list of words for a theme</returns>
        private bool IsWordList(string line)
        {
            return !line.Contains('<');
        }

        /// <summary>
        /// Try to switch to another theme from line
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="currentThemeName">current theme</param>
        /// <returns>old theme or new theme</returns>
        private string TrySwitchTheme(string line, string currentThemeName)
        {
            line = line.Trim();
            line = line.Replace(" ", "");
            if (!line.StartsWith("<") || line.StartsWith("</"))
                return currentThemeName;
            else
            {
                line = line.Substring(line.IndexOf("\"") + 1);
                line = line.Substring(0, line.IndexOf("\""));
                return line;
            }
        }

        /// <summary>
        /// Extract word list from line
        /// </summary>
        /// <param name="line">line</param>
        /// <returns>word list from line</returns>
        private IEnumerable<string> ExtractWordList(string line)
        {
            string[] wordArray = line.Split(',');
            List<string> wordList = new List<string>();

            foreach (string currentWord in wordArray)
            {
                string word = currentWord;
                word = word.Trim().ToLower();
                if (word.Length > 0)
                    wordList.Add(word);
            }

            return wordList;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// This class is used to load themes from text files
    /// </summary>
    internal class ThemeLoader
    {
        #region Fields
        /// <summary>
        /// Theme cache
        /// </summary>
        private Dictionary<string, Theme> themeCache;

        /// <summary>
        /// List of themes
        /// </summary>
        private ThemeList themeList;

        /// <summary>
        /// List of theme name ordered by name
        /// </summary>
        private List<string> sortedThemeNameList = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Create theme loader
        /// </summary>
        public ThemeLoader() : this("en")
        {
        }

        /// <summary>
        /// Constructor, we load the themes from file
        /// <param name="languageCode">language code</param>
        /// </summary>
        public ThemeLoader(string languageCode)
        {
            ThemeFileLoader themeFileLoader = new ThemeFileLoader();
            if (languageCode == "en")
                themeCache = themeFileLoader.LoadThemeListFromString(ArtificialArt.Properties.Resources.themeFile_themes_en);
            else if (languageCode == "fr")
                themeCache = themeFileLoader.LoadThemeListFromString(ArtificialArt.Properties.Resources.themeFile_themes_fr);
            else
                throw new ThemeException("Unrecoginzed language code: " + languageCode);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load theme
        /// </summary>
        /// <param name="themeName">theme's name</param>
        /// <returns>loaded theme</returns>
        public Theme Load(string themeName)
        {
            Theme theme;
            if (!themeCache.TryGetValue(themeName, out theme))
                throw new ThemeException("Theme not found");
            return theme;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Theme name list
        /// </summary>
        public IEnumerable<string> ThemeNameList
        {
            get
            {
                if (sortedThemeNameList == null)
                    sortedThemeNameList = new List<string>(from word in themeCache.Keys orderby word select word);
                return sortedThemeNameList;
            }
        }

        /// <summary>
        /// Theme list
        /// </summary>
        public ThemeList ThemeList
        {
            get
            {
                if (themeList == null)
                    themeList = new ThemeList(themeCache.Values);

                return themeList;
            }
        }
        #endregion
    }
}

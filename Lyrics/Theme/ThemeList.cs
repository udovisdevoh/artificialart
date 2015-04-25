using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Represents a set of themes
    /// </summary>
    internal class ThemeList : ICollection<Theme>
    {
        #region Fields
        /// <summary>
        /// Internal references to themes
        /// </summary>
        private HashSet<Theme> themeList;

        /// <summary>
        /// List of theme names
        /// </summary>
        private List<string> themeNameList = new List<string>();
        #endregion

        #region Constructors
        public ThemeList()
        {
            themeList = new HashSet<Theme>();
        }

        public ThemeList(IEnumerable<Theme> themeList)
        {
            this.themeList = new HashSet<Theme>(themeList);
        }
        #endregion

        #region Public Methods
        public bool Contains(string word)
        {
            foreach (Theme theme in themeList)
                if (theme.Contains(word))
                    return true;
            return false;
        }

        public Theme GetRandomTheme(Random random)
        {
            int index = random.Next(0, themeList.Count);

            int count = 0;
            foreach (Theme theme in themeList)
            {
                if (count == index)
                {
                    return theme;
                }
                count++;
            }

            throw new ThemeException("Theme list is empty, cannot pick a random theme");
        }

        /// <summary>
        /// Set language code
        /// </summary>
        /// <param name="languageCode">language code</param>
        /// <param name="themeLoader">theme loader</param>
        public void SetLanguageCode(string languageCode, ThemeLoader themeLoader)
        {
            themeList.Clear();
            foreach (string themeName in themeNameList)
                themeList.Add(themeLoader.Load(themeName));
        }
        #endregion

        #region ICollection<Theme> Members
        /// <summary>
        /// Add a theme
        /// </summary>
        /// <param name="theme">theme to add</param>
        public void Add(Theme theme)
        {
            themeList.Add(theme);
            themeNameList.Add(theme.Name);
        }

        /// <summary>
        /// Clear theme list
        /// </summary>
        public void Clear()
        {
            themeList.Clear();
            themeNameList.Clear();
        }

        /// <summary>
        /// Whether the theme manager contains theme
        /// </summary>
        /// <param name="theme">theme</param>
        /// <returns>whether the theme manager contains theme</returns>
        public bool Contains(Theme theme)
        {
            return themeList.Contains(theme);
        }

        /// <summary>
        /// Copy theme list to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(Theme[] array, int arrayIndex)
        {
            themeList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Count how many themes in theme manager
        /// </summary>
        public int Count
        {
            get { return themeList.Count; }
        }

        /// <summary>
        /// Whether theme manager is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove theme
        /// </summary>
        /// <param name="theme">theme to remove</param>
        /// <returns>if removal succeeded</returns>
        public bool Remove(Theme theme)
        {
            return themeList.Remove(theme) && themeNameList.Remove(theme.Name);
        }
        #endregion

        #region IEnumerable<Theme> Members
        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Theme> GetEnumerator()
        {
            return themeList.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return themeList.GetEnumerator();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Theme name list
        /// </summary>
        public List<string> Keys
        {
            get { return themeNameList; }
        }
        #endregion
    }
}

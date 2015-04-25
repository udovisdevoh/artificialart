using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Represents a song's verse
    /// </summary>
    internal class Verse : IEquatable<Verse>
    {
        #region Fields
        /// <summary>
        /// Text value
        /// </summary>
        private string textValue;

        /// <summary>
        /// Score for each theme (don't use directly: lazy initialization)
        /// </summary>
        private Dictionary<string, float> _themeScore;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a verse
        /// </summary>
        /// <param name="textValue">text value</param>
        public Verse(string textValue)
        {
            this.textValue = textValue;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get verse's text value
        /// </summary>
        /// <returns>verse's string value</returns>
        public override string ToString()
        {
            return textValue;
        }

        /// <summary>
        /// Verse's length
        /// </summary>
        public int Length
        {
            get
            {
                return textValue.Length;
            }
        }

        /// <summary>
        /// List of word in verse
        /// </summary>
        public List<string> WordList
        {
            get
            {
                string line = ToString();
                line = line.PunctuationToSpace().HardTrim();

                List<string> wordList = new List<string>();
                string[] words = ToString().Split(' ');

                foreach (string word in words)
                {
                    wordList.Add(word.Trim());
                }

                return wordList;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Score for teach theme
        /// </summary>
        public Dictionary<string, float> ThemeScore
        {
            get
            {
                if (_themeScore == null)
                    _themeScore = new Dictionary<string, float>();
                return _themeScore;
            }
        }
        #endregion

        #region IEquatable<Verse> Members
        /// <summary>
        /// Whether verses are identical
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Verse other)
        {
            return this.ToString().Equals(other.ToString());
        }
        #endregion
    }
}

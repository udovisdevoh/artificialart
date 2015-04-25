using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Abstract verse factory
    /// </summary>
    internal abstract class AbstractVerseFactory
    {
        #region Public Methods
        /// <summary>
        /// Build a verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>verse</returns>
        public abstract Verse Build(Verse previousVerse);

        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public Verse Build()
        {
            return Build(null);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Language code
        /// </summary>
        public abstract string LanguageCode
        {
            get;
            set;
        }
        #endregion
    }
}

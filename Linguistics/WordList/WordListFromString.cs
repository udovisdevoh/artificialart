using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Word list created from string
    /// </summary>
    internal class WordListFromString : WordList
    {
        #region Constructor
        /// <summary>
        /// Create word list from string
        /// </summary>
        /// <param name="resourceValue">resource value</param>
        public WordListFromString(string resourceValue)
        {
            internalHash = BuildInternalHash(resourceValue);
        }
        #endregion
    }
}

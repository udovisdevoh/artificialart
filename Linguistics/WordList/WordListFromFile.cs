using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Word list from file
    /// </summary>
    internal class WordListFromFile : WordList
    {
        #region Constructor
        /// <summary>
        /// Create word list from text file
        /// </summary>
        /// <param name="fileName">file name</param>
        public WordListFromFile(string fileName)
        {
            string content = File.ReadAllText(fileName);
            internalHash = BuildInternalHash(content);
        }
        #endregion
    }
}
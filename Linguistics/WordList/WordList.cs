using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    internal abstract class WordList
    {
        #region Parts
        /// <summary>
        /// Will contain the words
        /// </summary>
        protected HashSet<string> internalHash;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Whether word is in word list as negative or positive form
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is in word list as negative or positive form</returns>
        internal bool ContainsAsNegativeOrPositiveForm(string word)
        {
            word = word.ToLowerInvariant();
            if (word.EndsWith("n't"))
            {
                word = word.Substring(0, word.Length - 3);
                return ContainsExact(word) || ContainsExact(word + "n");
            }
            else
            {
                return ContainsExact(word);
            }
        }

        /// <summary>
        /// Whether word is in word list as exact form
        /// </summary>
        /// <param name="word">word</param>
        /// <returns>Whether word is in word list as exact form</returns>
        internal bool ContainsExact(string word)
        {
            word = word.ToLowerInvariant();
            return internalHash.Contains(word);
        }
        #endregion

        #region Protected Methods
        protected HashSet<string> BuildInternalHash(string content)
        {
            HashSet<string> internalHash = new HashSet<string>();
            content = content.Replace('\n', ' ');
            content = content.Replace('\r', ' ');
            content = content.Replace('\t', ' ');
            content = content.Replace(',', ' ');

            string[] chunkList = content.Split(' ');


            foreach (string word in chunkList)
            {
                string trimmedWord = word.Trim().ToLowerInvariant();
                if (trimmedWord.Length > 0)
                {
                    internalHash.Add(word.ToLowerInvariant());
                }
            }

            return internalHash;
        }
        #endregion
    }
}

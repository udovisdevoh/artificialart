using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages operations on the word "ain't"
    /// </summary>
    internal class AintManager
    {
        #region Internal Methods
        /// <summary>
        /// Remove word "aint" from text
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Text with word "aint" removed once</returns>
        internal string RemoveAintOnce(string text)
        {
            WordStream wordStream = new WordStream(text);
            bool isDone = false;
            Word word;
            while (wordStream.TryGetNextWord(out word))
            {
                if (!isDone && word.ToString().ToLowerInvariant() == "ain't")
                {
                    if (word.PreviousWord == null)
                    {
                        word.StringValue = "are";
                    }
                    else
                    {
                        if (word.PreviousWord.ToString().ToLowerInvariant() == "i")
                        {
                            word.StringValue = "am";
                        }
                        else if (word.PreviousWord.ToString().ToLowerInvariant() == "you")
                        {
                            word.StringValue = "are";
                        }
                        else if (word.PreviousWord.ToString().ToLowerInvariant() == "they")
                        {
                            word.StringValue = "are";
                        }
                        else if (word.PreviousWord.ToString().ToLowerInvariant() == "we")
                        {
                            word.StringValue = "are";
                        }
                        else
                        {
                            word.StringValue = "is";
                        }
                    }

                    if (word.NextWord != null && word.NextWord.ToString().ToLowerInvariant() == "no")
                    {
                        word.NextWord.StringValue = "a";
                    }

                    isDone = true;
                }
            }

            return wordStream.ToString();
        }
        #endregion
    }
}

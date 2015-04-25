using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.WebServices
{
    /// <summary>
    /// Represents a remote translation bot
    /// </summary>
    public abstract class AbstractTranslationBot
    {
        /// <summary>
        /// Translate a string
        /// May throw TranslationException
        /// </summary>
        /// <param name="textSource">text to translate</param>
        /// <param name="fromLanguageCode">from language code</param>
        /// <param name="toLanguageCode">to language code</param>
        /// <returns>translated text</returns>
        public abstract string Translate(string textSource, string fromLanguageCode, string toLanguageCode);

        /// <summary>
        /// Translate a chunk of strings
        /// May throw TranslationException
        /// </summary>
        /// <param name="textChunkSource">text to translate</param>
        /// <param name="fromLanguageCode">from language code</param>
        /// <param name="toLanguageCode">to language code</param>
        /// <param name="ignoreChunkSizeMissmatch">ignore chunk size miss match, default: false</param>
        /// <returns>translated string chunk</returns>
        public abstract IList<string> Translate(IEnumerable<string> textChunkSource, string fromLanguageCode, string toLanguageCode, bool ignoreChunkSizeMissmatch);

        /// <summary>
        /// Translate a chunk of strings
        /// May throw TranslationException
        /// </summary>
        /// <param name="textChunkSource">text to translate</param>
        /// <param name="fromLanguageCode">from language code</param>
        /// <param name="toLanguageCode">to language code</param>
        /// <returns>translated string chunk</returns>
        public IList<string> Translate(IEnumerable<string> textChunkSource, string fromLanguageCode, string toLanguageCode)
        {
            return Translate(textChunkSource, fromLanguageCode, toLanguageCode, false);
        }
    }
}

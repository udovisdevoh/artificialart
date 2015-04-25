using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.WebServices
{
    /// <summary>
    /// Represents BabelFish as a remote translation bot
    /// </summary>
    public class BabelFishTranslationBot : AbstractTranslationBot
    {
        #region Fields and Parts
        /// <summary>
        /// Primitive web bot
        /// </summary>
        private WebBot webBot = new WebBot();

        /// <summary>
        /// Post data
        /// </summary>
        private Dictionary<string, string> postData;
        #endregion

        #region Constructor
        /// <summary>
        /// Build BabelFish translation bot
        /// </summary>
        public BabelFishTranslationBot()
        {
            postData = new Dictionary<string, string>();
            postData["ei"] = "UTF-8";
            postData["doit"] = "done";
            postData["fr"] = "bf-res";
            postData["intl"] = "1";
            postData["tt"] = "urltext";

            postData["btnTrTxt"] = "Translate";
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Translate a string
        /// May throw TranslationException
        /// </summary>
        /// <param name="textSource">text to translate</param>
        /// <param name="fromLanguageCode">from language</param>
        /// <param name="toLanguageCode">to language</param>
        /// <returns>translated text</returns>
        public override string Translate(string textSource, string fromLanguageCode, string toLanguageCode)
        {
            fromLanguageCode = fromLanguageCode.ToLowerInvariant();
            toLanguageCode = toLanguageCode.ToLowerInvariant();
            textSource = textSource.Replace('\'', '’');
            textSource = textSource.Replace('-', ' ');
            textSource = textSource.Replace('-', ' ');
            textSource = textSource.Replace("&", "and");

            postData["lp"] = fromLanguageCode + '_' + toLanguageCode;
            postData["trtext"] = textSource;

            string pageContent = webBot.GetPageContent("http://babelfish.yahoo.com/translate_txt",postData);

            pageContent = pageContent.Replace('\n', ' ');
            pageContent = pageContent.Replace('\r', ' ');
            pageContent = pageContent.Replace('\t', ' ');

            while (pageContent.Contains("  "))
                pageContent = pageContent.Replace("  ", " ");

            pageContent = pageContent.Trim();
            pageContent = pageContent.Substring(pageContent.IndexOf("<div id=\"result\">") + 17);
            pageContent = pageContent.Substring(pageContent.IndexOf('>') + 1);
            pageContent = pageContent.Substring(0,pageContent.IndexOf("</div"));

            if (pageContent.Contains('<') && pageContent.Contains('>'))
                throw new TranslationException("Translation failed");

            return pageContent;
        }

        /// <summary>
        /// Translate a chunk of strings
        /// May throw TranslationException
        /// </summary>
        /// <param name="textChunkSource">text to translate</param>
        /// <param name="fromLanguageCode">from language code</param>
        /// <param name="toLanguageCode">to language code</param>
        /// <param name="ignoreChunkSizeMissmatch">ignore chunk size miss match, default: false</param>
        /// <returns>translated string chunk</returns>
        public override IList<string> Translate(IEnumerable<string> textChunkSource, string fromLanguageCode, string toLanguageCode, bool ignoreChunkSizeMissmatch)
        {
            string text = string.Empty;
            foreach (string line in textChunkSource)
                text += line.Replace(".","") + ". ";
            string translatedText = Translate(text, fromLanguageCode, toLanguageCode);

            translatedText = translatedText.Replace('\n', ' ');
            translatedText = translatedText.Replace('\r', ' ');
            translatedText = translatedText.Replace('\t', ' ');
            while (translatedText.Contains("  "))
                translatedText = translatedText.Replace("  ", " ");

            translatedText = translatedText.Replace('.', '\n');

            translatedText = translatedText.Replace("\n ", "\n");
            translatedText = translatedText.Replace(" \n", "\n");

            translatedText = translatedText.Trim();

            List<string> translatedChunk = new List<string>(translatedText.Split('\n'));

            if (!ignoreChunkSizeMissmatch && translatedChunk.Count != textChunkSource.Count())
                throw new TranslationException("Invalid line count in translated chunk");

            return translatedChunk;
        }
        #endregion
    }
}

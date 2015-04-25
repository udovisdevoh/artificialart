using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ArtificialArt.WebServices
{
    /// <summary>
    /// Google bot
    /// </summary>
    public class GoogleChatBot : AbstractSearchChatBot
    {
        #region Parts
        /// <summary>
        /// Web bot used to get page contents
        /// </summary>
        private WebBot webBot = new WebBot();

        /// <summary>
        /// Html tag regex
        /// </summary>
        private Regex htmlTag = new Regex(@"<(.|\n)*?>");

        /// <summary>
        /// Result list
        /// </summary>
        private List<string> result = new List<string>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Start a new search
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        public override void Search(string searchCriteria)
        {
            string searchUrl = "http://www.google.ca/search?num=100&hl=en&q=%22" + searchCriteria + "%22";

            string content = webBot.GetPageContent(searchUrl);

            content = HttpUtility.HtmlDecode(content);

            content = htmlTag.Replace(content, string.Empty);

            /*content = content.Replace("<em>", " ");
            content = content.Replace("</em>", " ");

            content = content.Replace("<b>", " ");
            content = content.Replace("</b>", " ");

            content = content.Replace("<br>", " ");
            content = content.Replace("</br>", " ");*/

            content = content.Replace("\n", " ");
            content = content.Replace("\r", " ");
            content = content.Replace("\t", " ");
            content = content.Replace("\"", " ");

            while (content.Contains("  "))
                content = content.Replace("  ", " ");

            result = BuildResult(searchCriteria, content);
        }

        /// <summary>
        /// Search results
        /// </summary>
        /// <returns>Search results</returns>
        public override IEnumerator<string> GetEnumerator()
        {
            return result.GetEnumerator();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Count search results
        /// </summary>
        public override int Count
        {
            get { return result.Count; }
        }
        #endregion

        #region Private Methods
        private List<string> BuildResult(string searchCriteria, string content)
        {
            Regex regex;
            MatchCollection matches;
            result.Clear();
            
            regex = new Regex(@searchCriteria + @"[,.!? ][a-zéè, ]*[.!?]", RegexOptions.IgnoreCase);
            matches = regex.Matches(content);

            if (matches.Count < 0)
            {
                regex = new Regex(@"[a-zéè, ]*[.!?]" + @searchCriteria, RegexOptions.IgnoreCase);
                matches = regex.Matches(content);
            }

            foreach (Match match in matches)
                result.Add(match.ToString());

            result = new List<string>(from currentResult in result orderby currentResult.Length descending select currentResult);

            return result;
        }
        #endregion
    }
}

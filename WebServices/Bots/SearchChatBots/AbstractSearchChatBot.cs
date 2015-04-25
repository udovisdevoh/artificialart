using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArtificialArt.WebServices
{
    /// <summary>
    /// Google, bing, yahoo search bots
    /// </summary>
    public abstract class AbstractSearchChatBot : IEnumerable<string>
    {
        #region Fields and Parts
        private Regex camelCaseArtefact = new Regex(@"[a-z][A-Z]");

        private Regex doubleUpperCase = new Regex(@"[A-Z]{2}");

        private Regex doubleLowerCase = new Regex(@"[A-Z]{2}");

        private Regex corporateTitle = new Regex(@"[A-Z][a-z]* [A-Z][a-z]* [A-Z][a-z]*");

        private Regex websiteLink = new Regex(@"[,. ](Pictures|Photos|Movies|Forum|Lyrics|Reviews|Critics|Forums|Blog|Blogs)[,. ]");

        private Regex websiteLinkCaseInsensitive = new Regex(@"[,. ](Pictures|Photos|Movies|Forum|Lyrics|Reviews|Critics|Forums|Blog|Blogs)[,. ]", RegexOptions.IgnoreCase);
        #endregion

        #region Public Concrete Methods
        /// <summary>
        /// Try expand string using search bot (add prefex and suffix to search criteria)
        /// If fails, return original string
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        /// <param name="random">random number generator</param>
        /// <returns>Expanded string using search bot (add prefex and suffix to search criteria)</returns>
        public string TryExpandString(string searchCriteria, Random random)
        {
            Search(searchCriteria);

            if (Count < 1)
                return searchCriteria;

            string rememberCurrentResult = searchCriteria;
            foreach (string currentResult in this)
            {
                rememberCurrentResult = currentResult;

                if (currentResult.EndsWith(" ."))
                    continue;

                if (camelCaseArtefact.IsMatch(currentResult))
                    continue;

                if (camelCaseArtefact.IsMatch(currentResult))
                    continue;

                if (doubleUpperCase.IsMatch(currentResult) && doubleLowerCase.IsMatch(currentResult))
                    continue;

                if (corporateTitle.IsMatch(currentResult))
                    continue;

                if (websiteLink.IsMatch(currentResult))
                    continue;

                if (websiteLinkCaseInsensitive.Matches(currentResult).Count > 1)
                    continue;

                if (random.Next(3) == 0)
                    return currentResult;
            }
            return rememberCurrentResult;
        }
        #endregion

        #region Public Abstract Methods and Properties
        /// <summary>
        /// Start a new search
        /// </summary>
        /// <param name="searchCriteria">search criteria</param>
        public abstract void Search(string searchCriteria);

        /// <summary>
        /// Search results
        /// </summary>
        /// <returns>Search results</returns>
        public abstract IEnumerator<string> GetEnumerator();

        /// <summary>
        /// Count search results
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// Search results
        /// </summary>
        /// <returns>Search results</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Parsing
{
    /// <summary>
    /// Represents a bracket
    /// </summary>
    internal class BracketDefinition : IEquatable<BracketDefinition>
    {
        #region Fields
        /// <summary>
        /// Begining markup
        /// </summary>
        public char BeginMarkup;

        /// <summary>
        /// End markup
        /// </summary>
        public char EndMarkup;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a bracket definition
        /// </summary>
        /// <param name="beginMarkup">Begining markup</param>
        /// <param name="endMarkup">End markup</param>
        internal BracketDefinition(char beginMarkup, char endMarkup)
        {
            BeginMarkup = beginMarkup;
            EndMarkup = endMarkup;
        }
        #endregion

        #region IEquatable<BracketDefinition> Members
        /// <summary>
        /// Whether bracket definitions are equivalent
        /// </summary>
        /// <param name="other">other bracket definition</param>
        /// <returns>Whether bracket definitions are equivalent</returns>
        public bool Equals(BracketDefinition other)
        {
            if (other == null)
                return false;

            return this.BeginMarkup == other.BeginMarkup && this.EndMarkup == other.EndMarkup;
        }
        #endregion
    }
}
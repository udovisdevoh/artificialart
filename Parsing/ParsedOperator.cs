using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Parsing
{
    /// <summary>
    /// Parsed operator
    /// </summary>
    public class ParsedOperator
    {
        /// <summary>
        /// String value
        /// </summary>
        private string stringValue;

        /// <summary>
        /// Parsed operator
        /// </summary>
        /// <param name="stringValue">string value</param>
        public ParsedOperator(string stringValue)
        {
            this.stringValue = stringValue;
        }

        /// <summary>
        /// String value
        /// </summary>
        public string StringValue
        {
            get { return stringValue; }
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string value</returns>
        public override string ToString()
        {
            return stringValue;
        }
    }
}

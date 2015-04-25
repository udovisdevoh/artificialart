using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Logic operators
    /// </summary>
    static class LogicOperators
    {
        /// <summary>
        /// Or
        /// </summary>
        public static ParsedOperator _Or_ = new ParsedOperator("||");

        /// <summary>
        /// And
        /// </summary>
        public static ParsedOperator _And_ = new ParsedOperator("&&");

        /// <summary>
        /// ->
        /// </summary>
        public static ParsedOperator _Imply_ = new ParsedOperator("->");

        /// <summary>
        /// Not ->
        /// </summary>
        public static ParsedOperator _Not_Imply_ = new ParsedOperator("!>");

        /// <summary>
        /// Is equivalent
        /// </summary>
        public static ParsedOperator _Equals_ = new ParsedOperator("==");
    }
}

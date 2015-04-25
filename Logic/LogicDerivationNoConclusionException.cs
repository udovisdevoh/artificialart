using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Thrown when impossible to solve a logic problem
    /// </summary>
    public class LogicDerivationNoConclusionException : Exception
    {
        /// <summary>
        /// Thrown when impossible to solve a logic problem
        /// </summary>
        /// <param name="message">message</param>
        public LogicDerivationNoConclusionException(string message) : base(message) { }
    }
}

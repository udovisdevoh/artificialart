using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Method of doing a derivation
    /// </summary>
    public enum SolvingMethod
    {
        /// <summary>
        /// Straight derivation
        /// </summary>
        Straight,
        /// <summary>
        /// By prooving that the negation of proposition implies contradictions
        /// </summary>
        ReductioAdAbsurdum
    }
}

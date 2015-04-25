using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// To manage imperative propositions
    /// </summary>
    internal class ImperativeManager
    {
        /// <summary>
        /// Whether original proposition is imperative
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <returns>Whether original proposition is imperative</returns>
        internal bool IsImperative(string originalProposition)
        {
            if (originalProposition.IsQuestion())
                return false;

            WordStringStream wordStringStream = new WordStringStream(originalProposition);

            return wordStringStream.First().IsVerb();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics.English
{
    /// <summary>
    /// Manages questions that are questions because they begin with modal verbs
    /// </summary>
    internal class QuestionManagerByModalVerb
    {
        /// <summary>
        /// Whether question was detected becayse proposition begins with modal verb
        /// </summary>
        /// <param name="originalProposition">original proposition</param>
        /// <returns>Whether question was detected becayse proposition begins with modal verb</returns>
        internal bool IsQuestion(string originalProposition)
        {
            originalProposition = originalProposition.ToLowerInvariant();
            WordStringStream stream = new WordStringStream(originalProposition);
            return stream.First().IsModalVerb();
        }

        /// <summary>
        /// Remove question from proposition
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <returns>proposition with removed question</returns>
        internal string RemoveQuestion(string proposition)
        {
            WordStream wordStream = new WordStream(proposition);
            wordStream[0].StringValue = wordStream[0].StringValue.ToLowerInvariant();
            proposition = wordStream.ToString();

            if (proposition.CountWords() > 1)
                return proposition.InvertWordPosition(0, 1);
            else
                return proposition;
        }
    }
}

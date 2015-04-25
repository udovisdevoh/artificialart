using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.WebServices
{
    /// <summary>
    /// Exception thrown when translation fails
    /// </summary>
    public class TranslationException : Exception
    {
        /// <summary>
        /// Exception thrown when translation fails
        /// </summary>
        /// <param name="message">Message</param>
        public TranslationException(string message) : base(message)
        {
        }
    }
}

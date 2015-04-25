using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    /// <summary>
    /// Exception thrown when building riff fails
    /// </summary>
    class RiffBuilderException : Exception
    {
        /// <summary>
        /// Exception thrown when building riff fails
        /// </summary>
        /// <param name="message">exception message</param>
        public RiffBuilderException(string message) : base(message)
        {
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    /// <summary>
    /// Exception thrown when building metaSong fails
    /// </summary>
    class MetaSongException : Exception
    {
        /// <summary>
        /// Exception thrown when building metaSong fails
        /// </summary>
        /// <param name="message">exception's message</param>
        public MetaSongException(string message) : base(message) { }
    }
}

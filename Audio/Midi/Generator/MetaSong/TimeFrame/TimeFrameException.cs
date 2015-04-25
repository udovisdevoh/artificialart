using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class TimeFrameException : Exception
    {
        public TimeFrameException(string message) : base(message) { }
    }
}

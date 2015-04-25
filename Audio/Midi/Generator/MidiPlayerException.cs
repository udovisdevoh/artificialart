using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MidiPlayerException : Exception
    {
        public MidiPlayerException(string message) : base(message) { }
    }
}

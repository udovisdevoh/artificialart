using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    internal class VerseFactoryException : Exception
    {
        public VerseFactoryException(string message)
            : base(message)
        {
        }
    }
}

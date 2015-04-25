using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    internal class ThemeException : Exception
    {
        public ThemeException(string message)
            : base(message)
        {
        }
    }
}

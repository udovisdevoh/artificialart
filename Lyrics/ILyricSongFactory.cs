using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Lyric song factory
    /// </summary>
    public interface ILyricSongFactory
    {
        /// <summary>
        /// Build lyrics
        /// </summary>
        /// <returns>lyrics</returns>
        List<string> Build();

        /// <summary>
        /// Language code
        /// </summary>
        string LanguageCode
        {
            get;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics 
{
    /// <summary>
    /// Represents a collection of lyric song factories with common source file name
    /// </summary>
    public class LyricSongFactoryCollection : IEnumerable<LyricSongFactory>, ILyricSongFactory
    {
        #region Const
        /// <summary>
        /// Default language code
        /// </summary>
        private const string defaultLanguageCode = "en";
        #endregion

        #region Fields
        /// <summary>
        /// Lyric source file name
        /// </summary>
        private string lyricSourceFileName;

        /// <summary>
        /// Internal list of lyric song factories
        /// </summary>
        private List<LyricSongFactory> internalList = new List<LyricSongFactory>();

        /// <summary>
        /// Language code
        /// </summary>
        private string languageCode = defaultLanguageCode;

        /// <summary>
        /// Lyric source path
        /// </summary>
        private string lyricSourcePath;
        #endregion

        #region Constructor
        /// <summary>
        /// Create lyric song factory collection with one lyric song factory, use AddNew() to enlarge collection
        /// </summary>
        /// <param name="lyricSourceFileName">Lyric source file name</param>
        /// <param name="lyricSourcePath">lyrics source folder</param>
        /// <param name="languageCode">language code</param>
        public LyricSongFactoryCollection(string lyricSourceFileName, string lyricSourcePath, string languageCode)
        {
            this.languageCode = languageCode;
            this.lyricSourcePath = lyricSourcePath;
            this.lyricSourceFileName = lyricSourceFileName;
            this.AddNew();
        }

        /// <summary>
        /// Create lyric song factory collection from existing lyric song factory
        /// </summary>
        /// <param name="lyricSongFactory">lyric song factory</param>
        public LyricSongFactoryCollection(LyricSongFactory lyricSongFactory)
        {
            this.lyricSourceFileName = lyricSongFactory.LyricSourceFileName;
            this.lyricSourcePath = lyricSongFactory.LyricSourcePath;
            this.internalList.Add(lyricSongFactory);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add new lyric source file name
        /// <returns>the lyric song factory</returns>
        /// </summary>
        public LyricSongFactory AddNew()
        {
            LyricSongFactory lyricSongFactory = new LyricSongFactory(lyricSourcePath, languageCode);
            internalList.Add(lyricSongFactory);
            return lyricSongFactory;
        }

        /// <summary>
        /// Build song
        /// </summary>
        /// <returns>song</returns>
        public List<string> Build()
        {
            List<List<string>> parallelSongPatternList = new List<List<string>>();

            foreach (LyricSongFactory lyricSongFactory in this)
            {
                List<string> songFromLyricFactory = lyricSongFactory.Build();
                parallelSongPatternList.Add(songFromLyricFactory);
            }

            List<string> finalSong = new List<string>();

            int lineCounter = 0;
            foreach (string line in parallelSongPatternList.First())
            {
                if (line.Trim().Length > 0)
                {
                    finalSong.Add(line);
                }
                else
                {
                    bool couldFindOtherLine = false;
                    foreach (List<string> otherSongPattern in parallelSongPatternList)
                    {
                        if (otherSongPattern.Count > lineCounter && otherSongPattern[lineCounter].Trim().Length > 0)
                        {
                            finalSong.Add(otherSongPattern[lineCounter]);
                            couldFindOtherLine = true;
                            break;
                        }
                    }
                    if (!couldFindOtherLine)
                        finalSong.Add("****** ****** ****** ******");
                }
                lineCounter++;
            }

            return finalSong;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set list element at specified index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>lyric song factory at specified index</returns>
        public LyricSongFactory this[int index]
        {
            get { return internalList[index]; }
            set { internalList[index] = value; }
        }

        /// <summary>
        /// How many bars in each lyric song factory
        /// </summary>
        public int BarCount
        {
            set
            {
                foreach (LyricSongFactory lyricSongFactory in this)
                    lyricSongFactory.BarCount = value;
            }
        }

        /// <summary>
        /// How many lyric song factory
        /// </summary>
        public int Count
        {
            get { return internalList.Count; }
        }

        /// <summary>
        /// Line count per bar
        /// </summary>
        public int LineCountPerBar
        {
            set
            {
                foreach (LyricSongFactory lyricSongFactory in this)
                    lyricSongFactory.LineCountPerBar = value;
            }
        }

        /// <summary>
        /// Current language code
        /// </summary>
        public string LanguageCode
        {
            get
            {
                return languageCode;
            }

            set
            {
                languageCode = value;
                foreach (LyricSongFactory lyricSongFactory in this)
                    lyricSongFactory.LanguageCode = value;
            }
        }
        #endregion

        #region IEnumerable<LyricSongFactory> Members
        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<LyricSongFactory> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion
    }
}

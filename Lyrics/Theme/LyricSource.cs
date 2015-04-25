using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ArtificialArt.Linguistics;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Represents a lyric source
    /// </summary>
    internal class LyricSource
    {
        #region Fields
        /// <summary>
        /// File name
        /// </summary>
        private string fileName;

        /// <summary>
        /// Verse list cache
        /// </summary>
        private Dictionary<string, Verse> verseListCache;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a lyric source from file name
        /// </summary>
        /// <param name="fileName">file name</param>
        public LyricSource(string fileName)
        {
            this.fileName = fileName;
            verseListCache = new Dictionary<string, Verse>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Return random source line
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random source line</returns>
        public Verse GetRandomSourceLine(Random random)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            string line = string.Empty;
            long position = (long)(random.NextDouble() * fileStream.Length - 300);

            if (position < 0)
                position = 0;

            fileStream.Seek(position, 0);

            line = streamReader.ReadLine();
            line = streamReader.ReadLine();
            line = line.HardTrim();

            Verse verse;

            if (!verseListCache.TryGetValue(line, out verse))
            {
                verse = new Verse(line);
                verseListCache.Add(line, verse);
            }
            return verse;
        }

        /// <summary>
        /// Return random source lines
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="samplingSize">sampling size</param>
        /// <returns>random source lines</returns>
        public HashSet<Verse> GetRandomSourceLineList(Random random, int samplingSize)
        {
            int samplingSizeSqrt = (int)(Math.Sqrt(samplingSize));
            HashSet<Verse> verseList = new HashSet<Verse>();
            for (int i = 0; i < samplingSizeSqrt; i++)
                verseList.UnionWith(GetRandomContiguousSourceLineList(random, samplingSizeSqrt));
            return verseList;
        }

        /// <summary>
        /// Return random contiguous line list
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="samplingSize">how many ling you want</param>
        /// <returns>random contiguous line list</returns>
        public IEnumerable<Verse> GetRandomContiguousSourceLineList(Random random, int samplingSize)
        {
            return GetRandomContiguousSourceLineList(random, samplingSize, null, false);
        }

        /// <summary>
        /// Return random contiguous line list
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="samplingSize">how many ling you want</param>
        /// <param name="startLine">line to start search on</param>
        /// <param name="isStartPointPositive">whether the start point is considered from begining of line or end of line</param>
        /// <returns>random contiguous line list</returns>
        public IEnumerable<Verse> GetRandomContiguousSourceLineList(Random random, int samplingSize, string startLine, bool isStartPointPositive)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            string line = string.Empty;
            long position;

            if (startLine == null)
                position = (long)(random.NextDouble() * fileStream.Length - 300 * samplingSize);
            else
                position = GetStartLinePosition(fileStream, streamReader, fileStream.Length, startLine, isStartPointPositive);

            if (position < 0)
                position = 0;

            fileStream.Seek(position, 0);

            line = streamReader.ReadLine();

            HashSet<Verse> verseList = new HashSet<Verse>();
            for (int i = 0; i < samplingSize; i++)
            {
                line = streamReader.ReadLine();
                line = line.HardTrim();

                if (line.IsForeignLanguage())
                    continue;

                Verse verse;

                if (!verseListCache.TryGetValue(line, out verse))
                {
                    verse = new Verse(line);
                    verseListCache.Add(line, verse);
                }
                verseList.Add(verse);
            }
            return verseList;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get start line position from previous verse
        /// </summary>
        /// <param name="fileStream">file stream</param>
        /// <param name="streamReader">stream reader</param>
        /// <param name="fileSize">file size</param>
        /// <param name="startLine">previous verse line</param>
        /// <param name="isStartPointPositive">if true: we use begining of previous verse line, else: we use the end of it</param>
        /// <returns>start line position</returns>
        private long GetStartLinePosition(FileStream fileStream, StreamReader streamReader, long fileSize, string startLine, bool isStartPointPositive)
        {
            string currentLine;
            long start = 0;
            long end = fileSize;
            long pivot;
            int comparison;

            while (true)
            {
                streamReader = new StreamReader(fileStream);

                pivot = (start + end) / 2;
                fileStream.Seek(pivot, 0);
                currentLine = streamReader.ReadLine();
                currentLine = streamReader.ReadLine();

                if (isStartPointPositive)
                    comparison = currentLine.CompareTo(startLine);
                else
                    comparison = currentLine.ReverseString().CompareTo(startLine.ReverseString());

                if (end - start < 300)
                {
                    break;
                }
                else if (comparison < 0)
                {
                    start = pivot;
                }
                else
                {
                    end = pivot;
                }
            }
            return pivot;
        }
        #endregion
    }
}

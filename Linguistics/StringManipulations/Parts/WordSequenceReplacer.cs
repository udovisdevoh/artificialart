using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Manages replace of word sequences
    /// </summary>
    internal class WordSequenceReplacer
    {
        #region Fields and parts
        /// <summary>
        /// Jointed delimiter list
        /// </summary>
        private string joinedDelimiterList;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public WordSequenceReplacer()
        {
            joinedDelimiterList = "[ .,']*";
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Replace a word sequence in proposition
        /// </summary>
        /// <param name="originalProposition">original proposition (case insensitive)</param>
        /// <param name="fromSequence">from word sequence (case insensitive, delimiter insensitive)</param>
        /// <param name="toSequence">from word sequence (case insensitive, delimiter insensitive)</param>
        /// <returns>proposition with replaced word sequences</returns>
        internal string ReplaceWordSequence(string originalProposition, string fromSequence, string toSequence)
        {
            WordStream fromSequenceStream = new WordStream(fromSequence);
            Regex regex;

            string pattern = BuildWordSequencePattern(fromSequenceStream);

            if (toSequence.Length == 0)
                regex = new Regex(@pattern + ' ', RegexOptions.IgnoreCase);
            else
                regex = new Regex(@pattern, RegexOptions.IgnoreCase);
            

            string newString = regex.Replace(originalProposition, toSequence);
            
            return newString;
        }

        /// <summary>
        /// Replace word sequence
        /// </summary>
        /// <param name="original">original proposition</param>
        /// <param name="startWordSequence">begining of the word sequence</param>
        /// <param name="midWordCount">how many unknown words in middle of sequence to replace</param>
        /// <param name="endWordSequence">ending of word sequence</param>
        /// <param name="startReplace">begining of replaced word sequence</param>
        /// <param name="endReplace">ending of replaced word sequence</param>
        /// <returns>Text with replaced word sequences</returns>
        internal string ReplaceWordSequence(string original, string startWordSequence, int midWordCount, string endWordSequence, string startReplace, string endReplace)
        {
            WordStream fromStartSequenceStream = new WordStream(startWordSequence);
            WordStream endWordSequenceStream = new WordStream(endWordSequence);
            Regex regexStart, regexEnd;

            string patternStart = BuildWordSequencePattern(fromStartSequenceStream);
            string patternEnd = BuildWordSequencePattern(endWordSequenceStream);

            string middleUnknownWordsPattern = BuildUnknownWordPattern(midWordCount);

            regexStart = new Regex(@patternStart + @"(?=" + @joinedDelimiterList + @middleUnknownWordsPattern + @joinedDelimiterList + @patternEnd + @")", RegexOptions.IgnoreCase);
            regexEnd = new Regex(@"(?<=" + @startReplace + @joinedDelimiterList + @middleUnknownWordsPattern + @joinedDelimiterList + @")" + @patternEnd, RegexOptions.IgnoreCase);

            original = regexStart.Replace(original, startReplace);
            original = regexEnd.Replace(original, endReplace);

            return original;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Regex pattern matching word sequence with any kind of delimiter
        /// </summary>
        /// <param name="wordStream">wordStream</param>
        /// <returns>Regex pattern matching word sequence with any kind of delimiter</returns>
        private string BuildWordSequencePattern(WordStream wordStream)
        {
            int counter = 0;
            string pattern = string.Empty;
            foreach (Word word in wordStream)
            {
                pattern += word.ToString();

                if (counter < wordStream.CountWords() - 1)
                {
                    pattern += joinedDelimiterList;
                }

                counter++;
            }
            return pattern;
        }

        private string BuildUnknownWordPattern(int midWordCount)
        {
            string pattern = string.Empty;

            for (int i = 0; i < midWordCount;i++ )
            {
                pattern += "[a-z']*";

                if (i < midWordCount - 1)
                    pattern += joinedDelimiterList;
            }
            return pattern;
        }
        #endregion
    }
}

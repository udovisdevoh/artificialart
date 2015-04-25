using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Represents a stream of words that can be extracted and manipulated form a string
    /// </summary>
    public class WordStream : WordStringStream, IEnumerable<Word>
    {
        #region Fields and parts
        /// <summary>
        /// List of words
        /// </summary>
        private List<Word> wordList;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a word stream
        /// </summary>
        /// <param name="originalText">original text</param>
        public WordStream(string originalText)
            : base(originalText)
        {
            wordList = new List<Word>();

            WordStringStream wordStringStream = new WordStringStream(originalText);

            string nextWordAsString, nextDelimiterAsString;
            bool isSentenceBegin;
            Word previousWord = null;
            string previousDelimiter = wordStringStream.FirstDelimiter;

            while (wordStringStream.TryGetNextWord(out nextWordAsString, out nextDelimiterAsString, out isSentenceBegin))
            {
                Word currentWord = new Word(nextWordAsString);
                currentWord.LeftDelimiter = previousDelimiter;
                currentWord.RightDelimiter = nextDelimiterAsString;
                currentWord.IsSentenceBegin = isSentenceBegin;
                wordList.Add(currentWord);

                if (previousWord != null)
                {
                    previousWord.NextWord = currentWord;
                    currentWord.PreviousWord = previousWord;
                }

                previousWord = currentWord;
                previousDelimiter = nextDelimiterAsString;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Try to get next word and advance pointer
        /// </summary>
        /// <param name="word">next word (can be null if none available</param>
        /// <returns>Whether could get next word or not</returns>
        public bool TryGetNextWord(out Word word)
        {
            if (pointer >= wordList.Count)
            {
                word = null;
                return false;
            }

            word = wordList[pointer];

            pointer++;
            return true;
        }

        /// <summary>
        /// Convert word stream to string
        /// </summary>
        /// <returns>converted to string</returns>
        public override string ToString()
        {
            Reset();
            string textValue = FirstDelimiter;

            Word word;
            while (this.TryGetNextWord(out word))
            {
                textValue += word.ToString();

                if (word.RightDelimiter != null)
                    textValue += word.RightDelimiter;
            }

            return textValue;
        }
        #endregion

        #region IEnumerable<Word> Members
        /// <summary>
        /// To iterate in word stream, returns a word iterator
        /// </summary>
        /// <returns>To iterate in word stream, returns a word iterator</returns>
        public new IEnumerator<Word> GetEnumerator()
        {
            return wordList.GetEnumerator();
        }

        /// <summary>
        /// To iterate in word stream, returns a word iterator
        /// </summary>
        /// <returns>To iterate in word stream, returns a word iterator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wordList.GetEnumerator();
        }

        /// <summary>
        /// Word at specified index
        /// </summary>
        /// <param name="index">specified index</param>
        /// <returns>Word at specified index</returns>
        public new Word this[int index]
        {
            get
            {
                return wordList[index];
            }
            set
            {
                wordList[index] = value;
            }
        }

        /// <summary>
        /// Last word
        /// </summary>
        /// <returns>Last word</returns>
        public Word Last()
        {
            return wordList.Last();
        }
        #endregion
    }
}
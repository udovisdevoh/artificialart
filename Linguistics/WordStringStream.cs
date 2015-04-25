using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Represents a stream of words (as strings) that can be extracted form a string
    /// </summary>
    public class WordStringStream : IEnumerable<string>
    {
        #region Fields and parts
        /// <summary>
        /// Current pointer in word list
        /// </summary>
        protected int pointer = 0;

        /// <summary>
        /// List of words
        /// </summary>
        private List<string> wordListAsString;

        /// <summary>
        /// List of words in lowercase
        /// </summary>
        private List<string> wordListAsStringLowerCase;

        /// <summary>
        /// First delimiter before first word
        /// </summary>
        private string firstDelimiter;

        /// <summary>
        /// List of other delimiters
        /// </summary>
        private List<string> delimiterList;

        /// <summary>
        /// List of word indexes that correspound to begining of sentences
        /// </summary>
        private HashSet<int> listSentenceBegin;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a word stream from a source text
        /// </summary>
        /// <param name="sourceText">source text</param>
        public WordStringStream(string sourceText)
        {
            wordListAsString = new List<string>();
            wordListAsStringLowerCase = new List<string>();
            delimiterList = new List<string>();
            listSentenceBegin = new HashSet<int>();

            listSentenceBegin.Add(0);//We add the index of the first word as sentence begining

            string currentWord = string.Empty;
            string currentDelimiter = string.Empty;
            string previousDelimiter = null;
            firstDelimiter = string.Empty;
            foreach (char letter in sourceText)
            {
                if (StringManipulations.IsWordDelimiter(letter))
                {
                    currentDelimiter += letter;
                    if (wordListAsString.Count == 0 && currentWord == string.Empty)
                        firstDelimiter += letter;
                    if (currentWord != string.Empty)
                    {
                        wordListAsString.Add(currentWord);
                        wordListAsStringLowerCase.Add(currentWord.ToLowerInvariant());

                        if (previousDelimiter != null && (previousDelimiter.Contains('.') || previousDelimiter.Contains('!') || previousDelimiter.Contains('?')))
                            listSentenceBegin.Add(wordListAsString.Count - 1);

                        currentWord = string.Empty;
                    }
                }
                else
                {
                    if (currentDelimiter != string.Empty)
                    {
                        delimiterList.Add(currentDelimiter);
                        previousDelimiter = currentDelimiter;
                        currentDelimiter = string.Empty;
                    }
                    currentWord += letter;
                }
            }

            if (currentWord != string.Empty)
            {
                wordListAsString.Add(currentWord);
                currentWord = string.Empty;
            }

            if (currentDelimiter != string.Empty)
            {
                delimiterList.Add(currentDelimiter);
                currentDelimiter = string.Empty;
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Try get next word and delimiter from word stream
        /// </summary>
        /// <param name="nextWord">next word</param>
        /// <param name="nextDelimiter">next word delimiter (can be null if it's the last word)</param>
        /// <returns>whether there are still words to get</returns>
        public bool TryGetNextWord(out string nextWord, out string nextDelimiter)
        {
            bool isSentenceBegin = false;
            return TryGetNextWord(out nextWord, out nextDelimiter, out isSentenceBegin);
        }

        /// <summary>
        /// Try get next word and delimiter from word stream
        /// </summary>
        /// <param name="nextWord">next word</param>
        /// <param name="nextDelimiter">next word delimiter (can be null if it's the last word)</param>
        /// <param name="isSentenceBegin">whether current word is the begining of a sentence</param>
        /// <returns>whether there are still words to get</returns>
        public bool TryGetNextWord(out string nextWord, out string nextDelimiter, out bool isSentenceBegin)
        {
            isSentenceBegin = false;
            nextWord = null;
            nextDelimiter = null;
            if (pointer >= wordListAsString.Count)
                return false;

            nextWord = wordListAsString[pointer];

            if (pointer < delimiterList.Count)
                nextDelimiter = delimiterList[pointer];

            isSentenceBegin = listSentenceBegin.Contains(pointer);

            pointer++;
            return true;
        }

        /// <summary>
        /// Try to get next word without advancing pointer
        /// Null if no word left
        /// </summary>
        /// <returns>next word or null if none available</returns>
        public string PeekNextWord()
        {
            if (pointer >= wordListAsString.Count)
                return null;

            return wordListAsString[pointer];
        }

        /// <summary>
        /// Try to get next delimiter without advancing pointer
        /// Null if no delimiter left
        /// </summary>
        /// <returns>next delimiter or null if none available</returns>
        public string PeekNextDelimiter()
        {
            if (pointer >= delimiterList.Count)
                return null;

            return delimiterList[pointer];
        }

        /// <summary>
        /// Reset the word pointer
        /// </summary>
        public void Reset()
        {
            pointer = 0;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Whether word stream contains specified word
        /// </summary>
        /// <param name="word">specified word</param>
        /// <returns>Whether word stream contains specified word</returns>
        public bool ContainsWord(string word)
        {
            return wordListAsStringLowerCase.Contains(word);
        }

        /// <summary>
        /// Word count
        /// </summary>
        /// <returns>word count</returns>
        public int CountWords()
        {
            return wordListAsString.Count;
        }
        #endregion

        #region Properties
        /// <summary>
        /// First delimiter before first word
        /// </summary>
        public string FirstDelimiter
        {
            get { return firstDelimiter; }
        }

        /// <summary>
        /// Get word as string at specified index
        /// </summary>
        /// <param name="index">specified index</param>
        /// <returns>word as string at specified index</returns>
        public string this[int index]
        {
            get
            {
                if (index >= wordListAsString.Count || index < 0)
                    return null;

                return wordListAsString[index];
            }
        }
        #endregion

        #region IEnumerable<string> Members
        /// <summary>
        /// To iterate in word string stream
        /// </summary>
        /// <returns>word string stream iterator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return wordListAsString.GetEnumerator();
        }

        /// <summary>
        /// To iterate in word string stream
        /// </summary>
        /// <returns>word string stream iterator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wordListAsString.GetEnumerator();
        }
        #endregion
    }
}
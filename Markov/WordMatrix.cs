using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArtificialArt.Markov
{
    /// <summary>
    /// Word markov matrix (key: pair of 2 word, value: next word)
    /// </summary>
    public class WordMatrix : Matrix
    {
        #region Public Methods
        /// <summary>
        /// Learn from text line
        /// </summary>
        /// <param name="textLine">source text line</param>
        public void LearnFromLine(string textLine)
        {
            textLine = HardTrim(textLine);
            string[] wordList = textLine.Split(' ');

            string previousPair = string.Empty;
            string previousWord = string.Empty;
            string previousPreviousWord = string.Empty;

            foreach (string word in wordList)
            {
                AddStatistics(previousPair, word);

                previousPreviousWord = previousWord;
                previousWord = word;
                previousPair = previousPreviousWord + " " + previousWord;
            }
        }

        /// <summary>
        /// Learn from text file
        /// </summary>
        /// <param name="fileName">file's name</param>
        public void LearnFromFile(string fileName)
        {
            int counter = 0;
            string line;

            using (StreamReader file = new StreamReader(fileName))
            {
                while ((line = file.ReadLine()) != null)
                {
                    LearnFromLine(line);
                    counter++;
                }
                file.Close();
            }
        }

        /// <summary>
        /// Generate sentence from matrix
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>generated sentence</returns>
        public string GenerateSentence(Random random)
        {
            Dictionary<string, float> row;

            string previousPair = string.Empty;
            string word = string.Empty;
            string previousWord = string.Empty;
            string previousPreviousWord = string.Empty;

            string sentence = string.Empty;

            while (NormalData.TryGetValue(previousPair, out row))
            {
                word = row.GetPonderatedRandom(random);

                sentence += " " + word;

                previousPreviousWord = previousWord;
                previousWord = word;

                previousPair = previousPreviousWord + " " + previousWord;
            }

            sentence = sentence.Trim();

            return sentence;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Remove undesired characters from text
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>Clean text</returns>
        private string HardTrim(string text)
        {
            text = text.Replace('\n', ' ');
            text = text.Replace('\r', ' ');
            text = text.Replace('\t', ' ');
            text = text.Replace('0', ' ');
            text = text.Replace('1', ' ');
            text = text.Replace('2', ' ');
            text = text.Replace('3', ' ');
            text = text.Replace('4', ' ');
            text = text.Replace('5', ' ');
            text = text.Replace('6', ' ');
            text = text.Replace('7', ' ');
            text = text.Replace('8', ' ');
            text = text.Replace('9', ' ');

            while (text.Contains("  "))
                text = text.Replace("  ", " ");

            text = text.ToLowerInvariant();
            text = text.Trim();
            return text;
        }
        #endregion
    }
}

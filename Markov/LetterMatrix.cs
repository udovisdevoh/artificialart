using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Markov
{
    /// <summary>
    /// Letter matrix
    /// </summary>
    public class LetterMatrix
    {
        #region Fields
        /// <summary>
        /// Previous word
        /// </summary>
        private char previousChar;

        /// <summary>
        /// Previous previous word
        /// </summary>
        private char previousPreviousChar;

        /// <summary>
        /// Integer based letter matrix (key: starting pair of letter, value: (key: ending letter, value: count))
        /// </summary>
        private Dictionary<string, Dictionary<char, int>> absoluteMatrix;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lineList">Line list</param>
        public LetterMatrix(IEnumerable<string> lineList)
        {
            resetCursor();
            absoluteMatrix = new Dictionary<string, Dictionary<char, int>>();
            foreach (string line in lineList)
                Learn(absoluteMatrix, line);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a verse from letter markov matrix
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Verse</returns>
        public char GenerateNextChar(Random random)
        {
            string sentence = string.Empty;
            Dictionary<char, int> row;

            if (!absoluteMatrix.TryGetValue(previousPreviousChar + "" + previousChar, out row))
                return ' ';
            char selectedChar = row.GetPonderatedRandom(random);

            previousPreviousChar = previousChar;
            previousChar = selectedChar;

            return selectedChar;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Reset cursor to begining
        /// </summary>
        private void resetCursor()
        {
            previousChar = '*';
            previousPreviousChar = '*';
        }

        /// <summary>
        /// Learn from text line
        /// </summary>
        /// <param name="absoluteMatrix">absolute matrix</param>
        /// <param name="textLine">text line</param>
        private void Learn(Dictionary<string, Dictionary<char, int>> absoluteMatrix, string textLine)
        {
            char currentPreviousPreviousChar = '*';
            char currentPreviousChar = '*';
            int currentValue;
            foreach (char currentChar in textLine)
            {
                string sourcePair = currentPreviousPreviousChar + "" + currentPreviousChar;

                Dictionary<char, int> currentRow;
                if (!absoluteMatrix.TryGetValue(sourcePair, out currentRow))
                {
                    currentRow = new Dictionary<char, int>();
                    absoluteMatrix.Add(sourcePair, currentRow);
                }

                if (!currentRow.TryGetValue(currentChar, out currentValue))
                    currentRow.Add(currentChar, 0);

                currentRow[currentChar]++;

                currentPreviousPreviousChar = currentPreviousChar;
                currentPreviousChar = currentChar;
            }
        }
        #endregion
    }
}
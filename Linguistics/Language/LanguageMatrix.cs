using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Markov;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Markov matrix representing a language
    /// </summary>
    public class LanguageMatrix : Matrix
    {
        #region Constructor
        /// <summary>
        /// Create language matrix from text source
        /// </summary>
        /// <param name="textSource">text source</param>
        public LanguageMatrix(string textSource) : this()
        {
            Learn(textSource);
        }

        /// <summary>
        /// Create language matrix
        /// </summary>
        public LanguageMatrix()
        {
        }

        /// <summary>
        /// Create language matrix from matrix
        /// </summary>
        /// <param name="matrix">matrix</param>
        public LanguageMatrix(Matrix matrix)
        {
            this.NormalData = matrix.NormalData;
            this.ReversedData = matrix.ReversedData;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Learn from text source
        /// </summary>
        /// <param name="textSource"></param>
        public void Learn(string textSource)
        {
            textSource = textSource.ToUpperInvariant();

            textSource = textSource.Replace('\n',' ');
            textSource = textSource.Replace('\r', ' ');
            textSource = textSource.Replace('\t', ' ');
            textSource = textSource.Replace("*", "");
            textSource = textSource.Replace("[", "");
            textSource = textSource.Replace("]", "");
            textSource = textSource.Replace("°", "");
            textSource = textSource.Replace("/", "");
            textSource = textSource.Replace("\\", "");
            textSource = textSource.Replace("+", "");
            textSource = textSource.Replace("(", "");
            textSource = textSource.Replace("-", "");
            textSource = textSource.Replace("%", "");
            textSource = textSource.Replace(":", "");
            textSource = textSource.Replace("#", "");
            textSource = textSource.Replace(";", "");
            textSource = textSource.Replace(";", "");
            textSource = textSource.Replace("…", "");
            textSource = textSource.Replace("’", "'");
            textSource = textSource.Replace("^", "");
            textSource = textSource.Replace(")", "");
            textSource = textSource.Replace("|", "");
            textSource = textSource.Replace("=", "");
            textSource = textSource.Replace("~", "");
            textSource = textSource.Replace("<", "");
            textSource = textSource.Replace(">", "");
            textSource = textSource.Replace("\"", "");
            textSource = textSource.Replace("▲", "");
            textSource = textSource.Replace("▼", "");
            textSource = textSource.Replace("$", "");
            textSource = textSource.Replace("ː", "");
            textSource = textSource.Replace("ˈ", "");
            textSource = textSource.Replace("Ɛ", "");
            textSource = textSource.Replace("Ə", "");
            textSource = textSource.Replace("Ʊ", "");
            textSource = textSource.Replace(".SVG", "");
            textSource = textSource.Replace(".JPG", "");
            textSource = textSource.Replace(".JPEG", "");
            textSource = textSource.Replace(".GIF", "");

            textSource = textSource.Replace("撃", "");
            textSource = textSource.Replace("特", "");
            textSource = textSource.Replace("別", "");
            textSource = textSource.Replace("特", "");
            textSource = textSource.Replace("攻", "");
            textSource = textSource.Replace("隊", "");
            

            

            for (int i = 0; i <= 9; i++)
                textSource = textSource.Replace(i.ToString(), "");


            while (textSource.Contains("  "))
                textSource = textSource.Replace("  ", " ");

            textSource = textSource.Replace(" o ", "");
            textSource = textSource.Replace("█", "");

            textSource = textSource.Replace(" . ", " ");
            

            string previousPair = "  ";
            char previousChar = ' ';
            foreach (char currentChar in textSource)
            {
                if (previousPair != "  ")
                    AddStatistics(previousPair, currentChar.ToString());
                //AddStatistics(previousChar.ToString(), currentChar.ToString());
                previousChar = currentChar;
                previousPair = previousPair.Substring(1) + previousChar;
            }
        }

        /// <summary>
        /// Get random starting pair
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random starting pair</returns>
        public string GetRandomStartingPair(Random random)
        {
            List<string> availablePairList = new List<string>();

            string key;
            Dictionary<string, float> value;
            foreach (KeyValuePair<string, Dictionary<string, float>> keyValuePair in NormalData)
            {
                key = keyValuePair.Key;
                value = keyValuePair.Value;

                if (key.StartsWith(" ") && value.Count > 2)
                {
                    availablePairList.Add(key);
                }
            }

            if (availablePairList.Count > 0)
                return (string)((IEnumerable<object>)availablePairList.ToArray()).GetRandomValue(random);
            else
                return (string)((IEnumerable<object>)NormalData.Keys.ToArray()).GetRandomValue(random);
        }
        #endregion
    }
}

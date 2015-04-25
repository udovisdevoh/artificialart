using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArtificialArt.Lyrics
{
    internal static class StringManipulations
    {
        #region Parts
        /// <summary>
        /// Anything but a letter
        /// </summary>
        private static Regex notALetter = new Regex(@"[^a-zA-Z]");

        /// <summary>
        /// Anything but a letter or space
        /// </summary>
        private static Regex notALetterNorSpace = new Regex(@"[^a-zA-Z ]");
        #endregion

        #region Protected Methods
        internal static string PunctuationToSpace(this string text)
        {
            return notALetterNorSpace.Replace(text, " ");
        }

        internal static bool IsForeignLanguage(this string source)
        {
            foreach (char currentChar in source)
                if (currentChar > '\x00FF')
                    return true;

            return false;
        }

        /// <summary>
        /// Clean the string
        /// </summary>
        /// <param name="text">source string</param>
        /// <returns>cleaned string</returns>
        internal static string HardTrim(this string text)
        {
            if (text == "[stop]")
                return text;

            while (text.Contains("  "))
                text = text.Replace("  ", " ");
            if (text.EndsWith("[stop]"))
                text = text.Substring(0, text.Length - 6);
            text = text.Trim();
            return text;
        }

        /// <summary>
        /// Clean the verse
        /// </summary>
        /// <param name="verse">source verse</param>
        /// <returns>cleaned verse</returns>
        internal static Verse HardTrim(this Verse verse)
        {
            return new Verse(verse.ToString().HardTrim());
        }
        #endregion
    }
}

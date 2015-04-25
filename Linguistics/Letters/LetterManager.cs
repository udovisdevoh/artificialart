using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Manages analysis on letters
    /// </summary>
    public static class LetterManager
    {
        #region Fields and parts
        /// <summary>
        /// List of vowels
        /// </summary>
        private static HashSet<char> vowelList;

        /// <summary>
        /// List of consonants
        /// </summary>
        private static HashSet<char> consonantList;

        /// <summary>
        /// List of letter groups
        /// </summary>
        private static List<HashSet<char>> letterGroupList;

        /// <summary>
        /// Letter phonetic manager
        /// </summary>
        private static LetterPhoneticDistanceManager letterPhoneticDistanceManager;

        /// <summary>
        /// Letter group for each letter
        /// </summary>
        private static Dictionary<char, HashSet<char>> letterGroupCache;
        #endregion

        #region Constructors
        /// <summary>
        /// Build letter manager
        /// </summary>
        static LetterManager()
        {
            letterPhoneticDistanceManager = new LetterPhoneticDistanceManager();
            vowelList = BuildVowelList();
            consonantList = BuildConsonantList();
            letterGroupList = BuildLetterGroupList();
            letterGroupCache = new Dictionary<char, HashSet<char>>();
        }

        /// <summary>
        /// Build letter group list
        /// </summary>
        /// <returns>letter group list</returns>
        private static List<HashSet<char>> BuildLetterGroupList()
        {
            List<HashSet<char>> letterGroupList = new List<HashSet<char>>();

            HashSet<char> letterGroup;

            letterGroup = new HashSet<char>(new char[]{'b', 'p'});
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'c', 'g', 'k' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'c', 'ç', 'ć' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'd', 't' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'f', 'v' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'l', 'ł' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'n', 'ñ', 'ń' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'j', 'y' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 's', 'z', 'ś', 'ß', 'ź', 'ż' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'v', 'w'});
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'v', 'u' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'a', 'á', 'à', 'â', 'ä', 'ã', 'ą' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'e', 'é', 'è', 'ê', 'ë', 'ę' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'i', 'í', 'î', 'ï' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'o', 'ó', 'ô', 'ö', 'õ' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'u', 'ú', 'ù', 'û', 'ü' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'y', 'ÿ' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'p', 'f' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'j', 'h' });
            letterGroupList.Add(letterGroup);

            letterGroup = new HashSet<char>(new char[] { 'i', 'y' });
            letterGroupList.Add(letterGroup);

            return letterGroupList;
        }

        /// <summary>
        /// Build consonant list
        /// </summary>
        /// <returns>consonant list</returns>
        private static HashSet<char> BuildConsonantList()
        {
            HashSet<char> consonantList = new HashSet<char>();
            consonantList.Add('b');
            consonantList.Add('c');
            consonantList.Add('ç');
            consonantList.Add('ć');
            consonantList.Add('d');
            consonantList.Add('f');
            consonantList.Add('g');
            consonantList.Add('h');
            consonantList.Add('j');
            consonantList.Add('k');
            consonantList.Add('l');
            consonantList.Add('ł');
            consonantList.Add('m');
            consonantList.Add('n');
            consonantList.Add('ñ');
            consonantList.Add('ń');
            consonantList.Add('p');
            consonantList.Add('q');
            consonantList.Add('r');
            consonantList.Add('s');
            consonantList.Add('ś');
            consonantList.Add('ß');
            consonantList.Add('t');
            consonantList.Add('v');
            consonantList.Add('w');
            consonantList.Add('x');
            consonantList.Add('z');
            consonantList.Add('ź');
            consonantList.Add('ż');
            return consonantList;
        }

        /// <summary>
        /// Build vowel list
        /// </summary>
        /// <returns>vowel list</returns>
        private static HashSet<char> BuildVowelList()
        {
            HashSet<char> vowelList = new HashSet<char>();
            vowelList.Add('a');
            vowelList.Add('á');
            vowelList.Add('à');
            vowelList.Add('â');
            vowelList.Add('ä');
            vowelList.Add('ã');
            vowelList.Add('ą');
            vowelList.Add('e');
            vowelList.Add('é');
            vowelList.Add('è');
            vowelList.Add('ê');
            vowelList.Add('ë');
            vowelList.Add('ę');
            vowelList.Add('i');
            vowelList.Add('í');
            vowelList.Add('î');
            vowelList.Add('ï');
            vowelList.Add('o');
            vowelList.Add('ó');
            vowelList.Add('ô');
            vowelList.Add('ö');
            vowelList.Add('õ');
            vowelList.Add('u');
            vowelList.Add('ú');
            vowelList.Add('ù');
            vowelList.Add('û');
            vowelList.Add('ü');
            vowelList.Add('y');
            vowelList.Add('ÿ');
            return vowelList;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Whether letter is consonant
        /// </summary>
        /// <param name="letter">letter</param>
        /// <returns>Whether letter is consonant</returns>
        public static bool IsConsonant(this char letter)
        {
            letter = letter.ToLower();
            return consonantList.Contains(letter);
        }

        /// <summary>
        /// Whether character is letter
        /// </summary>
        /// <param name="character">character</param>
        /// <returns>Whether character is letter</returns>
        public static bool IsLetter(this char character)
        {
            character = character.ToLower();
            return vowelList.Contains(character) || consonantList.Contains(character);
        }

        /// <summary>
        /// Whether letter is vowel
        /// </summary>
        /// <param name="letter">letter</param>
        /// <returns>Whether letter is vowel</returns>
        public static bool IsVowel(this char letter)
        {
            letter = letter.ToLower();
            return vowelList.Contains(letter);
        }

        /// <summary>
        /// Whether both letters are of the same letter group (f and v, i and ì etc...)
        /// </summary>
        /// <param name="letter1">letter 1</param>
        /// <param name="letter2">letter 2</param>
        /// <returns>Whether both letters are of the same letter group</returns>
        public static bool IsSameLetterGroup(this char letter1, char letter2)
        {
            letter1 = letter1.ToLower();
            letter2 = letter2.ToLower();
            return letter1.GetLetterGroup().Contains(letter2);
        }

        /// <summary>
        /// Get phonetic distance between two letters
        /// </summary>
        /// <param name="letter1">letter 1</param>
        /// <param name="letter2">letter 2</param>
        /// <returns>phonetic distance</returns>
        public static float GetLetterPhoneticDistance(this char letter1, char letter2)
        {
            return letterPhoneticDistanceManager.GetPhoneticDistance(letter1, letter2);
        }

        /// <summary>
        /// Get letter group for letter
        /// </summary>
        /// <param name="letter">letter</param>
        /// <returns>letter group for letter</returns>
        public static HashSet<char> GetLetterGroup(this char letter)
        {
            letter = letter.ToLower();
            HashSet<char> letterGroup;
            if (!letterGroupCache.TryGetValue(letter, out letterGroup))
            {
                letterGroup = new HashSet<char>();

                foreach (HashSet<char> currentLetterGroup in letterGroupList)
                    if (currentLetterGroup.Contains(letter))
                        letterGroup.UnionWith(currentLetterGroup);

                letterGroupCache.Add(letter, letterGroup);
            }
            return letterGroup;
        }
        #endregion
    }
}

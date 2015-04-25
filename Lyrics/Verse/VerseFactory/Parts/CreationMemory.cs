using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// To remember previous verse theme words not to repeat them too often
    /// </summary>
    internal class CreationMemory
    {
        #region Fields
        private Dictionary<string, int> occurencePerTheme = new Dictionary<string, int>();

        private HashSet<string> themeWordList = new HashSet<string>();

        private HashSet<Verse> straightSourceSampleVerseList;

        private int rhymeCounter;

        private int rhymeSpan;

        private List<Verse> verseToAddRhyme = new List<Verse>();

        private ICollection<Verse> splicedVerseList = new List<Verse>();

        private Queue<Verse> verseListToRhymeWith = new Queue<Verse>();
        #endregion

        #region Constructor
        public CreationMemory()
        {
            Clear();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Clear creation memory
        /// </summary>
        public void Clear()
        {
            occurencePerTheme.Clear();
            themeWordList.Clear();
            straightSourceSampleVerseList = null;
            rhymeCounter = 0;
            verseToAddRhyme.Clear();
            rhymeSpan = -1;
            splicedVerseList.Clear();
        }

        /// <summary>
        /// Remember information from generated verse to avoid repitition
        /// </summary>
        /// <param name="verse">generated verse</param>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public void Remember(Verse verse, VerseConstructionSettings verseConstructionSettings)
        {
            themeWordList.UnionWith(Evaluator.GetThemeWords(verse, verseConstructionSettings.ThemeList));
            AddOccurenceCountPerTheme(Evaluator.CountOccurencePerTheme(verse, verseConstructionSettings.ThemeList));
            verseListToRhymeWith.Enqueue(verse);
        }

        public void RememberWord(string word)
        {
            themeWordList.Add(word);
        }

        public bool ContainsWord(string word)
        {
            return themeWordList.Contains(word);
        }

        public int GetThemeAddedValue(string themeName)
        {
            int value;
            if (!occurencePerTheme.TryGetValue(themeName, out value))
                value = 0;

            value = value - GetMinimumlyGeneratedVerseWordCount();

            value = 10 - value;

            if (value < 0)
                value = 0;

            return value;
        }

        public Verse GetVerseToAddRhyme(int differenceFromLatest)
        {
            int index = verseToAddRhyme.Count + differenceFromLatest;
            if (index < 0 || index >= verseToAddRhyme.Count)
                return null;

            return verseToAddRhyme[index];
        }

        public void AddVerseToAddRhyme(Verse verse)
        {
            verseToAddRhyme.Add(verse);
        }
        #endregion

        #region Private Methods
        private void AddOccurenceCountPerTheme(Dictionary<string, int> localOccurencePerTheme)
        {
            foreach (KeyValuePair<string, int> themeAndCount in localOccurencePerTheme)
            {
                if (!occurencePerTheme.ContainsKey(themeAndCount.Key))
                    occurencePerTheme.Add(themeAndCount.Key, 0);
                occurencePerTheme[themeAndCount.Key] += themeAndCount.Value;
            }
        }

        private int GetMinimumlyGeneratedVerseWordCount()
        {
            int count = -1;

            foreach (int currentCount in occurencePerTheme.Values)
                if (currentCount < count || count == -1)
                    count = currentCount;

            return count;
        }
        #endregion

        #region Properties
        public HashSet<Verse> StraightSourceSampleVerseList
        {
            get { return straightSourceSampleVerseList; }
            set { straightSourceSampleVerseList = value; }
        }

        public int RhymeCounter
        {
            get { return rhymeCounter; }
            set { rhymeCounter = value; }
        }

        public int RhymeSpan
        {
            get { return rhymeSpan; }
            set { rhymeSpan = value; }
        }

        public ICollection<Verse> SplicedVerseList
        {
            get { return splicedVerseList; }
            set { splicedVerseList = value; }
        }

        public Queue<Verse> VerseListToRhymeWith
        {
            get { return verseListToRhymeWith; }
            set { verseListToRhymeWith = value; }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Markov;

namespace ArtificialArt.Linguistics
{
    /// <summary>
    /// Collection of predefined language matrixes
    /// </summary>
    internal class LanguageMatrixCollection : IEnumerable<LanguageMatrix>
    {
        #region Fields
        /// <summary>
        /// List of languages
        /// </summary>
        private Dictionary<string, LanguageMatrix> languageList;

        /// <summary>
        /// Xml Matrix Saver Loader
        /// </summary>
        private XmlMatrixSaverLoader xmlMatrixSaverLoader;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize languages
        /// </summary>
        public LanguageMatrixCollection()
        {
            xmlMatrixSaverLoader = new XmlMatrixSaverLoader();
            languageList = new Dictionary<string, LanguageMatrix>();
            AddLanguage("english", ArtificialArt.Properties.Resources.english_language_matrix);
            AddLanguage("french", ArtificialArt.Properties.Resources.french_language_matrix);
            AddLanguage("spanish", ArtificialArt.Properties.Resources.spanish_language_matrix);
            AddLanguage("portuguese", ArtificialArt.Properties.Resources.portuguese_language_matrix);
            AddLanguage("italian", ArtificialArt.Properties.Resources.italian_language_matrix);
            AddLanguage("polish", ArtificialArt.Properties.Resources.polish_language_matrix);
            AddLanguage("dutch", ArtificialArt.Properties.Resources.dutch_language_matrix);
            AddLanguage("german", ArtificialArt.Properties.Resources.german_language_matrix);
            //AddLanguage("romanian", ArtificialArt.Properties.Resources.romanian_language_matrix);
            //AddLanguage("vietnamese", ArtificialArt.Properties.Resources.vietnamese_language_matrix);
            //AddLanguage("danish", ArtificialArt.Properties.Resources.danish_language_matrix);
            //AddLanguage("estonian", ArtificialArt.Properties.Resources.estonian_language_matrix);
            //AddLanguage("hungarian", ArtificialArt.Properties.Resources.hungarian_language_matrix);
            //AddLanguage("turkish", ArtificialArt.Properties.Resources.turkish_language_matrix);
            //AddLanguage("latvian", ArtificialArt.Properties.Resources.latvian_language_matrix);
            //AddLanguage("russian", ArtificialArt.Properties.Resources.russian_language_matrix);
            //AddLanguage("indonesian", ArtificialArt.Properties.Resources.indonesian_language_matrix);
            //AddLanguage("norse", ArtificialArt.Properties.Resources.norse_language_matrix);
            //AddLanguage("swedish", ArtificialArt.Properties.Resources.swedish_language_matrix);
            //AddLanguage("gaelic", ArtificialArt.Properties.Resources.gaelic_language_matrix);
            //AddLanguage("czech", ArtificialArt.Properties.Resources.czech_language_matrix);
            //AddLanguage("finnish", ArtificialArt.Properties.Resources.finnish_language_matrix);
            //AddLanguage("swaili", ArtificialArt.Properties.Resources.swaili_language_matrix);
            //AddLanguage("wolof", ArtificialArt.Properties.Resources.wolof_language_matrix);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Return language matrix from language name
        /// </summary>
        /// <param name="languageName">language's name</param>
        /// <returns>language matrix from language name</returns>
        public LanguageMatrix this[string languageName]
        {
            get
            {
                languageName = languageName.ToLowerInvariant();
                return languageList[languageName];
            }
            set{languageList[languageName] = value;}
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add language
        /// </summary>
        /// <param name="languageName">language name</param>
        /// <param name="resource">string resource xml file content</param>
        private void AddLanguage(string languageName, string resource)
        {
            LanguageMatrix matrix = new LanguageMatrix(xmlMatrixSaverLoader.LoadString(resource));
            languageList.Add(languageName, matrix);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Language name list
        /// </summary>
        public IEnumerable<string> Keys
        {
            get { return languageList.Keys; }
        }
        #endregion

        #region IEnumerable<LanguageMatrix> Members
        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<LanguageMatrix> GetEnumerator()
        {
            return languageList.Values.GetEnumerator();
        }

        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return languageList.Values.GetEnumerator();
        }
        #endregion
    }
}

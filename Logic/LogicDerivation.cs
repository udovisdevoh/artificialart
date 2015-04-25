using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Represents a logic derivation
    /// </summary>
    public class LogicDerivation : IEnumerable<TreeExpression>
    {
        #region Fields and Parts
        /// <summary>
        /// List of propositions
        /// </summary>
        private Dictionary<string, TreeExpression> propositionList;
        #endregion

        #region Constructor
        /// <summary>
        /// Build logic derivation
        /// </summary>
        public LogicDerivation()
        {
            propositionList = new Dictionary<string, TreeExpression>();
        }

        /// <summary>
        /// Build logic derivation from existing one
        /// </summary>
        /// <param name="toClone">to clone</param>
        public LogicDerivation(LogicDerivation toClone)
        {
            propositionList = new Dictionary<string, TreeExpression>();
            foreach (KeyValuePair<string, TreeExpression> keyValuePair in toClone.propositionList)
                propositionList.Add(keyValuePair.Key, keyValuePair.Value);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a proposition to the list
        /// </summary>
        /// <param name="treeExpression">tree expression</param>
        /// <returns>whether could add proposition</returns>
        public void Add(TreeExpression treeExpression)
        {
            treeExpression.Replace("!!", "");
            propositionList.Add(treeExpression.ToString(), treeExpression);
        }

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns>Get enumerator</returns>
        public IEnumerator<TreeExpression> GetEnumerator()
        {
            return propositionList.Values.GetEnumerator();
        }

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns>Get enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return propositionList.GetEnumerator();
        }

        /// <summary>
        /// Clear the list of propositions
        /// </summary>
        public void Clear()
        {
            propositionList.Clear();
        }

        /// <summary>
        /// How many propositions
        /// </summary>
        public int Count
        {
            get { return propositionList.Count; }
        }

        /// <summary>
        /// Whether logic derivation contains tree expression
        /// </summary>
        /// <param name="treeExpression">tree expression</param>
        /// <returns>Whether logic derivation contains tree expression</returns>
        public bool Contains(TreeExpression treeExpression)
        {
            return propositionList.ContainsValue(treeExpression);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get tree proposition from its string value
        /// </summary>
        /// <param name="stringValue">string value</param>
        /// <returns>tree proposition</returns>
        public TreeExpression this[string stringValue]
        {
            get { return propositionList[stringValue]; }
        }
        #endregion
    }
}

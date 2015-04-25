using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Manages hypothetical syllogism
    /// </summary>
    internal class HypotheticalSyllogismManager
    {
        #region Fields and parts
        /// <summary>
        /// To evaluate statements
        /// </summary>
        private Evaluator evaluator;
        #endregion

        #region Constructor
        /// <summary>
        /// Build hypothetical syllogism manager
        /// </summary>
        /// <param name="evaluator">evaluator</param>
        public HypotheticalSyllogismManager(Evaluator evaluator)
        {
            this.evaluator = evaluator;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Expand logic derivation by building hypothetical syllogisms
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <param name="logicDerivation"></param>
        /// <param name="listToIterate">list to iterate</param>
        /// <returns>Whether could expand logic derivation</returns>
        internal bool BuildHypotheticalSyllogism(TreeExpression proposition, LogicDerivation logicDerivation, IEnumerable<TreeExpression> listToIterate)
        {
            bool isAddNewImplication = false;
            TreeExpression newExpression;
            string leastOperator;
            if (proposition.MiddleOperator == "->" || proposition.MiddleOperator == "==")
            {
                foreach (TreeExpression otherExpression in listToIterate)
                {
                    if (otherExpression.MiddleOperator == "->" || otherExpression.MiddleOperator == "==")
                    {
                        if (proposition.RightChild.Equals(otherExpression.LeftChild))
                        {
                            if (proposition.MiddleOperator == "==" && otherExpression.MiddleOperator == "==")
                                leastOperator = "==";
                            else
                                leastOperator = "->";

                            newExpression = new TreeExpression(proposition.LeftChild, leastOperator, otherExpression.RightChild);
                            if (!logicDerivation.Contains(newExpression))
                            {
                                newExpression.ArgumentList.Add(proposition);
                                newExpression.ArgumentList.Add(otherExpression);
                                logicDerivation.Add(newExpression);
                                isAddNewImplication = true;
                            }
                        }
                    }
                }
            }
            return isAddNewImplication;
        }
        #endregion
    }
}

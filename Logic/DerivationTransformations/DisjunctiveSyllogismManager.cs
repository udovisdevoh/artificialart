using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Create new propositions by applying disjunctive syllogism manager
    /// </summary>
    internal class DisjunctiveSyllogismManager
    {
        #region Fields and parts
        /// <summary>
        /// Evaluator
        /// </summary>
        private Evaluator evaluator;

        /// <summary>
        /// To negate or denegate in a clean way
        /// </summary>
        private Negator negator;
        #endregion

        #region Constructor
        /// <summary>
        /// Disjunctive syllogism manager
        /// </summary>
        /// <param name="evaluator">evaluator</param>
        /// <param name="negator">negator</param>
        public DisjunctiveSyllogismManager(Evaluator evaluator, Negator negator)
        {
            this.evaluator = evaluator;
            this.negator = negator;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Create new propositions by applying disjunctive syllogism
        /// </summary>
        /// <param name="proposition">source proposition</param>
        /// <param name="logicDerivation">logic derivation to expand</param>
        /// <returns>whether could expand logic derivation</returns>
        internal bool BuildDisjunctiveSyllogism(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            TreeExpression disjunctiveProposition = null;
            if (proposition.MiddleOperator == "->")
            {
                disjunctiveProposition = new TreeExpression(negator.Negate(proposition.LeftChild), "||", proposition.RightChild);
            }
            else if (proposition.MiddleOperator == "||")
            {
                disjunctiveProposition = new TreeExpression(negator.Negate(proposition.LeftChild), "->", proposition.RightChild);   
            }

            if (disjunctiveProposition != null)
            {
                if (!logicDerivation.Contains(disjunctiveProposition))
                {
                    disjunctiveProposition.ArgumentList.Add(proposition);
                    logicDerivation.Add(disjunctiveProposition);
                    isAddNewImplication = true;
                }
            }

            return isAddNewImplication;
        }
        #endregion
    }
}

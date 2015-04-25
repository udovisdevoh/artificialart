using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// To validate and apply implication propositions
    /// (modus ponens)
    /// </summary>
    internal class ModusPonensManager
    {
        #region Fields and parts
        /// <summary>
        /// To evaluate statements
        /// </summary>
        private Evaluator evaluator;
        #endregion

        #region Constructor
        /// <summary>
        /// Build modus ponens manager
        /// </summary>
        /// <param name="evaluator">evaluator</param>
        public ModusPonensManager(Evaluator evaluator)
        {
            this.evaluator = evaluator;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Add implications to logic derivation
        /// (modus ponens)
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <param name="logicDerivation">logic derivation</param>
        /// <returns>whether could create new implications or not</returns>
        internal bool BuildModusPonens(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            if (proposition.MiddleOperator == "->" || proposition.MiddleOperator == "==")
            {
                if (evaluator.IsSatisfied(proposition.LeftChild, logicDerivation))
                {
                    if (logicDerivation.Contains(proposition.LeftChild))
                        proposition.LeftChild = logicDerivation[proposition.LeftChild.ToString()];

                    if (!logicDerivation.Contains(proposition.RightChild))
                    {
                        proposition.RightChild.ArgumentList.Add(proposition);
                        proposition.RightChild.ArgumentList.Add(proposition.LeftChild);
                        logicDerivation.Add(proposition.RightChild);
                        isAddNewImplication = true;
                    }
                }
            }
            return isAddNewImplication;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Manages stuff like P->!P, therefore !P, and !P->P, therefor P
    /// </summary>
    internal class TrueImplyFalseManager
    {
        #region Fields and parts
        /// <summary>
        /// To negate in a clean way
        /// </summary>
        private Negator negator;
        #endregion

        #region Constructor
        /// <summary>
        /// Manages stuff like P->!P, therefore !P, and !P->P, therefor P
        /// </summary>
        /// <param name="negator">To negate in a clean way</param>
        public TrueImplyFalseManager(Negator negator)
        {
            this.negator = negator;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Create new propositions from "True Imply False"
        /// </summary>
        /// <param name="proposition">proposition</param>
        /// <param name="logicDerivation">expanded logic derivation</param>
        /// <returns></returns>
        internal bool BuildTrueImplyFalse(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            if (proposition.MiddleOperator == "->")
            {
                if (proposition.LeftChild.Equals(negator.Negate(proposition.RightChild)))
                {
                    if (!logicDerivation.Contains(proposition.RightChild))
                    {
                        proposition.RightChild.ArgumentList.Add(proposition);
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

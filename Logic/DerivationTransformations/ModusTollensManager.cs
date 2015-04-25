using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// To create new propositions by applying modus tollens
    /// </summary>
    internal class ModusTollensManager
    {
        #region Fields and parts
        /// <summary>
        /// Evaluator
        /// </summary>
        private Evaluator evaluator;

        /// <summary>
        /// To negate propositions in a clean way
        /// </summary>
        private Negator negator;
        #endregion

        #region Constructor
        /// <summary>
        /// To create new propositions by applying modus tollens
        /// </summary>
        /// <param name="evaluator">evaluator</param>
        /// <param name="negator">to negate propositions in a clean way</param>
        public ModusTollensManager(Evaluator evaluator, Negator negator)
        {
            this.evaluator = evaluator;
            this.negator = negator;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Build propositions by applying modus tollens
        /// </summary>
        /// <param name="proposition">source proposition</param>
        /// <param name="logicDerivation">logic derivation</param>
        /// <returns>whether could create new propositions from modus tollens</returns>
        internal bool BuildModusTollens(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            if (proposition.MiddleOperator == "->" || proposition.MiddleOperator == "==")
            {
                TreeExpression modusTollensExpression = new TreeExpression(negator.Negate(proposition.RightChild), proposition.MiddleOperator, negator.Negate(proposition.LeftChild));
                if (modusTollensExpression != null && !logicDerivation.Contains(modusTollensExpression))
                {
                    modusTollensExpression.ArgumentList.Add(proposition);
                    logicDerivation.Add(modusTollensExpression);
                    isAddNewImplication = true;
                }
            }
            return isAddNewImplication;
        }
        #endregion
    }
}

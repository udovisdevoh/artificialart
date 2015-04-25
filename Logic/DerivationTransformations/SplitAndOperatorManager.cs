using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Creates new proposition by splitting exsting propositions for which the immediate operator is and
    /// </summary>
    internal class SplitAndOperatorManager
    {
        /// <summary>
        /// Creates new proposition by splitting exsting propositions for which the immediate operator is and
        /// </summary>
        /// <param name="proposition">source proposition</param>
        /// <param name="logicDerivation">logic derivation to expand</param>
        /// <returns>whether it could expand logic derivation</returns>
        internal bool BuildSplitAnd(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            if (proposition.MiddleOperator == "&&")
            {
                if (!logicDerivation.Contains(proposition.LeftChild))
                {
                    proposition.LeftChild.ArgumentList.Add(proposition);
                    logicDerivation.Add(proposition.LeftChild);
                    isAddNewImplication = true;
                }
                if (!logicDerivation.Contains(proposition.RightChild))
                {
                    proposition.RightChild.ArgumentList.Add(proposition);
                    logicDerivation.Add(proposition.RightChild);
                    isAddNewImplication = true;
                }
            }
            return isAddNewImplication;
        }
    }
}

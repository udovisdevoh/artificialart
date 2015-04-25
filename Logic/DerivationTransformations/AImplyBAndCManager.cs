using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Manages stuff like A->BandC, therefore A->B, A->C
    /// </summary>
    internal class AImplyBAndCManager
    {
        /// <summary>
        /// Builds stuff like A->BandC, therefore A->B, A->C
        /// </summary>
        /// <param name="proposition">proposition to evaluate</param>
        /// <param name="logicDerivation">logic derivation to expand</param>
        internal bool BuildAImplyBAndC(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            if (proposition.MiddleOperator == "->")
            {
                if (proposition.RightChild != null)
                {
                    if (proposition.RightChild.MiddleOperator == "&&")
                    {
                        TreeExpression firstRight = proposition.RightChild.LeftChild;
                        TreeExpression secondRight = proposition.RightChild.RightChild;

                        TreeExpression firstExpression = new TreeExpression(proposition.LeftChild, proposition.MiddleOperator, firstRight);
                        TreeExpression secondExpression = new TreeExpression(proposition.LeftChild, proposition.MiddleOperator, secondRight);

                        if (!logicDerivation.Contains(firstExpression))
                        {
                            firstExpression.ArgumentList.Add(proposition);
                            logicDerivation.Add(firstExpression);
                            isAddNewImplication = true;
                        }

                        if (!logicDerivation.Contains(secondExpression))
                        {
                            secondExpression.ArgumentList.Add(proposition);
                            logicDerivation.Add(secondExpression);
                            isAddNewImplication = true;
                        }
                    }
                }
            }
            return isAddNewImplication;
        }
    }
}

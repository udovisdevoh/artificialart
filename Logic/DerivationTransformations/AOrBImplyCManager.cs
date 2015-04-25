using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Manages stuff like A||B->C, therefore A->C, B->C
    /// </summary>
    internal class AOrBImplyCManager
    {
        /// <summary>
        /// Build stuff like A||B->C, therefore A->C, B->C
        /// </summary>
        /// <param name="proposition">proposition to derive from</param>
        /// <param name="logicDerivation">logic derivation to expand</param>
        internal bool BuildAOrBImplyC(TreeExpression proposition, LogicDerivation logicDerivation)
        {
            bool isAddNewImplication = false;
            if (proposition.MiddleOperator == "->")
            {
                if (proposition.LeftChild != null)
                {
                    if (proposition.LeftChild.MiddleOperator == "||")
                    {
                        TreeExpression firstLeft = proposition.LeftChild.LeftChild;
                        TreeExpression secondLeft = proposition.LeftChild.RightChild;

                        TreeExpression firstExpression = new TreeExpression(firstLeft, proposition.MiddleOperator, proposition.RightChild);
                        TreeExpression secondExpression = new TreeExpression(secondLeft, proposition.MiddleOperator, proposition.RightChild);

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

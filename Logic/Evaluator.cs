using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Evaluates whether proposition are true or false
    /// </summary>
    internal class Evaluator
    {
        #region Internal Method
        /// <summary>
        /// Whether composite tree expression is satisfied
        /// </summary>
        /// <param name="treeExpression">tree expression</param>
        /// <param name="logicDerivation">logic derivation</param>
        /// <returns>Whether composite expression is satisfied</returns>
        internal bool IsSatisfied(TreeExpression treeExpression, LogicDerivation logicDerivation)
        {
            IEnumerable<TreeExpression> proof;
            return IsSatisfied(treeExpression, logicDerivation, out proof);
        }

        /// <summary>
        /// Whether composite tree expression is satisfied
        /// </summary>
        /// <param name="treeExpression">tree expression</param>
        /// <param name="logicDerivation">logic derivation</param>
        /// <param name="proof">proof (facultative), can be null if no proof was found</param>
        /// <returns>Whether composite expression is satisfied</returns>
        internal bool IsSatisfied(TreeExpression treeExpression, LogicDerivation logicDerivation, out IEnumerable<TreeExpression> proof)
        {
            proof = null;

            if (treeExpression == null)
                return false;

            if (logicDerivation.Contains(treeExpression))
            {
                treeExpression = logicDerivation[treeExpression.ToString()];
                proof = treeExpression.ArgumentList;
                return true;
            }

            if (treeExpression.MiddleOperator == "||")
            {
                if (IsSatisfied(treeExpression.LeftChild, logicDerivation))
                {
                    if (treeExpression.ArgumentList.Count == 0)
                    {
                        treeExpression.ArgumentList.Add(treeExpression.LeftChild);
                    }
                    proof = treeExpression.ArgumentList;
                    logicDerivation.Add(treeExpression);
                    return true;
                }
                else if (IsSatisfied(treeExpression.RightChild, logicDerivation))
                {
                    if (treeExpression.ArgumentList.Count == 0)
                    {
                        treeExpression.ArgumentList.Add(treeExpression.RightChild);
                    }
                    proof = treeExpression.ArgumentList;
                    logicDerivation.Add(treeExpression);
                    return true;
                }
            }
            else if (treeExpression.MiddleOperator == "&&")
            {
                if (IsSatisfied(treeExpression.LeftChild, logicDerivation) && IsSatisfied(treeExpression.RightChild, logicDerivation))
                {
                    if (treeExpression.ArgumentList.Count == 0)
                    {
                        treeExpression.ArgumentList.Add(treeExpression.LeftChild);
                        treeExpression.ArgumentList.Add(treeExpression.RightChild);
                    }
                    proof = treeExpression.ArgumentList;
                    logicDerivation.Add(treeExpression);
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}

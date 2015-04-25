using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// To negate propositions in a clean way
    /// </summary>
    internal class Negator
    {
        #region Internal Methods
        /// <summary>
        /// Negate an expression in a clean way
        /// can return null if it's impossible to negate it
        /// </summary>
        /// <param name="treeExpression">tree expression to negate</param>
        /// <returns>new negated tree expression
        /// can return null if it's impossible to negate it</returns>
        internal TreeExpression Negate(TreeExpression treeExpression)
        {
            if (treeExpression.NegatedExpression == null)
            {
                TreeExpression negatedExpression = null;
                if (treeExpression.AtomicValue != null)
                {
                    negatedExpression = new TreeExpression(Negate(treeExpression.AtomicValue));
                }
                else if (treeExpression.MiddleOperator == "&&")
                {
                    negatedExpression = new TreeExpression(Negate(treeExpression.LeftChild), "||", Negate(treeExpression.RightChild));
                    if (negatedExpression.LeftChild == null || negatedExpression.RightChild == null)
                        return null;
                }
                else if (treeExpression.MiddleOperator == "||")
                {
                    negatedExpression = new TreeExpression(Negate(treeExpression.LeftChild), "&&", Negate(treeExpression.RightChild));
                    if (negatedExpression.LeftChild == null || negatedExpression.RightChild == null)
                        return null;
                }
                else if (treeExpression.MiddleOperator == "->")
                {
                    negatedExpression = new TreeExpression(treeExpression.LeftChild, "!>", treeExpression.RightChild);
                    if (negatedExpression.LeftChild == null || negatedExpression.RightChild == null)
                        return null;
                }
                else if (treeExpression.MiddleOperator == "!>")
                {
                    negatedExpression = new TreeExpression(treeExpression.LeftChild, "->", treeExpression.RightChild);
                    if (negatedExpression.LeftChild == null || negatedExpression.RightChild == null)
                        return null;
                }
                treeExpression.NegatedExpression = negatedExpression;
                treeExpression.NegatedExpression.NegatedExpression = treeExpression;
            }
            return treeExpression.NegatedExpression;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Negate or denegated atomic expression
        /// </summary>
        /// <param name="atomicExpression">atomic expression</param>
        /// <returns>negated (or denegated) atomic expression</returns>
        private string Negate(string atomicExpression)
        {
            atomicExpression = atomicExpression.Replace("(", "");
            atomicExpression = atomicExpression.Replace(")", "");
            atomicExpression = atomicExpression.Trim();
            atomicExpression = atomicExpression + " ";
            if (atomicExpression.StartsWith("!"))
                atomicExpression = atomicExpression.Substring(1);
            else
                atomicExpression = "!" + atomicExpression;

            atomicExpression = atomicExpression.Trim();
            return atomicExpression;
        }
        #endregion
    }
}

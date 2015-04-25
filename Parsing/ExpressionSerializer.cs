using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Parsing
{
    /// <summary>
    /// To convert tree expressions into strings
    /// </summary>
    internal static class ExpressionSerializer
    {
        /// <summary>
        /// Append flat string representationt to string builder
        /// </summary>
        /// <param name="stringBuilder">string builder</param>
        /// <param name="treeExpression">tree expression</param>
        internal static void AppendFlatStringRepresentation(StringBuilder stringBuilder, TreeExpression treeExpression)
        {
            if (treeExpression.AtomicValue != null)
            {
                stringBuilder.Append(treeExpression.AtomicValue);
            }
            else
            {
                stringBuilder.Append("(");

                if (treeExpression.LeftChild != null)
                    AppendFlatStringRepresentation(stringBuilder, treeExpression.LeftChild);

                if (treeExpression.MiddleOperator != null)
                {
                    stringBuilder.Append(treeExpression.MiddleOperator);
                }

                if (treeExpression.RightChild != null)
                    AppendFlatStringRepresentation(stringBuilder, treeExpression.RightChild);

                stringBuilder.Append(")");
            }
        }
    }
}

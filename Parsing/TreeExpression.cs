using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Parsing
{
    /// <summary>
    /// Result of parsing by universal parser
    /// </summary>
    public class TreeExpression
    {
        #region Parts
        /// <summary>
        /// Atomic value
        /// </summary>
        private string atomicValue;

        /// <summary>
        /// Left child
        /// </summary>
        private TreeExpression leftChild;

        /// <summary>
        /// Middle operator
        /// </summary>
        private string middleOperator;

        /// <summary>
        /// Right child
        /// </summary>
        private TreeExpression rightChild;

        /// <summary>
        /// String representation
        /// </summary>
        private string stringValue = null;

        /// <summary>
        /// Cached facultative negation of the expression
        /// (used for optimization)
        /// </summary>
        private TreeExpression negatedExpression = null;
        
        /// <summary>
        /// Lazy initialization, do not use this variable
        /// </summary>
        private HashSet<TreeExpression> _argumentList;
        #endregion

        #region Constructor
        /// <summary>
        /// Create tree expression
        /// </summary>
        /// <param name="atomicValue">atomic value</param>
        public TreeExpression(string atomicValue)
        {
            this.atomicValue = atomicValue;
            leftChild = null;
            middleOperator = null;
            rightChild = null;
        }

        /// <summary>
        /// Create tree expression
        /// </summary>
        /// <param name="leftChild">left child</param>
        /// <param name="middleOperator">middle operator</param>
        /// <param name="rightChild">right child</param>
        public TreeExpression(TreeExpression leftChild, string middleOperator, TreeExpression rightChild)
        {
            atomicValue = null;
            this.leftChild = leftChild;
            this.rightChild = rightChild;
            this.middleOperator = middleOperator;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Replace occurence of string to something else
        /// </summary>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        public void Replace(string from, string to)
        {
            if (atomicValue != null)
                atomicValue = atomicValue.Replace(from, to);
            if (middleOperator != null)
                middleOperator = middleOperator.Replace(from, to);
            if (stringValue != null)
                stringValue = stringValue.Replace(from, to);

            if (leftChild != null)
                leftChild.Replace(from, to);
            if (rightChild != null)
                rightChild.Replace(from, to);
        }

        /// <summary>
        /// String representation of the tree
        /// </summary>
        /// <returns>String representation of the tree</returns>
        public override string ToString()
        {
            if (stringValue == null)
                stringValue = ToString(false);
            return stringValue;
        }

        /// <summary>
        /// Indented string representation of the tree
        /// </summary>
        /// <param name="isIndented">whether the string is indented (default: false)</param>
        /// <returns>Indented string representation of the tree</returns>
        public string ToString(bool isIndented)
        {
            return ToString(0, isIndented);
        }

        /// <summary>
        /// Indented string representation of the tree
        /// </summary>
        /// <param name="isIndented">whether the string is indented (default: false)</param>
        /// <param name="indentationDepth">indentation depth (default: 0)</param>
        /// <returns>Indented string representation of the tree</returns>
        public string ToString(int indentationDepth, bool isIndented)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (isIndented)
            {
                for (int i = 0; i < indentationDepth; i++)
                    stringBuilder.Append("\t");

                if (middleOperator != null)
                {
                    stringBuilder.Append(middleOperator);
                }
                else if (atomicValue != null)
                {
                    stringBuilder.Append(atomicValue);
                }
                if (isIndented)
                    stringBuilder.Append("\r\n");

                if (leftChild != null)
                    stringBuilder.Append(leftChild.ToString(indentationDepth + 1, isIndented));
                if (rightChild != null)
                    stringBuilder.Append(rightChild.ToString(indentationDepth + 1, isIndented));
            }
            else
            {
                ExpressionSerializer.AppendFlatStringRepresentation(stringBuilder,this);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Whether the tree expressions are equal
        /// </summary>
        /// <param name="obj">other</param>
        /// <returns>Whether the tree expressions are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            TreeExpression other = (TreeExpression)obj;
            
            if (middleOperator != other.middleOperator)
                return false;

            if (atomicValue != other.atomicValue)
                return false;

            if (middleOperator == "&&" || middleOperator == "||" || middleOperator == "==")
            {
                if (leftChild.Equals(other.leftChild) && rightChild.Equals(other.rightChild))
                {
                }
                else if (leftChild.Equals(other.rightChild) && rightChild.Equals(other.leftChild))
                {
                }
                else
                {
                    return false;
                }
            }
            else if (middleOperator == "->" || middleOperator == "!>")
            {
                if (!leftChild.Equals(other.leftChild) || !rightChild.Equals(other.rightChild))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Atomic value
        /// </summary>
        public string AtomicValue
        {
            get { return atomicValue; }
            /*set { atomicValue = value; }*/
        }

        /// <summary>
        /// Left child
        /// </summary>
        public TreeExpression LeftChild
        {
            get { return leftChild; }
            set { leftChild = value; }
        }

        /// <summary>
        /// Right child
        /// </summary>
        public TreeExpression RightChild
        {
            get { return rightChild; }
            /*set { rightChild = value; }*/
        }

        /// <summary>
        /// Middle operator
        /// </summary>
        public string MiddleOperator
        {
            get { return middleOperator; }
            /*set { middleOperator = value; }*/
        }

        /// <summary>
        /// Cached facultative negation of the expression
        /// (used for optimization)
        /// </summary>
        public TreeExpression NegatedExpression
        {
            get { return negatedExpression; }
            set { negatedExpression = value; }
        }

        /// <summary>
        /// List of tree expression supporting this
        /// </summary>
        public HashSet<TreeExpression> ArgumentList
        {
            get
            {
                if (_argumentList == null)
                    _argumentList = new HashSet<TreeExpression>();
                return _argumentList;
            }
        }
        #endregion
    }
}
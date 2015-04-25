using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Parsing
{
    /// <summary>
    /// Converts string into tree like structures
    /// </summary>
    public class UniversalParser
    {
        #region Fields and Parts
        /// <summary>
        /// Operators and their priority (0 = first (low priority))
        /// </summary>
        private List<string> operatorPriorityList;

        /// <summary>
        /// Bracket definitions and their priority (0: outermost)
        /// </summary>
        private List<BracketDefinition> bracketPriorityList;

        /// <summary>
        /// Defaut bracket concatenation operator (default: *)
        /// </summary>
        private string defaultBracketConcatenationOperator;

        /// <summary>
        /// Validates bracket stack consistency
        /// </summary>
        private BracketStackConsistencyValidator bracketStackConsistencyValidator;
        #endregion

        #region Constructor
        /// <summary>
        /// Build universal parser
        /// </summary>
        public UniversalParser()
        {
            defaultBracketConcatenationOperator = "*";
            operatorPriorityList = new List<string>();
            bracketPriorityList = new List<BracketDefinition>();
            bracketStackConsistencyValidator = new BracketStackConsistencyValidator();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a list of operator to current priority level
        /// and increment current priority level
        /// </summary>
        /// <param name="operatorList">list of operator</param>
        public void AddOperatorPriority(string[] operatorList)
        {
            operatorPriorityList.AddRange(operatorList);
        }

        /// <summary>
        /// Add a single operator to current priority level
        /// New operators added will override this priority so add least prioritary first
        /// </summary>
        /// <param name="operatorToAdd">operator to add</param>
        public void AddOperatorPriority(string operatorToAdd)
        {
            operatorPriorityList.Add(operatorToAdd);
        }

        /// <summary>
        /// Add bracket by priority (outer brackets first)
        /// </summary>
        /// <param name="beginMarkup">begining markup</param>
        /// <param name="endMarkup">end markup</param>
        public void AddBracketPriority(char beginMarkup, char endMarkup)
        {
            bracketPriorityList.Add(new BracketDefinition(beginMarkup, endMarkup));
        }

        /// <summary>
        /// Parse flat expression and return it as a tree
        /// Can throw argument exception if brackets are not consistant in expression
        /// </summary>
        /// <param name="expression">flat expression to parse</param>
        /// <returns>tree expression</returns>
        public TreeExpression Parse(string expression)
        {
            if (!bracketStackConsistencyValidator.IsBracketConsistant(expression, bracketPriorityList))
                throw new ArgumentException("The brackets are not consistant in that expression");
            expression = expression.Trim();
            expression = AddDefaultBracketConcatenationOperator(expression);
            expression = RemoveBracketsFromBeginingAndIfConsistencyIsKept(expression);

            TreeExpression treeExpression = null;
            bool isFindOperator = false;
            foreach (string currentOperator in operatorPriorityList)
            {
                for (int position = 0; position < expression.Length; position++)
                {
                    if (position + currentOperator.Length <= expression.Length)
                    {
                        if (expression.Substring(position, currentOperator.Length) == currentOperator)
                        {
                            if (bracketStackConsistencyValidator.GetStackHeight(expression, position, bracketPriorityList) == 0)
                            {
                                string leftValue = expression.Substring(0, position);
                                string rightValue = expression.Substring(position + currentOperator.Length);
                                treeExpression = new TreeExpression(Parse(leftValue),currentOperator,Parse(rightValue));
                                isFindOperator = true;
                                break;
                            }
                        }
                    }
                }
                if (isFindOperator)
                    break;
            }

            if (!isFindOperator)
                treeExpression = new TreeExpression(expression);

            return treeExpression;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add default bracket concatenation operator when needed
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>repaired expression</returns>
        private string AddDefaultBracketConcatenationOperator(string expression)
        {
            foreach (BracketDefinition bracketDefinition in bracketPriorityList)
            {
                while (expression.Contains(bracketDefinition.EndMarkup + " "))
                    expression = expression.Replace(bracketDefinition.EndMarkup.ToString() + " ", bracketDefinition.EndMarkup.ToString());
                while (expression.Contains(" " + bracketDefinition.BeginMarkup))
                    expression = expression.Replace(" " + bracketDefinition.BeginMarkup.ToString(), bracketDefinition.BeginMarkup.ToString());
                while (expression.Contains(bracketDefinition.EndMarkup.ToString() + bracketDefinition.BeginMarkup.ToString()))
                    expression = expression.Replace(bracketDefinition.EndMarkup.ToString() + bracketDefinition.BeginMarkup.ToString(), bracketDefinition.EndMarkup.ToString() + defaultBracketConcatenationOperator + bracketDefinition.BeginMarkup.ToString());
            }
            return expression;
        }

        /// <summary>
        /// Remove brackets from begining and end if they are the only brackets
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>repaired expression</returns>
        private string RemoveBracketsFromBeginingAndIfConsistencyIsKept(string expression)
        {
            string oldExpression;
            do
            {
                oldExpression = expression;
                string newExpression = expression;
                foreach (BracketDefinition bracketDefinition in bracketPriorityList)
                {
                    if (expression[0] == bracketDefinition.BeginMarkup && expression[expression.Length - 1] == bracketDefinition.EndMarkup)
                    {
                        newExpression = newExpression.Substring(1, expression.Length - 2);
                        if (bracketStackConsistencyValidator.IsBracketConsistant(newExpression, bracketPriorityList))
                        {
                            expression = newExpression;
                        }
                    }
                }
            } while (oldExpression != expression);
            return expression;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Default bracket concatenation operator (default: *)
        /// May throw argument exception if value is null or empty
        /// </summary>
        public string DefaultBracketConcatenationOperator
        {
            get { return defaultBracketConcatenationOperator; }
            set
            {
                if (value == null || value.Length < 1)
                    throw new ArgumentException("Default bracket concatenation must not be null nor empty");
                defaultBracketConcatenationOperator = value;
            }
        }
        #endregion
    }
}
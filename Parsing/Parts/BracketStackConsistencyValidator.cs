using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Parsing
{
    /// <summary>
    /// Validates bracket stack consistency
    /// </summary>
    internal class BracketStackConsistencyValidator
    {
        #region Fields and Parts
        /// <summary>
        /// Temporary bracket definition stack
        /// </summary>
        private Stack<BracketDefinition> temporaryBracketDefinitionStack;
        #endregion

        #region Constructor
        /// <summary>
        /// Build bracket stack consistency validator
        /// </summary>
        public BracketStackConsistencyValidator()
        {
            temporaryBracketDefinitionStack = new Stack<BracketDefinition>();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Whether expression's bracket are consistant
        /// </summary>
        /// <param name="expression">expression</param>
        /// <param name="bracketDefinitionList">bracket definition list</param>
        /// <returns>Whether expression's bracket are consistant</returns>
        internal bool IsBracketConsistant(string expression, IEnumerable<BracketDefinition> bracketDefinitionList)
        {
            temporaryBracketDefinitionStack.Clear();
            foreach (char character in expression)
            {
                BracketDefinition currentBracketDefinition = GetBracketDefinition(character, bracketDefinitionList);
                if (currentBracketDefinition != null)
                {
                    if (currentBracketDefinition.BeginMarkup == character)
                    {
                        temporaryBracketDefinitionStack.Push(currentBracketDefinition);
                    }
                    else if (currentBracketDefinition.EndMarkup == character)
                    {
                        if (temporaryBracketDefinitionStack.Count < 1)
                            return false;

                        if (!temporaryBracketDefinitionStack.Pop().Equals(currentBracketDefinition))
                            return false;
                    }
                }
            }
            return temporaryBracketDefinitionStack.Count == 0;
        }

        /// <summary>
        /// Get height in stack for element in expression at position
        /// </summary>
        /// <param name="expression">expression</param>
        /// <param name="position">position</param>
        /// <param name="bracketDefinitionList">bracket definition list</param>
        /// <returns>height in stack for element in expression at position</returns>
        internal int GetStackHeight(string expression, int position, IEnumerable<BracketDefinition> bracketDefinitionList)
        {
            temporaryBracketDefinitionStack.Clear();
            int currentPosition = 0;
            int stackHeight = 0;
            foreach (char character in expression)
            {
                BracketDefinition currentBracketDefinition = GetBracketDefinition(character, bracketDefinitionList);
                if (currentBracketDefinition != null)
                {
                    if (currentBracketDefinition.BeginMarkup == character)
                    {
                        stackHeight++;
                    }
                    else if (currentBracketDefinition.EndMarkup == character)
                    {
                        stackHeight--;
                    }
                }

                if (currentPosition == position)
                    return stackHeight;
                currentPosition++;
            }
            return stackHeight;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get bracket definition associated to specified character or null if none found
        /// </summary>
        /// <param name="character">character</param>
        /// <param name="bracketDefinitionList">bracket definition list</param>
        /// <returns>bracket definition associated to specified character</returns>
        private BracketDefinition GetBracketDefinition(char character, IEnumerable<BracketDefinition> bracketDefinitionList)
        {
            foreach (BracketDefinition bracketDefinition in bracketDefinitionList)
                if (bracketDefinition.BeginMarkup == character || bracketDefinition.EndMarkup == character)
                    return bracketDefinition;
            return null;
        }
        #endregion
    }
}
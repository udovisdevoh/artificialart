using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace ArtificialArt.Logic
{
    /// <summary>
    /// Makes logic derivations
    /// </summary>
    public class Deriver
    {
        #region Fields and Parts
        /// <summary>
        /// Logic parser
        /// </summary>
        private UniversalParser parser;

        /// <summary>
        /// Temporary list of propositions
        /// </summary>
        private HashSet<TreeExpression> temporaryPropositionList;

        /// <summary>
        /// To validate and apply implication propositions
        /// </summary>
        private ModusPonensManager modusPonensManager;

        /// <summary>
        /// To create new propositions by applying modus tollens
        /// </summary>
        private ModusTollensManager modusTollensManager;

        /// <summary>
        /// Hypothetical syllogism manager
        /// </summary>
        private HypotheticalSyllogismManager hypotheticalSyllogismManager;

        /// <summary>
        /// Disjunctive syllogism manager
        /// </summary>
        private DisjunctiveSyllogismManager disjunctiveSyllogismManager;

        /// <summary>
        /// Creates new proposition by splitting exsting propositions for which the immediate operator is and
        /// </summary>
        private SplitAndOperatorManager splitAndOperatorManager;

        /// <summary>
        /// Manages stuff like P->!P, therefore !P, and !P->P, therefor P
        /// </summary>
        private TrueImplyFalseManager trueImplyFalseManager;

        /// <summary>
        /// Manages stuff like A->BandC, therefore A->B, A->C
        /// </summary>
        private AImplyBAndCManager aImplyBAndCManager;

        /// <summary>
        /// Manages stuff like A||B->C, therefore A->C, B->C
        /// </summary>
        private AOrBImplyCManager aOrBImplyCManager;

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
        /// Makes logic derivations
        /// </summary>
        public Deriver()
        {
            parser = new UniversalParser();
            parser.AddBracketPriority('(', ')');
            parser.AddOperatorPriority("==");
            parser.AddOperatorPriority("!>");
            parser.AddOperatorPriority("->");
            parser.AddOperatorPriority("||");
            parser.AddOperatorPriority("&&");

            evaluator = new Evaluator();
            negator = new Negator();
            temporaryPropositionList = new HashSet<TreeExpression>();

            //Transformations
            modusPonensManager = new ModusPonensManager(evaluator);
            modusTollensManager = new ModusTollensManager(evaluator, negator);
            hypotheticalSyllogismManager = new HypotheticalSyllogismManager(evaluator);
            disjunctiveSyllogismManager = new DisjunctiveSyllogismManager(evaluator, negator);
            splitAndOperatorManager = new SplitAndOperatorManager();
            trueImplyFalseManager = new TrueImplyFalseManager(negator);
            aImplyBAndCManager = new AImplyBAndCManager();
            aOrBImplyCManager = new AOrBImplyCManager();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create brand new propositions from original one
        /// </summary>
        /// <param name="expressionList">list of propositions</param>
        /// <returns>logic derivation</returns>
        public LogicDerivation Derive(IEnumerable<string> expressionList)
        {
            LogicDerivation logicDerivation = new LogicDerivation();
            foreach (string stringStatement in expressionList)
                logicDerivation.Add(parser.Parse(stringStatement));

            Derive(logicDerivation);

            return logicDerivation;
        }

        /// <summary>
        /// Try to proove a statement using the provided solving method
        /// can sometimes throw LogicDerivationNoConclusionException
        /// </summary>
        /// <param name="derivation">logic derivation</param>
        /// <param name="statement">statement to prove or disprove</param>
        /// <param name="solvingMethog">solving method</param>
        /// <param name="proof">proof or refutation (can be null if no proof was found)</param>
        /// <returns>whether the statement was true or false
        /// can sometimes throw LogicDerivationNoConclusionException</returns>
        public bool TryProove(LogicDerivation derivation, string statement, SolvingMethod solvingMethog, out IEnumerable<TreeExpression> proof)
        {
            proof = null;
            bool isSatisfied = false;
            TreeExpression parsedStatement = parser.Parse(statement);
            Derive(derivation);

            if (solvingMethog == SolvingMethod.Straight)
            {
                isSatisfied = evaluator.IsSatisfied(parsedStatement, derivation, out proof);
                if (!isSatisfied)
                {
                    TreeExpression negatedStatement = negator.Negate(parsedStatement);
                    if (!evaluator.IsSatisfied(negatedStatement, derivation, out proof))
                    {
                        throw new LogicDerivationNoConclusionException("Could find a decent conclusion");
                    }
                    List<TreeExpression> proofFromNegatedStatement = new List<TreeExpression>();
                    proofFromNegatedStatement.Add(derivation[negatedStatement.ToString()]);
                    proof = proofFromNegatedStatement;
                }
            }
            else if (solvingMethog == SolvingMethod.ReductioAdAbsurdum)
            {
                LogicDerivation derivationToCorrupt = new LogicDerivation(derivation);
                TreeExpression negatedStatement = negator.Negate(parsedStatement);
                if (!derivationToCorrupt.Contains(negatedStatement))
                    derivationToCorrupt.Add(negatedStatement);
                Derive(derivationToCorrupt);

                bool isFoundInconsistency = false;
                foreach (TreeExpression expression in derivationToCorrupt)
                {
                    TreeExpression negatedExpression = negator.Negate(expression);
                    if (derivationToCorrupt.Contains(negatedExpression))
                    {
                        List<TreeExpression> proofFromAbsurd = new List<TreeExpression>();
                        proofFromAbsurd.Add(expression);
                        proofFromAbsurd.Add(negatedExpression);
                        isFoundInconsistency = true;
                        proof = proofFromAbsurd;
                        isSatisfied = true;
                        break;
                    }
                }

                if (!isFoundInconsistency)
                {
                    derivationToCorrupt = new LogicDerivation(derivation);
                    if (!derivationToCorrupt.Contains(parsedStatement))
                        derivationToCorrupt.Add(parsedStatement);
                    Derive(derivationToCorrupt);

                    foreach (TreeExpression expression in derivationToCorrupt)
                    {
                        TreeExpression negatedExpression = negator.Negate(expression);
                        if (derivationToCorrupt.Contains(negatedExpression))
                        {
                            List<TreeExpression> proofFromAbsurd = new List<TreeExpression>();
                            proofFromAbsurd.Add(expression);
                            proofFromAbsurd.Add(negatedExpression);
                            isFoundInconsistency = true;
                            proof = proofFromAbsurd;
                            isSatisfied = false;
                            break;
                        }
                    }
                }

                if (!isFoundInconsistency)
                {
                    throw new LogicDerivationNoConclusionException("Could find a decent conclusion");
                }
            }

            return isSatisfied;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Expand new proposition from logic derivation
        /// </summary>
        /// <param name="logicDerivation">logic derivation</param>
        private void Derive(LogicDerivation logicDerivation)
        {
            bool isFoundNewProposition;
            do
            {
                isFoundNewProposition = false;

                temporaryPropositionList.Clear();
                temporaryPropositionList.UnionWith(logicDerivation);
                foreach (TreeExpression proposition in temporaryPropositionList)
                {
                    isFoundNewProposition |= splitAndOperatorManager.BuildSplitAnd(proposition, logicDerivation);
                    isFoundNewProposition |= modusPonensManager.BuildModusPonens(proposition, logicDerivation);
                    isFoundNewProposition |= hypotheticalSyllogismManager.BuildHypotheticalSyllogism(proposition, logicDerivation, temporaryPropositionList);
                    isFoundNewProposition |= modusTollensManager.BuildModusTollens(proposition, logicDerivation);
                    isFoundNewProposition |= disjunctiveSyllogismManager.BuildDisjunctiveSyllogism(proposition, logicDerivation);
                    isFoundNewProposition |= trueImplyFalseManager.BuildTrueImplyFalse(proposition, logicDerivation);
                    isFoundNewProposition |= aImplyBAndCManager.BuildAImplyBAndC(proposition, logicDerivation);
                    isFoundNewProposition |= aOrBImplyCManager.BuildAOrBImplyC(proposition, logicDerivation);
                }

            } while (isFoundNewProposition);
        }
        #endregion
    }
}

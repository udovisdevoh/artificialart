using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.PathFinding
{
    /// <summary>
    /// Represents an AStar node
    /// </summary>
    public class Node
    {
        #region Fields
        /// <summary>
        /// Previous node
        /// </summary>
        private Node previous;

        /// <summary>
        /// Node's state
        /// </summary>
        private INodeState state;

        /// <summary>
        /// Lowest cost from source
        /// </summary>
        private float lowestCostFromSource;

        /// <summary>
        /// Estimated cost to destination
        /// </summary>
        private float estimatedCostToDestination;
        #endregion

        #region Constructors
        /// <summary>
        /// Build AStar node
        /// </summary>
        /// <param name="previous">previous node</param>
        /// <param name="state">node's state</param>
        /// <param name="lowestCostFromSource">lowest cost from source</param>
        /// <param name="estimatedCostToDestination">estimated cost to destination</param>
        public Node(Node previous, INodeState state, float lowestCostFromSource, float estimatedCostToDestination)
        {
            this.previous = previous;
            this.state = state;
            this.lowestCostFromSource = lowestCostFromSource;
            this.estimatedCostToDestination = estimatedCostToDestination;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Update previous node and lowest cost from source
        /// </summary>
        /// <param name="previous">previous node</param>
        /// <param name="lowestCostFromSource">lowest cost from source</param>
        public void Update(Node previous, float lowestCostFromSource)
        {
            this.previous = previous;
            this.lowestCostFromSource = lowestCostFromSource;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Previous node
        /// </summary>
        public Node Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        /// <summary>
        /// Node's state
        /// </summary>
        public INodeState State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Lowest cost from source
        /// </summary>
        public float LowestCostFromSource
        {
            get { return lowestCostFromSource; }
            set { lowestCostFromSource = value; }
        }

        /// <summary>
        /// Estimated cost to destination
        /// </summary>
        public float EstimatedCostToDestination
        {
            get { return estimatedCostToDestination; }
            set { estimatedCostToDestination = value; }
        }

        /// <summary>
        /// Estimated total cost
        /// </summary>
        public float EstimatedTotalCost
        {
            get { return estimatedCostToDestination + lowestCostFromSource; }
        }
        #endregion
    }
}

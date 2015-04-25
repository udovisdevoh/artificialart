using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.PathFinding
{
    /// <summary>
    /// Represents an AStar path finder
    /// </summary>
    public class PathFinder
    {
        #region Fields and parts
        /// <summary>
        /// List of closed nodes
        /// </summary>
        private Dictionary<INodeState, Node> closedList;

        /// <summary>
        /// List of open nodes
        /// </summary>
        private Dictionary<INodeState, Node> openList;
        #endregion

        #region Constructor
        /// <summary>
        /// Build AStar Path Finder
        /// </summary>
        public PathFinder()
        {
            closedList = new Dictionary<INodeState, Node>();
            openList = new Dictionary<INodeState, Node>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get optimal node
        /// </summary>
        public Node GetOptimalNode()
        {
            Node optimalNode = null;
            float bestCost = 0;

            foreach (Node node in openList.Values)
            {
                if (node.EstimatedTotalCost < bestCost || optimalNode == null)
                {
                    optimalNode = node;
                    bestCost = node.EstimatedTotalCost;
                }
            }

            return optimalNode;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Open list
        /// </summary>
        public Dictionary<INodeState, Node> OpenList
        {
            get { return openList; }
        }

        /// <summary>
        /// Closed list
        /// </summary>
        public Dictionary<INodeState, Node> ClosedList
        {
            get { return closedList; }
        }
        #endregion
    }
}

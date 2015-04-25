using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.PathFinding
{
    /// <summary>
    /// Represents a node's state
    /// </summary>
    public interface INodeState
    {
        /// <summary>
        /// Whether node state is at destination
        /// </summary>
        bool IsDestination
        {
            get;
        }
    }
}

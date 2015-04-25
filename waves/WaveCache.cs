using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Waves
{
    /// <summary>
    /// To improve wave rendering performances
    /// </summary>
    internal class WaveCache
    {
        #region Fields
        /// <summary>
        /// Internal cache
        /// </summary>
        private Dictionary<double, double> internalCache = new Dictionary<double, double>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Whether cache contains value at x
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>Whether cache contains value at x</returns>
        public bool ContainsKey(double x)
        {
            return internalCache.ContainsKey(x);
        }

        /// <summary>
        /// Add key value pair to cache
        /// </summary>
        /// <param name="x">position/time index</param>
        /// <param name="value">value</param>
        public void Add(double x, double value)
        {
            if (!internalCache.ContainsKey(x))
                internalCache.Add(x, value);
        }

        /// <summary>
        /// Get value at position/time x
        /// </summary>
        /// <param name="x">position/time</param>
        /// <returns>value at position/time x</returns>
        public double Get(double x)
        {
            return internalCache[x];
        }
        #endregion
    }

}

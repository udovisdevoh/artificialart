using System;
using System.Collections;
using System.Text;

namespace ArtificialArt.Audio.Midi
{
    /// <summary>
    /// Chased event arguments
    /// </summary>
    public class ChasedEventArgs : EventArgs
    {
        /// <summary>
        /// Messages
        /// </summary>
        private ICollection messages;

        /// <summary>
        /// Chased event args
        /// </summary>
        /// <param name="messages">messages</param>
        public ChasedEventArgs(ICollection messages)
        {
            this.messages = messages;
        }

        /// <summary>
        /// Messages
        /// </summary>
        public ICollection Messages
        {
            get
            {
                return messages;
            }
        }
    }
}

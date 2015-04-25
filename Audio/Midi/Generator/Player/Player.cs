using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Audio.Midi;

namespace ArtificialArt.Audio.Midi.Generator
{
    /// <summary>
    /// Facade to play midi content
    /// </summary>
    public class Player
    {
        #region Fields and parts
        /// <summary>
        /// Song player
        /// </summary>
        private RiffPackPlayer riffPackPlayer;
        
        /// <summary>
        /// Midi output device
        /// </summary>
        private OutputDevice outputDevice = new OutputDevice(0);

        /// <summary>
        /// IRiff to play
        /// </summary>
        private IRiff iRiff = null;
        #endregion

        #region Event
        /// <summary>
        /// When playing note
        /// </summary>
        public event EventHandler OnNoteOn;

        /// <summary>
        /// When stopping note
        /// </summary>
        public event EventHandler OnNoteOff;

        /// <summary>
        /// When black note time has passed
        /// </summary>
        public event EventHandler OnBlackNoteTimeElapsed;
        #endregion

        #region Constructor
        /// <summary>
        /// Build midi player
        /// </summary>
        public Player()
        {
            riffPackPlayer = new RiffPackPlayer();
            riffPackPlayer.OnNoteOn += NoteOnHandler;
            riffPackPlayer.OnNoteOff += NoteOffHandler;
            riffPackPlayer.OnBlackNoteTimeElapsed += BlackNoteTimeElapsedHandler;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Play a riff or a riff pack
        /// </summary>
        /// <param name="stateInfo">when method is started by threadPool</param>
        public void Play(object stateInfo)
        {
            if (iRiff == null)
                throw new MidiPlayerException("Must set IRiff before playing");


            if (iRiff is Riff)
            {
                RiffPack riffPack = new RiffPack(iRiff);
                riffPackPlayer.Play(riffPack, outputDevice);
            }
            else if (iRiff is RiffPack)
            {
                RiffPack riffPack = (RiffPack)iRiff;
                riffPackPlayer.Play(riffPack, outputDevice);
            }
            else
            {
                throw new Exception("Unrecognized IRiff implementation");
            }
        }

        /// <summary>
        /// Stop music
        /// </summary>
        public void Stop()
        {
            riffPackPlayer.Stop();
        }

        /// <summary>
        /// Remove event listeners
        /// </summary>
        public void ClearEventHandlers()
        {
            this.OnNoteOn = null;
            this.OnNoteOff = null;
            this.OnBlackNoteTimeElapsed = null;
        }
        #endregion

        #region Event Handlers
        private void NoteOnHandler(object source, EventArgs e)
        {
            if (OnNoteOn != null) OnNoteOn(source, e);
        }

        private void NoteOffHandler(object source, EventArgs e)
        {
            if (OnNoteOff != null) OnNoteOff(source, e);
        }

        private void BlackNoteTimeElapsedHandler(object source, EventArgs e)
        {
            if (OnBlackNoteTimeElapsed != null) OnBlackNoteTimeElapsed(source, e);
        }
        #endregion

        #region Properties
        /// <summary>
        /// IRiff to play
        /// </summary>
        public IRiff IRiff
        {
            get { return iRiff; }
            set { iRiff = value; }
        }

        /// <summary>
        /// Whether the player is playing
        /// </summary>
        public bool IsPlaying
        {
            get { return riffPackPlayer.IsPlaying; }
        }
        #endregion
    }
}
#region License

/* Copyright (c) 2006 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArtificialArt.Audio.Midi.UI
{
    /// <summary>
    /// Device dialog
    /// </summary>
    public partial class DeviceDialog : Form
    {
        /// <summary>
        /// Input device id
        /// </summary>
        private int inputDeviceID = 0;

        /// <summary>
        /// Output device id
        /// </summary>
        private int outputDeviceID = 0;

        /// <summary>
        /// Device dialog
        /// </summary>
        public DeviceDialog()
        {
            InitializeComponent();

            if(InputDevice.DeviceCount > 0)
            {
                for(int i = 0; i < InputDevice.DeviceCount; i++)
                {
                    inputComboBox.Items.Add(InputDevice.GetDeviceCapabilities(i).name);
                }

                inputComboBox.SelectedIndex = inputDeviceID;
            }

            if(OutputDevice.DeviceCount > 0)
            {
                for(int i = 0; i < OutputDevice.DeviceCount; i++)
                {
                    outputComboBox.Items.Add(OutputDevice.GetDeviceCapabilities(i).name);
                }

                outputComboBox.SelectedIndex = inputDeviceID;
            }
        }

        /// <summary>
        /// When shown
        /// </summary>
        /// <param name="e">event arguments</param>
        protected override void OnShown(EventArgs e)
        {
            if(InputDevice.DeviceCount > 0)
            {
                inputComboBox.SelectedIndex = inputDeviceID;
            }

            if(OutputDevice.DeviceCount > 0)
            {
                outputComboBox.SelectedIndex = outputDeviceID;
            }

            base.OnShown(e);
        }

        /// <summary>
        /// Ok button click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if(InputDevice.DeviceCount > 0)
            {
                inputDeviceID = inputComboBox.SelectedIndex;
            }

            if(OutputDevice.DeviceCount > 0)
            {
                outputDeviceID = outputComboBox.SelectedIndex;
            }

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Cancel button click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Input device id
        /// </summary>
        public int InputDeviceID
        {
            get
            {
                #region Require

                if(InputDevice.DeviceCount == 0)
                {
                    throw new InvalidOperationException();
                }

                #endregion

                return inputDeviceID;
            }
        }

        /// <summary>
        /// Output device id
        /// </summary>
        public int OutputDeviceID
        {
            get
            {
                #region Require

                if(OutputDevice.DeviceCount == 0)
                {
                    throw new InvalidOperationException();
                }

                #endregion

                return outputDeviceID;
            }
        }
    }
}
// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-15-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="ConnectionForm.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Windows.Forms;

/// <summary>
/// The MusicPickerDeviceApp namespace.
/// </summary>
namespace MusicPickerDeviceApp
{
    /// <summary>
    /// Class ConnectionForm.
    /// </summary>
    public partial class ConnectionForm : Form
    {
        /// <summary>
        /// Delegate ConnectEvent
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="password">The password.</param>
        public delegate void ConnectEvent(string username, string deviceName, string password);
        /// <summary>
        /// The callback to use when the user wants to connect to the service.
        /// </summary>
        private ConnectEvent callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionForm"/> class.
        /// </summary>
        /// <param name="callback">The ConnectEvent callback.</param>
        public ConnectionForm(ConnectEvent callback)
        {
            this.callback = callback;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Connect button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            callback(username.Text, device.Text, password.Text);
            this.Close();
        }
    }
}

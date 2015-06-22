// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-18-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="SignUpForm.cs" company="Hutopi">
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
    /// Class SignUpForm.
    /// </summary>
    public partial class SignUpForm : Form
    {
        /// <summary>
        /// Delegate SignUpEvent
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmpassword">The confirmpassword.</param>
        public delegate void SignUpEvent(string username, string password, string confirmpassword);
        /// <summary>
        /// The callback to use when the user wants to sign up.
        /// </summary>
        private SignUpEvent callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpForm"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public SignUpForm(SignUpEvent callback)
        {
            this.callback = callback;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Sign up button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            callback(Username.Text, Password.Text, ConfirmPassword.Text);
            Close();
        }
    }
}

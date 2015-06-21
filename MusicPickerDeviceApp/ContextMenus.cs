// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-15-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="ContextMenus.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Windows.Forms;
using MusicPickerDeviceApp.Properties;
using System.Drawing;

/// <summary>
/// The MusicPickerDeviceApp namespace.
/// </summary>
namespace MusicPickerDeviceApp
{

    /// <summary>
    /// Class ContextMenus.
    /// Represents the menus which are in the tray application
    /// </summary>
    public class ContextMenus
    {
        /// <summary>
        /// The connection form
        /// </summary>
        public ConnectionForm ConnectionForm;
        /// <summary>
        /// The sign up form
        /// </summary>
        public SignUpForm SignUpForm;
        /// <summary>
        /// The collection load form
        /// </summary>
        public LibraryPathsForm LoadForm;
        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>The menu.</value>
        public ContextMenuStrip Menu { get; set; }

        /// <summary>
        /// The connect tool strip
        /// </summary>
        private ToolStripMenuItem connectToolStrip;
        /// <summary>
        /// The sign up tool strip
        /// </summary>
        private ToolStripMenuItem signUpToolStrip;
        /// <summary>
        /// The exit tool strip
        /// </summary>
        private ToolStripMenuItem exitToolStrip;

        /// <summary>
        /// Delegate LogoutEvent
        /// </summary>
        public delegate void LogoutEvent();
        /// <summary>
        /// The callback for the logout event
        /// </summary>
        private LogoutEvent callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenus"/> class.
        /// </summary>
        public ContextMenus()
        {
            Menu = new ContextMenuStrip();

            exitToolStrip = new ToolStripMenuItem()
            {
                Text = "Exit",
                Image = Resources.exit
            };
            exitToolStrip.Click += new System.EventHandler(Exit_Click);
        }

        /// <summary>
        /// Shows the unauthenticated menu.
        /// </summary>
        public void ShowUnauthenticatedMenu()
        {
            if (Menu.Items.Count > 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    DeleteMenu(0);
                }
            }
            
            signUpToolStrip = new ToolStripMenuItem()
            {
                Text = "Sign up",
                Image = Resources._in,

            };
            signUpToolStrip.Click += new EventHandler(SignUp_Click);
            Menu.Items.Add(signUpToolStrip);

            connectToolStrip = new ToolStripMenuItem()
            {
                Text = "Connect",
                Image = Resources._in,
                
            };
            connectToolStrip.Click += new EventHandler(Connection_Click);
            Menu.Items.Add(connectToolStrip);

            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(exitToolStrip);
        }

        /// <summary>
        /// Shows the authenticated menu.
        /// </summary>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="uploading">if set to <c>true</c> [uploading].</param>
        /// <param name="callback">The callback.</param>
        public void ShowAuthenticatedMenu(string deviceName, bool uploading, LogoutEvent callback)
        {
            Menu.Items.Clear();
            AddMenu(string.Format("{0}", deviceName), 0, Resources.device, Device_Click);

            if (!uploading)
            {
                AddMenu("Upload Music", 1, Resources.upload, Load_Click);
            }
            else
            {
                Menu.Items.Insert(1, new ToolStripMenuItem()
                {
                    Text = "Uploading...",
                    Enabled = false
                });
            }

            AddMenu("Logout", 2, Resources._out, Logout_Click);
            this.callback = callback;
            
            Menu.Items.Add(exitToolStrip);
        }

        /// <summary>
        /// Adds a menu.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="index">The index.</param>
        /// <param name="img">The img.</param>
        /// <param name="e">The e.</param>
        public void AddMenu(string name, int index, Bitmap img, EventHandler e)
        {
            ToolStripMenuItem item;
            item = new ToolStripMenuItem();
            item.Text = name;
            item.Image = img;
            item.Click += new System.EventHandler(e);
            Menu.Items.Insert(index, item);
        }

        /// <summary>
        /// Deletes the menu.
        /// </summary>
        /// <param name="index">The index.</param>
        public void DeleteMenu(int index)
        {
            Menu.Items.RemoveAt(index);
        }


        /// <summary>
        /// Handles the Click event of the Connection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Connection_Click(object sender, EventArgs e)
        {
            ConnectionForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the SignUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void SignUp_Click(object sender, EventArgs e)
        {
            SignUpForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the Load control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Load_Click(object sender, EventArgs e)
        {
            LoadForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the Device control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Device_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Click event of the Logout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Logout_Click(object sender, EventArgs e)
        {
            this.callback();
        }

        /// <summary>
        /// Handles the Click event of the Exit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
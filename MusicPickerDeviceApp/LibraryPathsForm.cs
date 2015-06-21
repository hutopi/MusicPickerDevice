// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-18-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="LibraryPathsForm.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;

/// <summary>
/// The MusicPickerDeviceApp namespace.
/// </summary>
namespace MusicPickerDeviceApp
{
    /// <summary>
    /// Class LibraryPathsForm.
    /// </summary>
    public partial class LibraryPathsForm : Form
    {
        /// <summary>
        /// Delegate LibraryPathsEvent
        /// </summary>
        /// <param name="paths">The paths of the folders containing the musics to send.</param>
        public delegate void LibraryPathsEvent(List<string> paths);
        /// <summary>
        /// The callback
        /// </summary>
        private LibraryPathsEvent callback;
        /// <summary>
        /// The configuration model of the device.
        /// </summary>
        private ConfigurationModel configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryPathsForm"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="callback">The callback.</param>
        public LibraryPathsForm(ConfigurationModel configuration, LibraryPathsEvent callback)
        {
            this.callback = callback;
            this.configuration = configuration;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the LinkClicked event of the "Select a folder" linkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                configuration.Paths.Add(folderBrowser.SelectedPath);
                foldersLabel.Text += string.Format("- {0} \n", folderBrowser.SelectedPath);
            }
        }

        /// <summary>
        /// Handles the click event of the button Load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonLoad_Click_1(object sender, EventArgs e)
        {
            callback(configuration.Paths);
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the LibraryPathsForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LibraryPathsForm_Load(object sender, EventArgs e)
        {
            foldersLabel.Text = "";
            this.Box.Items.Clear();
            foreach (var path in configuration.Paths)
            {
                this.Box.Items.Add(new TextBox()
                {
                    Text = string.Format(path)
                }.Text);
            }
        }

        /// <summary>
        /// Handles the Click event of the Delete button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            var path = this.Box.SelectedItem;
            configuration.Paths.Remove((string)path);

            callback(configuration.Paths);
            Box.Items.Remove(path);
        }
    }
}

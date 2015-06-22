// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-18-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="FileWatcher.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Linq;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// FileWatcher class.
    /// </summary>
    public class FileWatcher
    {
        /// <summary>
        /// The file system watcher
        /// </summary>
        private FileSystemWatcher fileWatcher;
        /// <summary>
        /// The extensions for the searched files
        /// </summary>
        private string[] extensions = { ".mp3", ".wav", ".flac" };
        /// <summary>
        /// The path of the inspected folder
        /// </summary>
        private string path;

        /// <summary>
        /// Delegate FileAddedEvent
        /// </summary>
        /// <param name="path">The path.</param>
        public delegate void FileAddedEvent(string path);
        /// <summary>
        /// The callback which is called when a file is added to the folder
        /// </summary>
        private FileAddedEvent callbackAdded;

        /// <summary>
        /// Delegate FileDeletedEvent
        /// </summary>
        /// <param name="path">The path.</param>
        public delegate void FileDeletedEvent(string path);
        /// <summary>
        /// The callback whick is called when a file is deleted from the folder
        /// </summary>
        private FileDeletedEvent callbackDeleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWatcher"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="add">The add.</param>
        /// <param name="delete">The delete.</param>
        public FileWatcher(string path, FileAddedEvent add, FileDeletedEvent delete)
        {
            this.path = path;
            callbackAdded = add;
            callbackDeleted = delete;
            fileWatcher = new FileSystemWatcher(path, "*.*");
            fileWatcher.Created += new FileSystemEventHandler(FileWatcherCreated);
            fileWatcher.Deleted += new FileSystemEventHandler(FileWatcherDeleted);
            fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Called when a file is added to the folder
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public void FileWatcherCreated(object sender, FileSystemEventArgs e)
        {
            var ext = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();

            if (extensions.Any(ext.Equals))
            {
                callbackAdded(e.FullPath);
            }
        }

        /// <summary>
        /// Called when a file is deleted from the folder
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public void FileWatcherDeleted(object sender, FileSystemEventArgs e)
        {
            var ext = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();

            if (extensions.Any(ext.Equals))
            {
                callbackDeleted(e.FullPath);
            }
        }
    }
}

// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-15-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="Seeker.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that is used to search the musics in the folders
    /// </summary>
    public class Seeker
    {
        /// <summary>
        /// The library that contains the tracks
        /// </summary>
        private Library library;
        /// <summary>
        /// The allowed extensions of the searched files
        /// </summary>
        private string[] allowedExtensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Seeker"/> class.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="allowedExtensions">The allowed extensions.</param>
        public Seeker(Library library, string[] allowedExtensions)
        {
            this.library = library;
            this.allowedExtensions = allowedExtensions;
        }

        /// <summary>
        /// Iterates the paths.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public IEnumerable<string> IteratePaths(string directory)
        {
            return Directory
                .GetFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(file => this.allowedExtensions.Any(file.ToLower().EndsWith));
        }

        /// <summary>
        /// Create a Track and adds it to the library
        /// </summary>
        /// <param name="directory">The directory.</param>
        public void GetTracks(string directory)
        {
            List<LibraryTrack> tracks = new List<LibraryTrack>();

            foreach (string filePath in IteratePaths(directory))
            {
                try
                {
                    TagLib.File tagFile = TagLib.File.Create(filePath);
                    tracks.Add(new LibraryTrack()
                    {
                        Artist = tagFile.Tag.FirstArtist,
                        Album = tagFile.Tag.Album,
                        Title = tagFile.Tag.Title,
                        Genre = tagFile.Tag.FirstGenre,
                        Year = tagFile.Tag.Year,
                        Number = tagFile.Tag.Track,
                        Count = tagFile.Tag.TrackCount,
                        Duration = (int)tagFile.Properties.Duration.TotalSeconds,
                        Path = filePath
                    });
                }
                catch
                {
                    continue;
                }
            }

            this.library.AddTracks(tracks);
        }
    }
}
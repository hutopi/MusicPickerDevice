// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-20-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-20-2015
// ***********************************************************************
// <copyright file="Album.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents an album.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Gets or sets the identifier of the album.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the album.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the year of the album.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }
        /// <summary>
        /// Gets or sets the MBLD of the album (MusicBrainz).
        /// </summary>
        /// <value>The MBLD.</value>
        public string Mbld { get; set; }
        /// <summary>
        /// Gets or sets the URL of the artwork album.
        /// </summary>
        /// <value>The artwork URL.</value>
        public string Artwork { get; set; }
        /// <summary>
        /// Gets or sets the artist identifier of the album.
        /// </summary>
        /// <value>The artist identifier.</value>
        public int ArtistId { get; set; }
    }
}

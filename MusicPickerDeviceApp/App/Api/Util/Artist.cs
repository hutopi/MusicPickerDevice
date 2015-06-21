// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-20-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-20-2015
// ***********************************************************************
// <copyright file="Artist.cs" company="Hutopi">
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
    /// Class that represents an artist.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Gets or sets the identifier of the artist.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the MBLD of the artists (MusicBrainz).
        /// </summary>
        /// <value>The MBLD.</value>
        public string Mbld { get; set; }
    }
}

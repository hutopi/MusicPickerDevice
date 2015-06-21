// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-20-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-20-2015
// ***********************************************************************
// <copyright file="Track.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Util namespace.
/// </summary>
namespace MusicPickerDeviceApp.App.Api.Util
{
    /// <summary>
    /// Class that represents a track.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Gets or sets the identifier of the track.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the track.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the number of the track if it belongs to an album.
        /// </summary>
        /// <value>The number.</value>
        public int Number { get; set; }
        /// <summary>
        /// Gets or sets the mb identifier of the track (MusicBrainz).
        /// </summary>
        /// <value>The mb identifier.</value>
        public string MbId { get; set; }
        /// <summary>
        /// Gets or sets the album identifier of the track.
        /// </summary>
        /// <value>The album identifier.</value>
        public int AlbumId { get; set; }
    }
}

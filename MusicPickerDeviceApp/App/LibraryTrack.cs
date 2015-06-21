// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-20-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-20-2015
// ***********************************************************************
// <copyright file="LibraryTrack.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LiteDB;
using Newtonsoft.Json;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents a Track for the database.
    /// </summary>
    public class LibraryTrack
    {
        /// <summary>
        /// Gets or sets the identifier of the track.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonConverter(typeof(ToStringJsonConverter))]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Gets or sets the artist of the track.
        /// </summary>
        /// <value>The artist.</value>
        public string Artist { get; set; }
        /// <summary>
        /// Gets or sets the album of the track.
        /// </summary>
        /// <value>The album.</value>
        public string Album { get; set; }
        /// <summary>
        /// Gets or sets the title of the track.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the genre of the track.
        /// </summary>
        /// <value>The genre.</value>
        public string Genre { get; set; }
        /// <summary>
        /// Gets or sets the year of the track.
        /// </summary>
        /// <value>The year.</value>
        public uint Year { get; set; }
        /// <summary>
        /// Gets or sets the number of the track.
        /// </summary>
        /// <value>The number.</value>
        public uint Number { get; set; }
        /// <summary>
        /// Gets or sets the count of the track.
        /// </summary>
        /// <value>The count.</value>
        public uint Count { get; set; }
        /// <summary>
        /// Gets or sets the duration of the track.
        /// </summary>
        /// <value>The duration.</value>
        public int Duration { get; set; }
        /// <summary>
        /// Gets or sets the path of the track.
        /// </summary>
        /// <value>The path.</value>
        [BsonIndex]
        public string Path { get; set; }
    }
}
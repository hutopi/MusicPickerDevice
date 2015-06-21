// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-15-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="Library.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using LiteDB;
using Newtonsoft.Json;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents the library (all tracks).
    /// </summary>
    public class Library
    {
        /// <summary>
        /// The database
        /// </summary>
        private LiteDatabase database;

        /// <summary>
        /// The tracks
        /// </summary>
        public LiteCollection<LibraryTrack> Tracks;

        /// <summary>
        /// Initializes a new instance of the <see cref="Library"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public Library(LiteDatabase database)
        {
            this.database = database;
            this.Tracks = this.database.GetCollection<LibraryTrack>("Tracks");
        }

        /// <summary>
        /// Adds the track.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <returns>System.String.</returns>
        public string AddTrack(LibraryTrack track)
        {
            this.Tracks.Insert(track);
            return track.Id.ToString();
        }

        /// <summary>
        /// Adds the tracks.
        /// </summary>
        /// <param name="tracks">The tracks.</param>
        public void AddTracks(List<LibraryTrack> tracks)
        {
            this.Tracks.InsertBulk(tracks);
        }

        /// <summary>
        /// Gets the track.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>LibraryTrack.</returns>
        public LibraryTrack GetTrack(string id)
        {
            return this.Tracks.FindById(new ObjectId(id));
        }

        /// <summary>
        /// Deletes the track.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DeleteTrack(string id)
        {
            return this.Tracks.Delete(new ObjectId(id));
        }

        /// <summary>
        /// Determines whether [is path present] [the specified path].
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if [is path present] [the specified path]; otherwise, <c>false</c>.</returns>
        public bool IsPathPresent(string path)
        {
            return this.Tracks.Exists(Query.EQ("Path", path));
        }

        /// <summary>
        /// Erases this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Erase()
        {
            return this.Tracks.Drop();
        }

        /// <summary>
        /// Exports this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Export()
        {
            return JsonConvert.SerializeObject(this.Tracks.FindAll());
        }
    }
}
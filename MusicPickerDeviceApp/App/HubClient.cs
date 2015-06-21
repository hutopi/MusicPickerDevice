// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-18-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="HubClient.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.SignalR.Client;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents the Hub client.
    /// </summary>
    public class HubClient
    {
        /// <summary>
        /// The music player
        /// </summary>
        private Player player;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubClient"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public HubClient(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            player.Play();
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            player.Pause();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            player.Stop();
        }

        /// <summary>
        /// Sets the track identifier.
        /// </summary>
        /// <param name="current">The current.</param>
        public void SetTrackId(string current)
        {
            player.SetTrack(current);
        }

        /// <summary>
        /// Attaches to hub.
        /// </summary>
        /// <param name="hub">The hub.</param>
        public void AttachToHub(IHubProxy hub)
        {
            hub.On("Play", Play);
            hub.On("Pause", Pause);
            hub.On("Stop", Stop);
            hub.On("SetTrackId", (id) => SetTrackId(id));
        }
    }
}
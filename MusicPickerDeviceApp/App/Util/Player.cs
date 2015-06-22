// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-15-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="Player.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using NAudio.Wave;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// The musics player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the waveout device.
        /// </summary>
        /// <value>The wave out device.</value>
        private IWavePlayer waveOutDevice { get; set; }
        /// <summary>
        /// The library that contains the tracks
        /// </summary>
        private Library library;

        /// <summary>
        /// Gets or sets the current song.
        /// </summary>
        /// <value>The current song.</value>
        public string CurrentSong { get; set; }
        /// <summary>
        /// Delegate NextTrackEvent
        /// </summary>
        public delegate void NextTrackEvent();
        /// <summary>
        /// The next callback
        /// </summary>
        private NextTrackEvent nextCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="library">The library.</param>
        public Player(Library library)
        {
            waveOutDevice = new WaveOutEvent();
            this.library = library;
            waveOutDevice = new WaveOutEvent();
        }

        /// <summary>
        /// Sets the track.
        /// </summary>
        /// <param name="trackId">The track identifier.</param>
        public void SetTrack(string trackId)
        {
            CurrentSong = trackId;
            LibraryTrack track = library.GetTrack(trackId);
            if (track != null)
            {
                WaveStream stream = new AudioFileReader(track.Path);
                waveOutDevice.Init(stream);
            }
            else
            {
                nextCallback();
            }
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public void Play()
        {
            try
            {
                waveOutDevice.Play();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            waveOutDevice.Pause();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            waveOutDevice.Stop();
        }

        /// <summary>
        /// Attaches the next callback.
        /// </summary>
        /// <param name="nextCallback">The next callback.</param>
        public void AttachNextCallback(NextTrackEvent nextCallback)
        {
            this.nextCallback = nextCallback;
            waveOutDevice.PlaybackStopped += (sender, ev) => nextCallback();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace MusicPickerDeviceApp.App
{
    public class Player
    {
        private IWavePlayer waveOutDevice { get; set; }
        private Library library;
        public string CurrentSong { get; set; }
        public delegate void NextTrackEvent();
        private NextTrackEvent nextCallback;

        public Player(Library library)
        {
            waveOutDevice = new WaveOutEvent();
            this.library = library;
            waveOutDevice = new WaveOutEvent();
        }

        public void SetTrack(string trackId)
        {
            this.CurrentSong = trackId;
            Track track = this.library.GetTrack(trackId);
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

        public void Pause()
        {
            waveOutDevice.Pause();
        }

        public void Stop()
        {
            waveOutDevice.Stop();
        }

        public void AttachNextCallback(NextTrackEvent nextCallback)
        {
            this.nextCallback = nextCallback;
            waveOutDevice.PlaybackStopped += (sender, ev) => nextCallback();
        }
    }
}
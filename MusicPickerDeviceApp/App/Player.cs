using System;
using System.Collections.Generic;
using NAudio.Wave;

namespace MusicPickerDeviceApp.App
{
    public class Player
    {

        private IWavePlayer waveOutDevice { get; set; }
        private Library library;
        public string CurrentSong { get; set; }
        public delegate void NextTrackEvent();

        public Player(Library library)
        {
            waveOutDevice = new WaveOutEvent();
            this.library = library;
            waveOutDevice = new WaveOutEvent();
        }

        public void SetTrack(string trackId)
        {
            Console.WriteLine("SET TRACK");
            this.CurrentSong = trackId;
            Track track = this.library.GetTrack(trackId);
            WaveStream stream = new AudioFileReader(track.Path);
            waveOutDevice.Init(stream);
        }

        public void Play()
        {
            Console.WriteLine("PLAY");
            waveOutDevice.Play();
        }

        public void Pause()
        {
            Console.WriteLine("PAUSE");
            waveOutDevice.Pause();
        }

        public void Stop()
        {
            waveOutDevice.Stop();
        }

        public void AttachNextCallback(NextTrackEvent nextCallback)
        {
            waveOutDevice.PlaybackStopped += (sender, ev) => nextCallback();
        }
    }
}
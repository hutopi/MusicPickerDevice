using System.Collections.Generic;
using NAudio.Wave;

namespace MusicPickerDeviceApp.App
{
    public class Player
    {

        private IWavePlayer waveOutDevice { get; set; }
        private Library library;
        public string CurrentSong { get; set; }
        public bool Paused { get; set; }

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
            WaveStream stream = new AudioFileReader(track.Path);
            waveOutDevice.Init(stream);
        }

        public void Play()
        {
            waveOutDevice.Play();
        }

        public void Pause()
        {
            waveOutDevice.Pause();
        }
    }
}
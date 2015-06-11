using System.Collections.Generic;
using NAudio.Wave;

namespace MusicPickerDevice
{
    public class Player
    {

        private IWavePlayer waveOutDevice { get; set; }
        private WaveStream fileWaveStream;
        private Queue<string> playlist;

        public Player()
        {
            waveOutDevice = new WaveOutEvent();
            playlist = new Queue<string>();
        }

        public void AddSong(string path)
        {
            playlist.Enqueue(path);
        }

        public void AddSongs(List<Track> tracks)
        {
            foreach (Track t in tracks)
            {
                playlist.Enqueue(t.Path);
            }
        }

        public void PlaySong()
        {
            if (fileWaveStream != null)
            {
                fileWaveStream.Dispose();
            }

            if (playlist.Count < 1)
            {
                return;
            }

            if (waveOutDevice != null && waveOutDevice.PlaybackState != PlaybackState.Stopped)
            {
                waveOutDevice.Stop();
            }

            if (waveOutDevice != null)
            {
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }

            waveOutDevice = new WaveOutEvent();
            fileWaveStream = new AudioFileReader(playlist.Dequeue());
            waveOutDevice.Init(fileWaveStream);
            waveOutDevice.PlaybackStopped += (sender, evn) => { PlaySong(); };
            waveOutDevice.Play();
        }

        public void Stop()
        {
            waveOutDevice.Stop();
        }

        public void Pause()
        {
            waveOutDevice.Pause();
        }
    }
}
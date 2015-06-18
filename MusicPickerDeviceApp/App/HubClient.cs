using System;
using Microsoft.AspNet.SignalR.Client;

namespace MusicPickerDeviceApp.App
{
    public class HubClient
    {
        private Player player;

        public HubClient(Player player)
        {
            this.player = player;
        }

        public void Play()
        {
            player.Play();
        }

        public void Pause()
        {
            player.Pause();
        }

        public void Stop()
        {
            player.Stop();
        }

        public void SetTrackId(string current)
        {
            player.SetTrack(current);
        }

        public void AttachToHub(IHubProxy hub)
        {
            hub.On("Play", Play);
            hub.On("Pause", Pause);
            hub.On("Stop", Stop);
            hub.On("SetTrackId", (id) => SetTrackId(id));
        }
    }
}
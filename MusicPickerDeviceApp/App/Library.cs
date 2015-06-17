using System;
using System.Collections.Generic;
using LiteDB;
using NAudio.CoreAudioApi.Interfaces;
using Newtonsoft.Json;

namespace MusicPickerDeviceApp.App
{
    public class Library
    {
        private LiteDatabase database;
        private LiteCollection<Track> tracks;

        public Library(LiteDatabase database)
        {
            this.database = database;
            this.tracks = this.database.GetCollection<Track>("tracks");
        }

        public string AddTrack(Track track)
        {
            this.tracks.Insert(track);
            return track.Id.ToString();
        }

        public Track GetTrack(string id)
        {
            return this.tracks.FindById(new ObjectId(id));
        }

        public bool DeleteTrack(string id)
        {
            return this.tracks.Delete(new ObjectId(id));
        }

        public bool IsPathPresent(string path)
        {
            return this.tracks.Exists(Query.EQ("Path", path));
        }

        public bool Erase()
        {
            return this.tracks.Drop();
        }

        public string Export()
        {
            return JsonConvert.SerializeObject(this.tracks.FindAll());
        }
    }
}
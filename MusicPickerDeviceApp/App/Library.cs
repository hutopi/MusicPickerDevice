using System.Collections.Generic;
using LiteDB;
using Newtonsoft.Json;

namespace MusicPickerDeviceApp.App
{
    public class Library
    {
        private LiteDatabase database;

        public LiteCollection<LibraryTrack> Tracks;

        public Library(LiteDatabase database)
        {
            this.database = database;
            this.Tracks = this.database.GetCollection<LibraryTrack>("Tracks");
        }

        public string AddTrack(LibraryTrack track)
        {
            this.Tracks.Insert(track);
            return track.Id.ToString();
        }

        public void AddTracks(List<LibraryTrack> tracks)
        {
            this.Tracks.InsertBulk(tracks);
        }

        public LibraryTrack GetTrack(string id)
        {
            return this.Tracks.FindById(new ObjectId(id));
        }

        public bool DeleteTrack(string id)
        {
            return this.Tracks.Delete(new ObjectId(id));
        }

        public bool IsPathPresent(string path)
        {
            return this.Tracks.Exists(Query.EQ("Path", path));
        }

        public bool Erase()
        {
            return this.Tracks.Drop();
        }

        public string Export()
        {
            return JsonConvert.SerializeObject(this.Tracks.FindAll());
        }
    }
}
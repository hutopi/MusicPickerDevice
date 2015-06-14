using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio;
using NAudio.Wave;

namespace MusicPickerDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            var allowedExtensions = new[] { "mp3", /*"flac",*/ "wav" };
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            List<string> musics = Seeker.GetMusics(allowedExtensions, path);
            List<Track> tracks = Seeker.GetTracks(musics);

            Library library = new Library("library.db");
            library.Erase();

            foreach (Track track in tracks)
            {
                library.AddTrack(track);
            }

            Console.WriteLine(library.Export());
        }
    }
}

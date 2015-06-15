using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPickerDeviceApp.App
{
    public class Seeker
    {
        public static List<string> GetMusics(string[] allowedExtensions, string directory)
        {
            var files = Directory
                .GetFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                .ToList();

            return files;
        }

        public static List<Track> GetTracks(List<string> musics)
        {
            var tracks = new List<Track>();

            foreach (string path in musics)
            {
                TagLib.File tagFile = TagLib.File.Create(path);
                tracks.Add(new Track()
                {
                    Artist = tagFile.Tag.FirstArtist,
                    Album = tagFile.Tag.Album,
                    Title = tagFile.Tag.Title,
                    Genre = tagFile.Tag.FirstGenre,
                    Year = tagFile.Tag.Year,
                    Number = tagFile.Tag.Track,
                    Count = tagFile.Tag.TrackCount,
                    Path = path
                });
            }

            return tracks;
        }
    }
}
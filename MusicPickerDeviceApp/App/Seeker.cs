using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicPickerDeviceApp.App
{
    public class Seeker
    {
        private Library library;
        private string[] allowedExtensions;

        public Seeker(Library library, string[] allowedExtensions)
        {
            this.library = library;
            this.allowedExtensions = allowedExtensions;
        }

        public IEnumerable<string> IteratePaths(string directory)
        {
            return Directory
                .GetFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(file => this.allowedExtensions.Any(file.ToLower().EndsWith));
        }

        public void GetTracks(string directory)
        {
            List<LibraryTrack> tracks = new List<LibraryTrack>();

            foreach (string filePath in IteratePaths(directory))
            {
                try
                {
                    TagLib.File tagFile = TagLib.File.Create(filePath);
                    tracks.Add(new LibraryTrack()
                    {
                        Artist = tagFile.Tag.FirstArtist,
                        Album = tagFile.Tag.Album,
                        Title = tagFile.Tag.Title,
                        Genre = tagFile.Tag.FirstGenre,
                        Year = tagFile.Tag.Year,
                        Number = tagFile.Tag.Track,
                        Count = tagFile.Tag.TrackCount,
                        Duration = (int)tagFile.Properties.Duration.TotalSeconds,
                        Path = filePath
                    });
                }
                catch
                {
                    continue;
                }
            }

            this.library.AddTracks(tracks);
        }
    }
}
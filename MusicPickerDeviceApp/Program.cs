using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPickerDeviceApp
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();
                Application.Run();
            }
        }
    }
}

/*
 * Ancien MAIN
            Player player = new Player();
            var allowedExtensions = new[] { "mp3", "flac", "wav" };
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            List<string> musics = Seeker.GetMusics(allowedExtensions, path);
            List<Track> tracks = Seeker.GetTracks(musics);

            player.AddSongs(tracks);
            player.PlaySong();

            Console.ReadLine();*/

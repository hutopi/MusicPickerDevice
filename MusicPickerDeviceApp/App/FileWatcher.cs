using System.IO;
using System.Linq;
namespace MusicPickerDeviceApp.App
{
    public class FileWatcher
    {
        private FileSystemWatcher fileWatcher;
        private string[] extensions = { ".mp3", ".wav", ".flac"};
        private string path;

        public delegate void FileAddedEvent(string path);
        private FileAddedEvent callbackAdded;

        public delegate void FileDeletedEvent(string path);
        private FileDeletedEvent callbackDeleted;

        public FileWatcher(string path, FileAddedEvent add, FileDeletedEvent delete)
        {
            this.path = path;
            this.callbackAdded = add;
            this.callbackDeleted = delete;
            fileWatcher = new FileSystemWatcher(path, "*.*");
            fileWatcher.Created += new FileSystemEventHandler(FileWatcherCreated);
            fileWatcher.Deleted += new FileSystemEventHandler(FileWatcherDeleted);
            fileWatcher.EnableRaisingEvents = true;
        }

       public void FileWatcherCreated(object sender, FileSystemEventArgs e)
        {
            var ext = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();

            if (extensions.Any(ext.Equals))
            {
                this.callbackAdded(e.FullPath);
            }
        }

        public void FileWatcherDeleted(object sender, FileSystemEventArgs e)
        {
            var ext = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();

            if (extensions.Any(ext.Equals))
            {
                this.callbackDeleted(e.FullPath);
            }
        }
    }
}

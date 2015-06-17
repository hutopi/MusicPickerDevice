using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;
using MusicPickerDeviceApp.Properties;
using LiteDB;

namespace MusicPickerDeviceApp
{
    public class MusicPickerDevice : IDisposable
    {
        NotifyIcon ni;
        private ContextMenus menu;
        private LiteDatabase database;
        private Configuration configuration;
        private ApiClient client;

        public MusicPickerDevice()
        {
            ni = new NotifyIcon();
            menu = new ContextMenus()
            {
                ConnectionForm = new ConnectionForm(Connect),
                LoadForm = new LibraryPathsForm(UpdateLibraryPaths)
            };
            database = new LiteDatabase("musicpicker.db");
            configuration = new Configuration();
            client = new ApiClient(new Uri("http://localhost:50559"));
        }

        public void Initialize()
        {
            Display();
            if (this.configuration.Model.Registered)
            {
                this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName);
            }
            else
            {
                this.menu.ShowUnauthenticatedMenu();
            }
        }

        public void Display()
        {
            ni.Icon = Resources.icon;
            ni.Text = "Music Picker";
            ni.Visible = true;

            ni.ContextMenuStrip = menu.Menu;
        }

        private void Connect(string username, string deviceName, string password)
        {
            if (client.LogIn(username, password)) {
                int deviceId = client.DeviceAdd(deviceName);
                if (deviceId != -1)
                {
                    this.configuration.Model.Registered = true;
                    this.configuration.Model.DeviceName = deviceName;
                    this.configuration.Model.DeviceId = deviceId;
                    this.configuration.Model.Bearer = client.RetrieveBearer();
                    
                    this.menu.ShowAuthenticatedMenu(deviceName);
                }
            }
        }

        private void UpdateLibraryPaths(List<string> paths)
        {
            this.configuration.Model.Paths = paths;
            UpdateLibrary();
            /*if (loadForm.Loaded)
            {
                Library library = new Library(new LiteDatabase("Library.db"));
                foreach (Track t in loadForm.Tracks)
                {
                    library.AddTrack(t);
                }

                client.DeviceCollectionSubmit(connectionForm.DeviceId, library.Export());
            }*/
        }

        private void UpdateLibrary()
        {
            foreach (string path in this.configuration.Model.Paths)
            {
                Console.WriteLine(path);
            }
        }

        public void Dispose()
        {
            ni.Dispose();
        }

    }
}

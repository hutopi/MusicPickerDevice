using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        private Seeker seeker;
        private Library library;

        public MusicPickerDevice()
        {
            database = new LiteDatabase("musicpicker.db");
            configuration = new Configuration();
            library = new Library(database);
            seeker = new Seeker(library, new[] { "mp3", "wav" });
            ni = new NotifyIcon();
            menu = new ContextMenus()
            {
                ConnectionForm = new ConnectionForm(Connect),
                LoadForm = new LibraryPathsForm(configuration.Model, UpdateLibraryPaths)
            };
            client = new ApiClient(new Uri("http://localhost:50559"));
        }

        public async void Initialize()
        {
            Display();

            if (this.configuration.Model.Registered)
            {
                this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, false);
                this.client.ProvideBearer(this.configuration.Model.Bearer);
                UpdateLibrary();
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
                    this.configuration.Save();

                    this.menu.ShowAuthenticatedMenu(deviceName, false);
                }
            }
        }

        private void UpdateLibraryPaths(List<string> paths)
        {
            this.configuration.Model.Paths = paths;
            this.configuration.Save();
            UpdateLibrary();
        }

        private async Task UpdateLibrary()
        {
            this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, true);
            foreach (string path in this.configuration.Model.Paths)
            {
                await Task.Run(() => this.seeker.GetTracks(path));
            }

            await this.client.DeviceCollectionSubmit(this.configuration.Model.DeviceId, this.library.Export());
            this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, false);
        }

        public void Dispose()
        {
            ni.Dispose();
        }

    }
}

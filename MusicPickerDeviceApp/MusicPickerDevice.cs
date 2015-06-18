using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;
using MusicPickerDeviceApp.Properties;
using LiteDB;
using Microsoft.AspNet.SignalR.Client;

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
        private Player player;
        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        public MusicPickerDevice()
        {
            database = new LiteDatabase("musicpicker.db");
            configuration = new Configuration();
            library = new Library(database);
            seeker = new Seeker(library, new[] { "mp3", "wav" });
            ni = new NotifyIcon();
            menu = new ContextMenus()
            {
                SignUpForm = new SignUpForm(SignUp),
                ConnectionForm = new ConnectionForm(Connect),
                LoadForm = new LibraryPathsForm(configuration.Model, UpdateLibraryPaths)
            };

            player = new Player(library);

            client = new ApiClient(new Uri("http://localhost:50559"));
            hubConnection = new HubConnection("http://localhost:50559");
            hubProxy = hubConnection.CreateHubProxy("MusicHub");
            HubClient hubClient = new HubClient(player);
            hubClient.AttachToHub(hubProxy);
        }

        public async void Initialize()
        {
            Display();

            if (this.configuration.Model.Registered)
            {
                this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, false, Disconnect);
                this.client.ProvideBearer(this.configuration.Model.Bearer);
            }
            else
            {
                this.menu.ShowUnauthenticatedMenu();
            }

            await hubConnection.Start();

            if (this.configuration.Model.Registered)
            {
                player.AttachNextCallback(async () => await hubProxy.Invoke("Next", this.configuration.Model.DeviceId));
                await UpdateLibrary();
                await hubProxy.Invoke("RegisterDevice", this.configuration.Model.DeviceId);
            }
        }

        public void Display()
        {
            ni.Icon = Resources.icon;
            ni.Text = "Music Picker";
            ni.Visible = true;
            

            ni.ContextMenuStrip = menu.Menu;
        }

        private void SignUp(string username, string password, string confirmpassword)
        {
            if (password == confirmpassword)
            {
                if (client.SignUp(username, password))
                {
                    ni.ShowBalloonTip(2000, "Successful registration", string.Format("Welcome {0} !", username),
                        ToolTipIcon.Info);
                }
                else
                {
                    ni.ShowBalloonTip(2000, "Registration failed", "Server error",
                        ToolTipIcon.Error);
                }
            }
            else
            {
                ni.ShowBalloonTip(2000, "Registration failed", "The passwords are different",
                        ToolTipIcon.Warning);
            }
            
        }

        private void Connect(string username, string deviceName, string password)
        {
            if (client.LogIn(username, password))
            {
                int deviceId = client.DeviceAdd(deviceName);
                if (deviceId != -1)
                {
                    this.configuration.Model.Registered = true;
                    this.configuration.Model.DeviceName = deviceName;
                    this.configuration.Model.DeviceId = deviceId;
                    this.configuration.Model.Bearer = client.RetrieveBearer();
                    this.configuration.Save();

                    this.menu.ShowAuthenticatedMenu(deviceName, false, Disconnect);

                    ni.ShowBalloonTip(2000, "Successful connection", string.Format("Welcome {0} !", username),
                        ToolTipIcon.Info);
                }
            }
            else
            {
                ni.ShowBalloonTip(2000, "Connection failed", "Error.",
                        ToolTipIcon.Error);
            }
        }

        private void Disconnect()
        {
            this.configuration.Model.Registered = false;
            this.configuration.Save();

            this.menu.ShowUnauthenticatedMenu();
        }

        private async void UpdateLibraryPaths(List<string> paths)
        {
            this.configuration.Model.Paths = paths;
            this.configuration.Save();
            await UpdateLibrary();
        }

        private async Task UpdateLibrary()
        {
            this.library.Erase();
            this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, true, Disconnect);
            foreach (string path in this.configuration.Model.Paths)
            {
                await Task.Run(() => this.seeker.GetTracks(path));
            }

            await this.client.DeviceCollectionSubmit(this.configuration.Model.DeviceId, this.library.Export());
            this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, false, Disconnect);
        }

        public void Dispose()
        {
            ni.Dispose();
        }

    }
}

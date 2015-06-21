// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-17-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="MusicPickerDevice.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;
using MusicPickerDeviceApp.Properties;
using LiteDB;
using Microsoft.AspNet.SignalR.Client;

/// <summary>
/// The MusicPickerDeviceApp namespace.
/// </summary>
namespace MusicPickerDeviceApp
{
    /// <summary>
    /// Class MusicPickerDevice.
    /// Represents the device application.
    /// </summary>
    public class MusicPickerDevice : IDisposable
    {
        /// <summary>
        /// The notify icon of the application
        /// </summary>
        private NotifyIcon notifyIcon;
        /// <summary>
        /// The menu of the application
        /// </summary>
        private ContextMenus menu;
        /// <summary>
        /// The database
        /// </summary>
        private LiteDatabase database;
        /// <summary>
        /// The configuration of the device
        /// </summary>
        private Configuration configuration;
        /// <summary>
        /// The API client in order to interact with the Webservice
        /// </summary>
        private ApiClient client;
        /// <summary>
        /// The seeker to find the music files in the selected folders
        /// </summary>
        private Seeker seeker;
        /// <summary>
        /// The library who represents the music collection
        /// </summary>
        private Library library;
        /// <summary>
        /// The music player
        /// </summary>
        private Player player;
        /// <summary>
        /// The hub connection
        /// </summary>
        private HubConnection hubConnection;
        /// <summary>
        /// The hub proxy
        /// </summary>
        private IHubProxy hubProxy;
        /// <summary>
        /// The file watchers in order to be aware if a file is suppressed or added in the selected folders
        /// </summary>
        private List<FileWatcher> fileWatchers = new List<FileWatcher>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicPickerDevice"/> class.
        /// </summary>
        public MusicPickerDevice()
        {
            database = new LiteDatabase("musicpicker.db");
            configuration = new Configuration();
            library = new Library(database);
            seeker = new Seeker(library, new[] { "mp3", "wav" });
            notifyIcon = new NotifyIcon();
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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public async void Initialize()
        {
            Display();

            if (this.configuration.Model.Registered)
            {
                this.menu.ShowAuthenticatedMenu(this.configuration.Model.DeviceName, false, Disconnect);
                this.client.ProvideBearer(this.configuration.Model.Bearer);
                hubConnection.Headers.Add("Authorization", "Bearer " + this.configuration.Model.Bearer);
                await hubConnection.Start();
            }
            else
            {
                this.menu.ShowUnauthenticatedMenu();
            }

            if (this.configuration.Model.Registered)
            {
                player.AttachNextCallback(async () => await hubProxy.Invoke("Next", this.configuration.Model.DeviceId));
                await UpdateLibrary();

                foreach (var path in this.configuration.Model.Paths)
                {
                    fileWatchers.Add(new FileWatcher(path, AddTrack, DeleteTrack));
                }

                await hubProxy.Invoke("RegisterDevice", this.configuration.Model.DeviceId);
            }
        }

        /// <summary>
        /// Displays this instance.
        /// </summary>
        public void Display()
        {
            notifyIcon.Icon = Resources.icon;
            notifyIcon.Text = "Music Picker";
            notifyIcon.Visible = true;


            notifyIcon.ContextMenuStrip = menu.Menu;
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmpassword">The confirmpassword.</param>
        private void SignUp(string username, string password, string confirmpassword)
        {
            if (password == confirmpassword)
            {
                if (client.SignUp(username, password))
                {
                    notifyIcon.ShowBalloonTip(2000, "Successful registration", string.Format("Welcome {0} !", username),
                        ToolTipIcon.Info);
                }
                else
                {
                    notifyIcon.ShowBalloonTip(2000, "Registration failed", "Server error",
                        ToolTipIcon.Error);
                }
            }
            else
            {
                notifyIcon.ShowBalloonTip(2000, "Registration failed", "The passwords are different",
                        ToolTipIcon.Warning);
            }

        }

        /// <summary>
        /// Connects the specified device with the username account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="password">The password.</param>
        private async void Connect(string username, string deviceName, string password)
        {
            if (client.LogIn(username, password))
            {
                int searchId = client.DeviceGetIdByName(deviceName);
                int deviceId = 0;
                if (searchId != -1)
                {
                    deviceId = searchId;
                }
                else
                {
                    deviceId = client.DeviceAdd(deviceName);
                }

                if (deviceId != -1)
                {
                    this.configuration.Model.Registered = true;
                    this.configuration.Model.DeviceName = deviceName;
                    this.configuration.Model.DeviceId = deviceId;
                    this.configuration.Model.Bearer = client.RetrieveBearer();
                    this.configuration.Save();

                    this.menu.ShowAuthenticatedMenu(deviceName, false, Disconnect);

                    notifyIcon.ShowBalloonTip(2000, "Successful connection", string.Format("Welcome {0} !", username),
                        ToolTipIcon.Info);

                    await UpdateLibrary();

                    hubConnection.Headers.Add("Authorization", "Bearer " + this.configuration.Model.Bearer);
                    await hubConnection.Start();
                    await hubProxy.Invoke("RegisterDevice", this.configuration.Model.DeviceId);
                }
            }
            else
            {
                notifyIcon.ShowBalloonTip(2000, "Connection failed", "Error.",
                        ToolTipIcon.Error);
            }
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        private void Disconnect()
        {
            hubConnection.Headers.Remove("Authorization");
            hubConnection.Stop();
            this.ResetConfiguration();
            this.menu.LoadForm = new LibraryPathsForm(configuration.Model, UpdateLibraryPaths);
            this.menu.SignUpForm = new SignUpForm(SignUp);
            this.menu.ConnectionForm = new ConnectionForm(Connect);
            this.menu.ShowUnauthenticatedMenu();
        }

        /// <summary>
        /// Resets the configuration of the device model.
        /// </summary>
        private void ResetConfiguration()
        {
            this.configuration.Model.Registered = false;
            this.configuration.Model.Bearer = "";
            this.configuration.Model.DeviceId = 0;
            this.configuration.Model.DeviceName = "";
            this.configuration.Save();
        }

        /// <summary>
        /// Updates the library paths.
        /// </summary>
        /// <param name="paths">The paths.</param>
        private async void UpdateLibraryPaths(List<string> paths)
        {
            this.configuration.Model.Paths = paths;
            this.configuration.Save();
            await UpdateLibrary();
        }

        /// <summary>
        /// Updates the library.
        /// </summary>
        /// <returns>Task.</returns>
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

        /// <summary>
        /// Adds the track to the service.
        /// </summary>
        /// <param name="path">The path.</param>
        public void AddTrack(string path)
        {
            //@TODO
            Console.WriteLine(String.Format("File added: Path:{0}", path));
        }

        /// <summary>
        /// Deletes the track of the service.
        /// </summary>
        /// <param name="path">The path.</param>
        public void DeleteTrack(string path)
        {
            //@TODO
            Console.WriteLine(String.Format("File deleted: Path:{0}", path));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            hubConnection.Stop();
            notifyIcon.Dispose();
        }

    }
}

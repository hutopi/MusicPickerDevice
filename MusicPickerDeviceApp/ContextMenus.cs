using System;
using System.Diagnostics;
using System.Windows.Forms;
using MusicPickerDeviceApp.Properties;
using System.Drawing;
using MusicPickerDeviceApp.App;

namespace MusicPickerDeviceApp
{

    class ContextMenus
    {

        private ConnectionForm connectionForm = new ConnectionForm();
        private UploadForm loadForm;
        private ApiClient client;

        public ContextMenuStrip Menu { get; set; }
        private bool connected = false;

        public ContextMenus()
        {
            Menu = new ContextMenuStrip();
            client = new ApiClient(new Uri("http://localhost:50559"));
        }

        public void Create()
        {
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            item = new ToolStripMenuItem();
            item.Text = "Connect";
            item.Image = Resources._in;
            item.Click += new EventHandler(Connection_Click);
            Menu.Items.Add(item);

            sep = new ToolStripSeparator();
            Menu.Items.Add(sep);
 
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Image = Resources.exit;
            item.Click += new System.EventHandler(Exit_Click);
            Menu.Items.Add(item);
        }

        public void AddMenu(string name, int index, Bitmap img, EventHandler e)
        {
            ToolStripMenuItem item;
            item = new ToolStripMenuItem();
            item.Text = name;
            item.Image = img;
            item.Click += new System.EventHandler(e);
            Menu.Items.Insert(index, item);
        }

        public void DeleteMenu(int index)
        {
            Menu.Items.RemoveAt(index);
        }


        void Connection_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                connectionForm = new ConnectionForm();
                connectionForm.ShowDialog();

                //@TODO treatment BDD if already connected

                if (Connect())
                {
                    connected = true;
                    connectionForm.DeviceId = client.DeviceAdd(connectionForm.DeviceName);

                    ToolStripMenuItem item = (ToolStripMenuItem)sender;
                    Menu.Items.Remove(item);

                    AddMenu("Upload Music", 0, Resources.upload, Load_Click);
                    AddMenu(string.Format("Log out ({0})", connectionForm.DeviceName), 1, Resources._out, Logout_Click);
                }
            }
        }

        private bool Connect()
        {
            return client.LogIn(connectionForm.User, connectionForm.Pwd);
        }


        void Load_Click(object sender, EventArgs e)
        {
            loadForm = new UploadForm();
            loadForm.ShowDialog();

            if (loadForm.Loaded)
            {
                Library library = new Library("");
                foreach (Track t in loadForm.Tracks)
                {
                    library.AddTrack(t);
                }

                client.DeviceCollectionSubmit(connectionForm.DeviceId, library.Export());
            }
        }

        void Logout_Click(object sender, EventArgs e)
        {
            connectionForm = new ConnectionForm();
            Menu.Items.Clear();
            Create();
        }

        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
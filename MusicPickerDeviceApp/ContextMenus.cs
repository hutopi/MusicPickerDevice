using System;
using System.Diagnostics;
using System.Windows.Forms;
using MusicPickerDeviceApp.Properties;
using System.Drawing;
using MusicPickerDeviceApp.App;
using LiteDB;

namespace MusicPickerDeviceApp
{

    public class ContextMenus
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
            CheckIfAlreadyConnect();
        }

        public void CheckIfAlreadyConnect()
        {
            //@TODO accès à la base + set de connectionForm.DeviceName + set de connectionForm.DeviceId
            //@TODO Récupération du Token bearer
        }

        public void Create()
        {
            if (!connected)
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
            else
            {
                UpdateMenuItems();
            }
            
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
            connectionForm.ShowDialog();

            if (Connect())
            {
                connectionForm.DeviceId = client.DeviceAdd(connectionForm.DeviceName);

                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                Menu.Items.Remove(item);

                UpdateMenuItems();
            }
        }

        private void UpdateMenuItems()
        {
            AddMenu(string.Format("{0}", connectionForm.DeviceName), 0, Resources.device, Device_Click);
            AddMenu("Upload Music", 1, Resources.upload, Load_Click);    
        }

        private bool Connect()
        {
            client.SignUp(connectionForm.User, connectionForm.Pwd);
            return client.LogIn(connectionForm.User, connectionForm.Pwd);
        }

        void Load_Click(object sender, EventArgs e)
        {
            loadForm = new UploadForm();
            loadForm.ShowDialog();

            if (loadForm.Loaded)
            {
                Library library = new Library(new LiteDatabase("Library.db"));
                foreach (Track t in loadForm.Tracks)
                {
                    library.AddTrack(t);
                }

                client.DeviceCollectionSubmit(connectionForm.DeviceId, library.Export());
            }
        }

        void Device_Click(object sender, EventArgs e)
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
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

        public ConnectionForm ConnectionForm;
        public LibraryPathsForm LoadForm;
        private ApiClient client;

        public ContextMenuStrip Menu { get; set; }
        private ToolStripMenuItem ConnectToolStrip;
        private ToolStripMenuItem ExitToolStrip;
        private bool connected = false;

        public ContextMenus()
        {
            Menu = new ContextMenuStrip();
            client = new ApiClient(new Uri("http://localhost:50559"));

            ExitToolStrip = new ToolStripMenuItem()
            {
                Text = "Exit",
                Image = Resources.exit
            };
            ExitToolStrip.Click += new System.EventHandler(Exit_Click);
        }

        public void ShowUnauthenticatedMenu()
        {
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            ConnectToolStrip = new ToolStripMenuItem()
            {
                Text = "Connect",
                Image = Resources._in,
                
            };
            ConnectToolStrip.Click += new EventHandler(Connection_Click);
            Menu.Items.Add(ConnectToolStrip);

            Menu.Items.Add(new ToolStripSeparator());
            Menu.Items.Add(ExitToolStrip);
        }

        public void ShowAuthenticatedMenu(string deviceName, bool uploading)
        {
            Menu.Items.Clear();
            AddMenu(string.Format("{0}", deviceName), 0, Resources.device, Device_Click);

            if (!uploading)
            {
                AddMenu("Upload Music", 1, Resources.upload, Load_Click);
            }
            else
            {
                Menu.Items.Insert(1, new ToolStripMenuItem()
                {
                    Text = "Uploading...",
                    Enabled = false
                });
            }
            
            Menu.Items.Add(ExitToolStrip);
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
            ConnectionForm.ShowDialog();
        }

        void Load_Click(object sender, EventArgs e)
        {
            LoadForm.ShowDialog();
        }

        void Device_Click(object sender, EventArgs e)
        {
            /*connectionForm = new ConnectionForm();
            Menu.Items.Clear();
            ShowUnauthenticatedMenu();*/
        }

        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
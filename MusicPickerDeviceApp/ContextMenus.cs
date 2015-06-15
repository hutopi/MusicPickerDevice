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

        public ContextMenuStrip Menu { get; set; }

        public ContextMenus()
        {
            Menu = new ContextMenuStrip();
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
            if (!connectionForm.Connected)
            {
                connectionForm = new ConnectionForm();
                connectionForm.ShowDialog();

                if (connectionForm.Connected)
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)sender;
                    Menu.Items.Remove(item);

                    AddMenu("Upload Music", 0, Resources.upload, Load_Click);
                    AddMenu(string.Format("Log out ({0})", connectionForm.DeviceName), 1, Resources._out, Logout_Click);
                }
            }
        }


        void Load_Click(object sender, EventArgs e)
        {
            loadForm = new UploadForm();
            loadForm.ShowDialog();
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
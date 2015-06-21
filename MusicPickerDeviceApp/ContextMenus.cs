using System;
using System.Windows.Forms;
using MusicPickerDeviceApp.Properties;
using System.Drawing;

namespace MusicPickerDeviceApp
{

    public class ContextMenus
    {
        public ConnectionForm ConnectionForm;
        public SignUpForm SignUpForm;
        public LibraryPathsForm LoadForm;

        public ContextMenuStrip Menu { get; set; }
        private ToolStripMenuItem ConnectToolStrip;
        private ToolStripMenuItem SignUpToolStrip;
        private ToolStripMenuItem ExitToolStrip;

        public delegate void LogoutEvent();
        private LogoutEvent callback;

        public ContextMenus()
        {
            Menu = new ContextMenuStrip();

            ExitToolStrip = new ToolStripMenuItem()
            {
                Text = "Exit",
                Image = Resources.exit
            };
            ExitToolStrip.Click += new System.EventHandler(Exit_Click);
        }

        public void ShowUnauthenticatedMenu()
        {
            if (Menu.Items.Count > 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    DeleteMenu(0);
                }
            }
            
            SignUpToolStrip = new ToolStripMenuItem()
            {
                Text = "Sign up",
                Image = Resources._in,

            };
            SignUpToolStrip.Click += new EventHandler(SignUp_Click);
            Menu.Items.Add(SignUpToolStrip);

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

        public void ShowAuthenticatedMenu(string deviceName, bool uploading, LogoutEvent callback)
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

            AddMenu("Logout", 2, Resources._out, Logout_Click);
            this.callback = callback;
            
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

        void SignUp_Click(object sender, EventArgs e)
        {
            SignUpForm.ShowDialog();
        }

        void Load_Click(object sender, EventArgs e)
        {
            LoadForm.ShowDialog();
        }

        void Device_Click(object sender, EventArgs e)
        {
            
        }

        void Logout_Click(object sender, EventArgs e)
        {
            this.callback();
        }

        void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
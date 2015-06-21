using System;
using System.Windows.Forms;

namespace MusicPickerDeviceApp
{
    public partial class ConnectionForm : Form
    {
        public delegate void ConnectEvent(string username, string deviceName, string password);
        private ConnectEvent callback;

        public ConnectionForm(ConnectEvent callback)
        {
            this.callback = callback;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            callback(username.Text, device.Text, password.Text);
            this.Close();
        }
    }
}

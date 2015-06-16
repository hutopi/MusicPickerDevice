using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPickerDeviceApp
{
    public partial class ConnectionForm : Form
    {

        private string deviceName;

        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        public int DeviceId { get; set; }

        private string user;

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        private string pwd;

        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void App_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = username.Text;
            DeviceName = device.Text;
            pwd = password.Text;

            this.Close();
        }
    }
}

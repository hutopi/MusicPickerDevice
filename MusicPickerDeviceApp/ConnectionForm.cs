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

        private string user;

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public bool Connected { get; set; }

        public ConnectionForm()
        {
            InitializeComponent();
            Connected = false;
        }

        private void App_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //treatment here @TODO 


            //si OK
            Connected = true;
            user = username.Text;
            DeviceName = device.Text;            

            this.Close();
        }
    }
}

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
    public partial class SignUpForm : Form
    {
        public delegate void SignUpEvent(string username, string password, string confirmpassword);
        private SignUpEvent callback;

        public SignUpForm(SignUpEvent callback)
        {
            this.callback = callback;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            callback(Username.Text, Password.Text, ConfirmPassword.Text);
            this.Close();
        }
    }
}

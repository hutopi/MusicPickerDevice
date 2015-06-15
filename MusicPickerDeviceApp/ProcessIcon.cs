using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;
using MusicPickerDeviceApp.Properties;

namespace MusicPickerDeviceApp
{
    public class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        private ContextMenus menu;

        public ProcessIcon()
        {
            ni = new NotifyIcon();
            menu = new ContextMenus();
        }

        public void Display()
        {
            ni.Icon = Resources.icon;
            ni.Text = "Music Picker";
            ni.Visible = true;

            menu.Create();
            ni.ContextMenuStrip = menu.Menu;

            ApiClient client = new ApiClient(new Uri("http://localhost.fiddler:50559"));
            bool inscrit = client.SignUp("Pierre", "isen59");
            bool connected = client.LogIn("Pierre", "isen59");

            int deviceId = client.DeviceAdd("Fifoxy");
            int deviceId2 = client.DeviceAdd("Coquine");
            int deviceId3 = client.DeviceAdd("Gog");

            List<Device> devices = client.DevicesGet();
        }



        public void Dispose()
        {
            ni.Dispose();
        }

    }
}

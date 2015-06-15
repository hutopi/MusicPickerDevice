using System;
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
        }

        public void Dispose()
        {
            ni.Dispose();
        }
    
    }
}

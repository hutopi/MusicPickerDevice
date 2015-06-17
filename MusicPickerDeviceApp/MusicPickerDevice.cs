using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;
using MusicPickerDeviceApp.Properties;
using LiteDB;

namespace MusicPickerDeviceApp
{
    public class MusicPickerDevice : IDisposable
    {
        NotifyIcon ni;
        private ContextMenus menu;
        private LiteDatabase database;
        private Configuration configuration;

        public MusicPickerDevice()
        {
            ni = new NotifyIcon();
            menu = new ContextMenus();
            database = new LiteDatabase("musicpicker.db");
            configuration = new Configuration();
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

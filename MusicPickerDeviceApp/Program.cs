using System;
using System.Windows.Forms;

namespace MusicPickerDeviceApp
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (MusicPickerDevice pi = new MusicPickerDevice())
            {
                pi.Initialize();
                Application.Run();
            }
        }
    }
}

using System;

namespace MusicPickerDeviceApp.App
{
    public class DeviceState
    {
        public bool Playing { get; set; }
        public int Current { get; set; }
        public DateTime LastPause { get; set; }
        public int[] Queue { get; set; }
    }
}

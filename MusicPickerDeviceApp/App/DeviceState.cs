using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MusicPickerDeviceApp.App
{
    public class ConfigurationModel
    {
        public bool Registered { get; set; }
        public int DeviceId { get; set; }
        public string Bearer { get; set; }
        public string[] Paths { get; set; }

    }

    public class Configuration
    {
        public ConfigurationModel Model;

        public Configuration()
        {
            if (File.Exists("musicpicker.json"))
            {
                Load();
            }
            else
            {
                this.Model = new ConfigurationModel();
                Save();
            }
        }

        public void Load()
        {
            this.Model = JsonConvert.DeserializeObject<ConfigurationModel>(Convert.ToString(File.ReadAllBytes("musicpicker.json")));
        }

        public void Save()
        {
            File.WriteAllText("musicpicker.json", JsonConvert.SerializeObject(this.Model));
        }
    }
}

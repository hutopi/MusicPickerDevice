using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MusicPickerDeviceApp.App
{
    public class ConfigurationModel
    {
        public bool Registered { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Bearer { get; set; }
        public List<string> Paths { get; set; }

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
                this.Model = new ConfigurationModel()
                {
                    Paths = new List<string>()
                };
                Save();
            }
        }

        public void Load()
        {
            this.Model = JsonConvert.DeserializeObject<ConfigurationModel>(File.ReadAllText("musicpicker.json"));
        }

        public void Save()
        {
            File.WriteAllText("musicpicker.json", JsonConvert.SerializeObject(this.Model));
        }
    }
}

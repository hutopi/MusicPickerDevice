// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-17-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="Configuration.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents the model of the device's configuration.
    /// </summary>
    public class ConfigurationModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ConfigurationModel"/> is registered.
        /// </summary>
        /// <value><c>true</c> if registered; otherwise, <c>false</c>.</value>
        public bool Registered { get; set; }
        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>The device identifier.</value>
        public int DeviceId { get; set; }
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        public string DeviceName { get; set; }
        /// <summary>
        /// Gets or sets the bearer token.
        /// </summary>
        /// <value>The bearer.</value>
        public string Bearer { get; set; }
        /// <summary>
        /// Gets or sets the paths of the folders.
        /// </summary>
        /// <value>The paths.</value>
        public List<string> Paths { get; set; }

    }

    /// <summary>
    /// Class that represents the configuration of the device.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The model configuration
        /// </summary>
        public ConfigurationModel Model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            if (File.Exists("musicpicker.json"))
            {
                Load();
            }
            else
            {
                Model = new ConfigurationModel()
                {
                    Paths = new List<string>()
                };
                Save();
            }
        }

        /// <summary>
        /// Loads this instance from a json file.
        /// </summary>
        public void Load()
        {
            Model = JsonConvert.DeserializeObject<ConfigurationModel>(File.ReadAllText("musicpicker.json"));
        }

        /// <summary>
        /// Saves this instance to a json file.
        /// </summary>
        public void Save()
        {
            File.WriteAllText("musicpicker.json", JsonConvert.SerializeObject(Model));
        }
    }
}

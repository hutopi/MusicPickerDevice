// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-20-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-20-2015
// ***********************************************************************
// <copyright file="Device.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//     Music Picker is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The App namespace.
/// </summary>
namespace MusicPickerDeviceApp.App
{
    /// <summary>
    /// Class that represents a device.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the identifier of the device.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the registration date of the device.
        /// </summary>
        /// <value>The registration date.</value>
        public DateTime RegistrationDate { get; set; }
        /// <summary>
        /// Gets or sets the access date of the device.
        /// </summary>
        /// <value>The access date.</value>
        public DateTime AccessDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}

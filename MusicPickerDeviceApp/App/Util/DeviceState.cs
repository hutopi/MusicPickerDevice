﻿// ***********************************************************************
// Assembly         : MusicPickerDeviceApp
// Author           : Pierre
// Created          : 06-17-2015
//
// Last Modified By : Pierre
// Last Modified On : 06-21-2015
// ***********************************************************************
// <copyright file="DeviceState.cs" company="Hutopi">
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
    /// Class that represents the state of the device.
    /// </summary>
    public class DeviceState
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DeviceState"/> is playing.
        /// </summary>
        /// <value><c>true</c> if playing; otherwise, <c>false</c>.</value>
        public bool Playing { get; set; }
        /// <summary>
        /// Gets or sets the current ID track.
        /// </summary>
        /// <value>The current.</value>
        public int Current { get; set; }
        /// <summary>
        /// Gets or sets the last pause.
        /// </summary>
        /// <value>The last pause.</value>
        public DateTime LastPause { get; set; }
        /// <summary>
        /// Gets or sets the queue.
        /// </summary>
        /// <value>The queue.</value>
        public int[] Queue { get; set; }
    }
}

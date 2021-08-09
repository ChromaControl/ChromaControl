// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using AuraServiceLib;
using ChromaControl.Abstractions;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Providers.Asus
{
    /// <summary>
    /// An Asus device
    /// </summary>
    public class AsusDevice : IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        public string Name => _device.Name;

        /// <summary>
        /// The lights the device has
        /// </summary>
        public IEnumerable<IDeviceLight> Lights => _lights;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private readonly List<AsusDeviceLight> _lights;

        /// <summary>
        /// The number of lights on the device
        /// </summary>
        public int NumberOfLights => _device.Lights.Count;

        /// <summary>
        /// The device
        /// </summary>
        private readonly IAuraSyncDevice _device;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<AsusDeviceProvider> _logger;

        /// <summary>
        /// Creates an Asus device
        /// </summary>
        /// <param name="device">The device</param>
        /// <param name="logger">The logger</param>
        internal AsusDevice(IAuraSyncDevice device, ILogger<AsusDeviceProvider> logger)
        {
            _device = device;
            _lights = new List<AsusDeviceLight>();
            _logger = logger;

            foreach (IAuraRgbLight light in _device.Lights)
            {
                _lights.Add(new AsusDeviceLight(light));
            }
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            try
            {
                if (NumberOfLights > 0)
                    _device.Apply();
            }
            catch (COMException ex)
            {
                _logger.LogError(ex, "Failed Applying Lights");
            }
        }
    }
}

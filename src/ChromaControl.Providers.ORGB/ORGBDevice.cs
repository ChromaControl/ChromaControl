// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using ChromaControl.Abstractions;
using Microsoft.Extensions.Logging;
using OpenRGB.NET;

namespace ChromaControl.Providers.ORGB
{
    /// <summary>
    /// An ORGB device
    /// </summary>
    public class ORGBDevice : IDevice
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
        private readonly List<ORGBDeviceLight> _lights;

        /// <summary>
        /// The number of lights on the device
        /// </summary>
        public int NumberOfLights => _device.Colors.Length;

        /// <summary>
        /// The device
        /// </summary>
        private readonly OpenRGB.NET.Models.Device _device;

        private readonly OpenRGBClient _sdk;

        private readonly int _deviceIndex;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ORGBDeviceProvider> _logger;

        /// <summary>
        /// Creates an ORGB device
        /// </summary>
        /// <param name="device">The device</param>
        /// <param name="logger">The logger</param>
        internal ORGBDevice(OpenRGB.NET.Models.Device device, ILogger<ORGBDeviceProvider> logger, int deviceIndex, OpenRGBClient sdk)
        {
            _device = device;
            _lights = new List<ORGBDeviceLight>();
            _logger = logger;
            _deviceIndex = deviceIndex;
            _sdk = sdk;

            foreach (OpenRGB.NET.Models.Color light in _device.Colors)
                _lights.Add(new ORGBDeviceLight(light));
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            try
            {
                if (NumberOfLights > 0)
                {
                    _sdk.UpdateLeds(_deviceIndex, _device.Colors);
                }
            }
            catch (COMException ex)
            {
                _logger.LogError(ex, "Failed Applying Lights");
            }
        }
    }
}

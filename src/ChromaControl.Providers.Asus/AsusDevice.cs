// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using AuraServiceLib;
using ChromaControl.Abstractions;

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
        /// Creates an Asus device
        /// </summary>
        /// <param name="device">The device</param>
        internal AsusDevice(IAuraSyncDevice device)
        {
            _device = device;
            _lights = new List<AsusDeviceLight>();

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
            if (NumberOfLights > 0)
                _device.Apply();
        }
    }
}

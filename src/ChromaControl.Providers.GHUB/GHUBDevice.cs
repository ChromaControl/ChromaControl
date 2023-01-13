// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using ChromaControl.Abstractions;
using LedCSharp;

namespace ChromaControl.Providers.GHUB
{
    /// <summary>
    /// A GHUB device
    /// </summary>
    public class GHUBDevice : IDevice
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
        /// The number of lights on the device
        /// </summary>
        public int NumberOfLights => _device.NumberOfLeds;

        /// <summary>
        /// The device index
        /// </summary>
       // private readonly int _deviceIndex;

        /// <summary>
        /// The device
        /// </summary>
        private readonly GHUBDev _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private readonly List<GHUBDeviceLight> _lights;

        /// <summary>
        /// Creates a GHUB device
        /// </summary>
        /// <param name="device">The device index</param>
        internal GHUBDevice(string name, DeviceType type)
        {
            _device = new GHUBDev();

            // The SDK assigns LED colors to specific peripherals, you can assign a color only to all devices of a specific type. 
            _device.deviceType = type;
            _device.Name = name;
         
            // As the SDK has no way to tell how many LED's are present, we default just to one, every led will be the same color
            _device.NumberOfLeds = 1;


            _lights = new List<GHUBDeviceLight>();
            _lights.Add(new GHUBDeviceLight(new System.Drawing.Color()));

        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            if (NumberOfLights > 0)
            {
                GHUBDeviceLight l = _lights[0];

                // The Logitech SDK expects RGB values in percentages instead of the usual 0-255 range
                var R = l.Color.R / 255.0 * 100.0;
                var G = l.Color.G / 255.0 * 100.0;
                var B = l.Color.B / 255.0 * 100.0;
                LogitechGSDK.LogiLedSetLighting(((int)R),(int)G,(int)B);
    
            }
        }
    }
}

